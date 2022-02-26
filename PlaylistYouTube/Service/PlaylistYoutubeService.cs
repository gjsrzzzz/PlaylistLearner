using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Jalindi.VideoUtil.Util;
using Microsoft.Extensions.Options;

namespace PlaylistYouTube.Service;

public class PlaylistYoutubeService : IVideoProvider
{
    private readonly PlaylistYouTubeOptions options;
    private string YouTubeApiKey => options.YouTubeApiKey;
    private readonly HttpClient client = new HttpClient();
    private readonly YouTubeService service;
    private readonly string[] defaultPlaylistIds;
    public PlaylistYoutubeService(IOptions<PlaylistYouTubeOptions> options)
    {
        this.options = options.Value;
        defaultPlaylistIds = string.IsNullOrEmpty(this.options.PlaylistIds) ? Array.Empty<string>():
            (from a in this.options.PlaylistIds.Split(',') select a.Trim()).ToArray();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", YouTubeApiKey);
        service = new YouTubeService(new BaseClientService.Initializer() { ApiKey = YouTubeApiKey });
    }
    
    public static TimeSpan GetTimeSpan(string duration)
    {
        var match = Regex.Match(duration, @"PT(?:(\d+)H)?(?:(\d+)M)?(?:(\d+)S)?$");
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        int totalseconds;

        if (!match.Success) return TimeSpan.Zero;
        hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
        minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
        seconds = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;
        totalseconds = hours * 3600 + minutes * 60 + seconds;
        return TimeSpan.FromSeconds(totalseconds);
    }

    public async Task<VideoInfo> GetVideoInfo(string videoId)
    {
        var request =
            service.Videos.List("contentDetails,id,player,snippet");
        request.Id = videoId;
        request.MaxWidth = 480;
        var result = await request.ExecuteAsync();
        var item = result?.Items.FirstOrDefault();
        return CreateVideoInfo(item);
    }

    public async Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds)
    {
        var videos = new List<VideoInfo>();
        int skip = 0;
        const int maxPerRequest=50;
        while (skip < videoIds.Count)
        {
            var request =
                service.Videos.List("contentDetails,id,player,snippet");
            request.Id = new Repeatable<string>(videoIds.Skip(skip).Take(50));
            request.MaxWidth = 480;
            var result = await request.ExecuteAsync();
            if (result?.Items == null) return videos;
            videos.AddRange(result.Items.Select(CreateVideoInfo));
            skip += result.Items.Count;
        }
        videos.Sort((x,y)=>x.Index.CompareTo(y.Index));
        return videos;
    }
    

    private static VideoInfo CreateVideoInfo(Video? item)
    {
        var snippet = item?.Snippet;
        var player = item?.Player;
        var contentDetails = item?.ContentDetails;
        var duration = contentDetails == null ? TimeSpan.Zero : GetTimeSpan(contentDetails.Duration);
        if (item == null || snippet == null || player == null || contentDetails == null) return new VideoInfo();
        int? index = 99999;
        var match = Regex.Match(snippet.Title, @".*\(\s*(\d+)\s*\)\s*$");
        if (match.Success && int.TryParse(match.Groups[1].Value, out var value))
        {
            index=value;
        }
        else
        {
            match = Regex.Match(snippet.Title, @".*\s+(\d+)\s*$");
            if (match.Success && int.TryParse(match.Groups[1].Value, out var value2))
            {
                index=value2;
            }
        }
        return new VideoInfo
            {
                Title = snippet.Title, Valid = true, Id = item.Id, Index=index??-1, Player = player.EmbedHtml, Duration = duration,
                Dimension = contentDetails.Dimension,
                Width = player.EmbedWidth == null ? 0 : (int)player.EmbedWidth,
                Height = player.EmbedHeight == null ? 0 : (int)player.EmbedHeight,
                AspectRatio = player.EmbedWidth == null || player.EmbedHeight == null
                    ? AspectRatio.Unknown
                    : AspectRatioHelper.GetAspectRatio((int)player.EmbedWidth, (int)player.EmbedHeight),
                Channel = snippet.ChannelTitle, Description = new Description(snippet.Description, duration)
            };
    }

    public async Task<PlaylistInfo> GetPlaylistInfo(string playListId, bool includeVideoInfo = false)
    {
        if (playListId.Equals("default", StringComparison.OrdinalIgnoreCase) && defaultPlaylistIds.Length == 1)
        {
            playListId = defaultPlaylistIds[0];
        }
        var request =service.Playlists.List("contentDetails,id,snippet");
        request.Id = playListId;
        var itemsTaskRequest=GetPlaylistItems(playListId);
        var result = await request.ExecuteAsync();
        var item = result?.Items.FirstOrDefault();
        var snippet = item?.Snippet;
        var contentDetails = item?.ContentDetails;
        var itemsTaskResult = itemsTaskRequest.Result;
        var videoList = GetVideoList(itemsTaskResult).ToList();
        while (!string.IsNullOrEmpty(itemsTaskResult.NextPageToken))
        {
            itemsTaskResult = GetPlaylistItems(playListId, itemsTaskResult.NextPageToken).Result;
            var videoList2 = GetVideoList(itemsTaskResult);
            videoList.AddRange(videoList2);
        }

        if (item == null || snippet == null || contentDetails == null) return new PlaylistInfo();
        return new PlaylistInfo
            {Title = snippet.Title, Description = new Description(snippet.Description, TimeSpan.Zero),
                Valid = true, Id=item.Id, Channel = snippet.ChannelTitle,
                VideoCount=(int)(contentDetails.ItemCount??0), VideoIdList = videoList,
                VideoInfoList = includeVideoInfo?await GetVideosInfo(videoList): new List<VideoInfo>()
                };
    }

    private static IEnumerable<string> GetVideoList(PlaylistItemListResponse itemsTaskResult)
    {
        var videoList = itemsTaskResult?.Items == null
            ? new List<string>()
            : from i in itemsTaskResult.Items select i.ContentDetails.VideoId;
        return videoList;
    }

    private Task<PlaylistItemListResponse> GetPlaylistItems(string playListId, string? nextPageToken=null)
    {
        var request =service.PlaylistItems.List("contentDetails");
        request.PlaylistId = playListId;
        request.MaxResults = 50;
        if (nextPageToken!=null) request.PageToken = nextPageToken;
        return request.ExecuteAsync();
      //  return result?.Items == null ? new List<string>():
     //       from i in result.Items select i.ContentDetails.VideoId;
    }


}