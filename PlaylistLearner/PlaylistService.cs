using PlaylistLearner.Model;
using System.Linq;
using Microsoft.Extensions.Options;
using VideoUtil;
using VideoUtil.Model;

namespace PlaylistLearner;

public static class PlaylistHelper
{
    public static string? GetLink(this PlaylistInfo playlist)
    {
        return string.Empty;
    }
    public static string? GetLinkText(this PlaylistInfo playlist)
    {
        return string.Empty;
    }
    public static string? GetAltName(this VideoInfo playlist)
    {
        return string.Empty;
    }
}


public class PlaylistService
{
    private readonly PlaylistOptions options;
    private readonly IVideoProvider videoProvider;
    public PlaylistService(IVideoProvider videoProvider, IOptions<PlaylistOptions> options)
    {
        this.videoProvider=videoProvider;
        this.options = options.Value;
    }
    public async Task<Playlist> GetPlaylist(string playListId)
    {
          var youtubeList = await videoProvider.GetPlaylistInfo(playListId);
          var videos = await videoProvider.GetVideosInfo(youtubeList.VideoIdList);
          var items = (from i in videos select new PlaylistItem(ItemType.Default,
              i.AspectRatio, i.Title, i.GetAltName(), i.Description.Remaining, $"{i.Id}", -1, -1));
          var playList = new Playlist()
          {
              Name = youtubeList.Title,
              Link = youtubeList.GetLink(),
              LinkText = youtubeList.GetLinkText(),
              Items=items.ToList()
          };
          return playList;
    }

    public async Task<Playlist>  GetPlaylist()
    {
        return await GetPlaylist(options.PlaylistId);
    }
}