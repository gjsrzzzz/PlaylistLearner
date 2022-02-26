using System;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace PlaylistApi;

public class PlaylistYouTubeFunction
{
    private readonly IVideoProvider videoProvider;
    public PlaylistYouTubeFunction(IVideoProvider videoProvider)
    {
        this.videoProvider = videoProvider;
    }
    [FunctionName("Playlist")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        if (!req.Query.TryGetValue("playlistId", out var playlistId)) return new UnauthorizedResult();
        if (playlistId.ToString().Equals("hello",StringComparison.OrdinalIgnoreCase))
            return new OkObjectResult(new Playlist() { Id = playlistId});
        var includeVideoInfo=false;
        if (req.Query.TryGetValue("includeVideoInfo", out var includeVideoInfoString))
        {
            includeVideoInfo = includeVideoInfoString.ToString().Equals("true",StringComparison.OrdinalIgnoreCase);
        }
        var playlist = await videoProvider.GetPlaylistInfo(playlistId, includeVideoInfo);
        return new OkObjectResult(playlist);

    }

    [FunctionName("Manifest")]
    public async Task<IActionResult> RunManifest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manifest.json")]
        HttpRequest req,
        ILogger log)
    {
        var playlist = await videoProvider.GetPlaylistInfo("default", false);
        var manifest = new Manifest(playlist.Title, playlist.Title, "./",
            "standalone", "#ffffff", "#03173d", false, 
            new []{new Icon( "icon-512.png", "image/png", "512x512"), new Icon("icon-192.png", "image/png", "192x192")});
        return new OkObjectResult(manifest);
    }

}