namespace Jalindi.VideoUtil.Model;

public class PlaylistInfo
{
    public string Id { get; init; } = string.Empty;
    public bool Valid { get; init; } = false;
    public string Channel { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public Description Description { get; init; } = Description.Empty;
    public int VideoCount { get; init; }
    public List<string> VideoIdList { get; init; }
    public List<VideoInfo> VideoInfoList { get; init; }
    public override string ToString()
    {
        return $"{Id}: {Title}";
    }
}