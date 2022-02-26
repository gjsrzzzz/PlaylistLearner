using VideoUtil.Model;

namespace VideoUtil;

public interface IVideoProvider
{
    public Task<PlaylistInfo> GetPlaylistInfo(string playListId);
    public Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds);

    public Task<VideoInfo> GetVideoInfo(string videoId);
}