namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink
};
public enum Format
{
    Default, Mobile
};
public record PlaylistItem(ItemType Type, Format Format, string Name, string AltName, string Description, string Link);