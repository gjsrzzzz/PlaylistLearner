using Jalindi.VideoUtil.Model;

namespace Jalindi.VideoUtil;

public interface IVideoProvider
{
    public Task<PlaylistInfo> GetPlaylistInfo(string playListId, bool includeVideoInfo = false);
    public Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds);

    public Task<VideoInfo> GetVideoInfo(string videoId);
}