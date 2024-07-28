using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jalindi.VideoUtil.Model;


public class PlaylistInfo
{
    public string Id { get; set; } = string.Empty;
    public bool Valid { get; set; } = false;
    public string Channel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    
    public string? GetKey() {
        return Description?.GetStringTag("Key");
    }
    
    public string GetIconBase()
    {
        
        return GetKey() == null || string.IsNullOrEmpty(GetKey()) ? string.Empty : $"{GetKey()}/";
    }

    public Description Description { get; set; } = Description.Empty;

    public List<KeyValuePair<string, string>> GetTags()

    {
        return Description.Tags;
    }

    public int VideoCount { get; set; }
    public List<string> VideoIdList { get; set; }= [];
    public List<VideoInfo> VideoInfoList { get; set; } = [];
    public override string ToString()
    {
        return $"{Id}: {Title}";
    }


}