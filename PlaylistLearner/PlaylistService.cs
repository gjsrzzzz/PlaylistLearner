using PlaylistLearner.Model;
using System.Linq;
using System.Net.Sockets;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.Extensions.Options;

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
          var youtubeList = await videoProvider.GetPlaylistInfo(playListId, true);
          var items = new List<PlaylistItem>();
          foreach (var videoInfo in youtubeList.VideoInfoList)
          {
              Add(items, videoInfo);
          }
          
          var playList = new Playlist()
          {
              Name = youtubeList.Title,
              Description=youtubeList.Description.Remaining,
              Silent = youtubeList.Description.GetBooleanTag(nameof(Playlist.Silent)),
              SpeedControls = youtubeList.Description.GetBooleanTag(nameof(Playlist.SpeedControls)),
              Link = youtubeList.GetLink(),
              LinkText = youtubeList.GetLinkText(),
              Items=items
          };
          return playList;
    }

    private void Add(ICollection<PlaylistItem> items, VideoInfo video)
    {
        if (video.Description.TimeCodes.Count > 0)
        {
            foreach (var timeCode in video.Description.TimeCodes)
            {
                var playlistItem = new PlaylistItem(ItemType.Default,
                    video.AspectRatio, timeCode.Name, timeCode.Extra??string.Empty, video.Description.Remaining, $"{video.Id}", 
                    timeCode.Start.TotalSeconds, timeCode.End.TotalSeconds);
                items.Add(playlistItem);
            }
        }
        else
        {
            var playlistItem = new PlaylistItem(ItemType.Default,
                video.AspectRatio, video.Title, video.GetAltName()??string.Empty, video.Description.Remaining, $"{video.Id}", -1, -1);
            items.Add(playlistItem);
        }
    }

    public async Task<Playlist>  GetPlaylist()
    {
        return await GetPlaylist(options.PlaylistId);
    }
}