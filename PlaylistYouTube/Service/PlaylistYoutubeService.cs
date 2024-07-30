using System.Net.Http.Headers;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
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
    


    public async Task<VideoInfo> GetVideoInfo(string videoId)
    {
        var request =
            service.Videos.List("contentDetails,id,player,snippet");
        request.Id = videoId;
        request.MaxWidth = 480;
        var result = await request.ExecuteAsync();
        var item = result?.Items.FirstOrDefault();
        return new PlaylistFetcher(service).CreateVideoInfo(item);
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
        var fetcher=new PlaylistFetcher(service);
        while (!string.IsNullOrEmpty(itemsTaskResult.NextPageToken))
        {
            itemsTaskResult = GetPlaylistItems(playListId, itemsTaskResult.NextPageToken).Result;
            var videoList2 = GetVideoList(itemsTaskResult);
            videoList.AddRange(videoList2);
        }

        if (item == null || snippet == null || contentDetails == null) return new PlaylistInfo();
        var description = new Description(snippet.Description, TimeSpan.Zero, 0);
        return new PlaylistInfo
            {Title = snippet.Title, Description = description,
                Valid = true, Id=item.Id, Channel = snippet.ChannelTitle,
                VideoCount=(int)(contentDetails.ItemCount??0), VideoIdList = videoList,
                VideoInfoList = includeVideoInfo?await fetcher.GetVideosInfo(videoList): new List<VideoInfo>()
                };
    }
    
    public async Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds)
    {
        return await new PlaylistFetcher(service).GetVideosInfo(videoIds);
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