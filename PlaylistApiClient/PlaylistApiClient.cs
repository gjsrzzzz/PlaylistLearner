using System.Net.Http.Json;
using System.Text.Json;
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
    
    private static readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web);

    public async Task<PlaylistInfo> GetPlaylistInfo(string playListId, bool includeVideoInfo = false)
    {
        var url = $"/api/Playlist?playlistId={playListId}&includeVideoInfo={includeVideoInfo}";
        
        var result = await httpClient.GetStringAsync(url);
        await Console.Out.WriteLineAsync(result);
        var playlistInfo = JsonSerializer.Deserialize<PlaylistInfo>(result, DefaultSerializerOptions) ?? new PlaylistInfo();

//        var playlistInfo = await httpClient.GetFromJsonAsync<PlaylistInfo>(url) ?? new PlaylistInfo();
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