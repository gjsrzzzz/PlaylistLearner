﻿using System.Text.RegularExpressions;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Jalindi.VideoUtil.Model;
using Jalindi.VideoUtil.Util;

namespace PlaylistYouTube.Service;

internal class PlaylistFetcher
{
    private int _order=0;
    private readonly YouTubeService _service;

    public PlaylistFetcher(YouTubeService service)
    {
        this._service = service;
    }

    public async Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds)
    {
        var videos = new List<VideoInfo>();
        int skip = 0;
        const int maxPerRequest=50;
        while (skip < videoIds.Count)
        {
            var request =
                _service.Videos.List("contentDetails,id,player,snippet");
            request.Id = new Repeatable<string>(videoIds.Skip(skip).Take(50));
            request.MaxWidth = 480;
            var result = await request.ExecuteAsync();
            if (result?.Items == null) return videos;
            videos.AddRange(result.Items.Select(CreateVideoInfo));
            skip += result.Items.Count;
        }

        /*      if (orderBy == OrderBy.EndNumber)
              {
                  videos.Sort((x, y) => x.Index.CompareTo(y.Index));
              }*/
        return videos;
    }
    
    internal VideoInfo CreateVideoInfo(Video? item)
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
        var videoInfo= new VideoInfo
        {
            Title = snippet.Title, Valid = true, Id = item.Id, Index=index??-1, Player = player.EmbedHtml, Duration = duration,
            Dimension = contentDetails.Dimension,
            Width = player.EmbedWidth == null ? 0 : (int)player.EmbedWidth,
            Height = player.EmbedHeight == null ? 0 : (int)player.EmbedHeight,
            AspectRatio = player.EmbedWidth == null || player.EmbedHeight == null
                ? AspectRatio.Unknown
                : AspectRatioHelper.GetAspectRatio((int)player.EmbedWidth, (int)player.EmbedHeight),
            Channel = snippet.ChannelTitle, Description = new Description(snippet.Description, duration, _order)
        };
        _order = videoInfo.Description.NextOrder;
        return videoInfo;
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
}