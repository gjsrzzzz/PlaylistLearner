﻿namespace Jalindi.VideoUtil.Model;

public class PlaylistInfo
{
    public string Id { get; set; } = string.Empty;
    public bool Valid { get; set; } = false;
    public string Channel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Key  => Description?.GetStringTag(nameof(Key));
    public string IconBase => Key==null || string.IsNullOrEmpty(Key) ? string.Empty : $"{Key}/";
    public Description Description { get; set; } = Description.Empty;
    public List<KeyValuePair<string, string>> Tags => Description.Tags;
    public int VideoCount { get; set; }
    public List<string> VideoIdList { get; set; }= [];
    public List<VideoInfo> VideoInfoList { get; set; } = [];
    public override string ToString()
    {
        return $"{Id}: {Title}";
    }


}