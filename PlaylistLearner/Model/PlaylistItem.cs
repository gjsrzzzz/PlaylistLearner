namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink, HyperLink
};
public enum Format
{
    Default, Mobile
};
public record PlaylistItem(ItemType Type, Format Format, string Name, string AltName, string Description, string Link, int Start=-1, int End=-1);