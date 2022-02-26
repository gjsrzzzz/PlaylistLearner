
using Jalindi.VideoUtil.Util;

namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink, HyperLink
};


public record PlaylistItem(ItemType Type, AspectRatio AspectRatio, string Name, string AltName, string Description, string Link,
    int Start = -1, int End = -1)
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
    public int ExtendedEnd => HasEnd ? (End - Start) * 2 + Start : End;
}