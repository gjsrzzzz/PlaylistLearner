using System.Net.Http.Json;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;

namespace PlaylistApiClient;

public class PlaylistApiClient : IVideoProvider
{
    private readonly HttpClient httpClient;
    public PlaylistApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<PlaylistInfo> GetPlaylistInfo(string playListId, bool includeVideoInfo = false)
    {
        var playlistInfo = await httpClient.GetFromJsonAsync<PlaylistInfo>($"/api/Playlist?playlistId={playListId}&includeVideoInfo={includeVideoInfo}") ?? new PlaylistInfo();
        return playlistInfo;
    }

    public Task<List<VideoInfo>> GetVideosInfo(List<string> videoIds)
    {
        throw new NotImplementedException();
    }

    public Task<VideoInfo> GetVideoInfo(string videoId)
    {
        throw new NotImplementedException();
    }
}