﻿namespace Jalindi.VideoUtil.Model;

public class PlaylistInfo
{
    public string Id { get; init; } = string.Empty;
    public bool Valid { get; init; } = false;
    public string Channel { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Key  => Description?.GetStringTag(nameof(Key));
    public string IconBase => Key==null || string.IsNullOrEmpty(Key) ? string.Empty : $"{Key}/";
    public Description Description { get; init; } = Description.Empty;
    public List<KeyValuePair<string, string>> Tags => Description.Tags;
    public int VideoCount { get; init; }
    public List<string> VideoIdList { get; init; }
    public List<VideoInfo> VideoInfoList { get; init; }
    public override string ToString()
    {
        return $"{Id}: {Title}";
    }


}