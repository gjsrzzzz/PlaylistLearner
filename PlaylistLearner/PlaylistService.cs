using PlaylistLearner.Model;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.Extensions.Options;

namespace PlaylistLearner;

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
          var keyedItems = new Dictionary<string, PlaylistItem>();
          foreach (var videoInfo in youtubeList.VideoInfoList)
          {
              Add(items,keyedItems, videoInfo);
          }

          var orderBy = youtubeList.Description.GetEnumTag(nameof(Playlist.OrderBy), OrderBy.Sequence);
          Sort(items, orderBy);

          var linkInfo = youtubeList.Description.GetStringsTag(nameof(Playlist.Link));
          var playList = new Playlist()
          {
              Name = youtubeList.Title,
              Description=youtubeList.Description.Remaining,
              Silent = youtubeList.Description.GetBooleanTag(nameof(Playlist.Silent)),
              SpeedControls = youtubeList.Description.GetBooleanTag(nameof(Playlist.SpeedControls)),
              OrderBy = orderBy,
              Link = linkInfo is { Length: > 1 }?linkInfo[1]:string.Empty,
              LinkText = linkInfo==null || linkInfo.Length==0?string.Empty: linkInfo[0],
              Items=items,
              KeyedItems=keyedItems
          };
          return playList;
    }

    private void Sort(List<PlaylistItem> items, OrderBy orderBy)
    {
        if (orderBy == OrderBy.OrderNumber)
        {
            items.Sort((x, y)=>x.Order.CompareTo(y.Order));
        }
    }

    private void Add(ICollection<PlaylistItem> items, IDictionary<string, PlaylistItem> keyedItems, VideoInfo video)
    {
        if (video.Description.TimeCodes.Count > 0)
        {
            foreach (var timeCode in video.Description.TimeCodes)
            {
                if (timeCode.Ignore) continue;
                var playlistItem = new PlaylistItem(ItemType.Default,
                    video.AspectRatio, timeCode.Name, timeCode.Key,
                    timeCode.AltName??string.Empty, timeCode.Description??string.Empty, $"https://youtu.be/{video.Id}", 
                    timeCode.Start.TotalSeconds, timeCode.End.TotalSeconds, timeCode.Order, timeCode.Lesson);
                if (string.IsNullOrEmpty(timeCode.Key))
                {
                    items.Add(playlistItem);
                }
                else
                {
                    keyedItems.Add(timeCode.Key, playlistItem);
                }
            }
        }
        else
        {
            var playlistItem = new PlaylistItem(ItemType.Default,
                video.AspectRatio, video.Title, video.Description.GetStringTag("Key")??string.Empty,
                video.Description.GetStringTag("Alt")??string.Empty, video.Description.Remaining, $"https://youtu.be/{video.Id}", -1, -1);
            items.Add(playlistItem);
        }
    }

    public async Task<Playlist>  GetPlaylist()
    {
        return await GetPlaylist(options.PlaylistId);
    }
}