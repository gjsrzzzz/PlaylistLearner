namespace PlaylistLearner.Model;

public enum ItemType
{
    Default, VideoLink, PrivateLink
};
public record PlaylistItem(ItemType type, string Name, string AltName, string Description, string Link);