
using Jalindi.VideoUtil.Util;

namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink, HyperLink
};


public record PlaylistItem(ItemType Type, AspectRatio AspectRatio, string Name, string Key, string AltName, string Description, string Link,
    double Start = -1, double End = -1, int Order=-1, string Lesson="")
{
    public string PaddingPercent => $"{AspectRatio.Ratio()*100}%";

    public string VideoId
    {
        get
        {
            var link = Link.Trim();
            int index = link.IndexOf("?v=", StringComparison.Ordinal);
            if (index > 0) link = link[(index + 3)..];
            else
            {
                index = link.LastIndexOf("/", StringComparison.Ordinal);
                if (index > 0)
                {
                    link = link[(index + 1)..];
                }
            }

            return link;
        }
    }

    public bool HasEnd => End > 0;
    public double ExtendedEnd => HasEnd ? (End - Start) * 2 + Start : End;
}