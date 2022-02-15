namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink, HyperLink
};
public enum Format
{
    Default, Standard, Mobile
};

public record PlaylistItem(ItemType Type, Format Format, string Name, string AltName, string Description, string Link,
    int Start = -1, int End = -1)
{
    public string PaddingPercent => Format switch
    {
        Format.Default => "56.25%",
        Format.Standard => "75%",
        Format.Mobile => "176.33%",
        _ => throw new ArgumentOutOfRangeException(nameof(Format), Format, null)
    };

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