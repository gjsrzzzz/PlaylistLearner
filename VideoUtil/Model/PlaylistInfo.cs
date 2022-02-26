namespace Jalindi.VideoUtil.Model;

public class PlaylistInfo
{
    public string Id { get; init; } = string.Empty;
    public bool Valid { get; init; } = false;
    public string Channel { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public Description Description { get; init; } = Description.Empty;
    public List<KeyValuePair<string, string>> Tags => Description.Tags;
    public int VideoCount { get; init; }
    public List<string> VideoIdList { get; init; }
    public List<VideoInfo> VideoInfoList { get; init; }
    public override string ToString()
    {
        return $"{Id}: {Title}";
    }

    public bool GetBooleanTag(string tagName)
    {
        return TryGetTag(tagName, out var tagValue) && tagValue.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    private bool TryGetTag(string tagName, out string tagValue)
    {
        tagValue = string.Empty;
        foreach (var pair in Tags.Where(pair => pair.Key.Equals(tagName, StringComparison.OrdinalIgnoreCase)))
        {
            tagValue = pair.Value.Trim();
            return true;
        }
        return false;
    }
}