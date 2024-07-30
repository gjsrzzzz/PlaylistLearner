using System;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace PlaylistApi;

public class PlaylistYouTubeFunction
{
    private readonly IVideoProvider videoProvider;
    public PlaylistYouTubeFunction(IVideoProvider videoProvider)
    {
        this.videoProvider = videoProvider;
    }
    [Function("Playlist")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        if (!req.Query.TryGetValue("playlistId", out var playlistId)) return new UnauthorizedResult();
        if (playlistId.ToString().Equals("hello",StringComparison.OrdinalIgnoreCase))
            return new OkObjectResult(new Playlist() { Id = playlistId});
        // Should be authenticated to get any playlist
        if (!playlistId.ToString().Equals("default",StringComparison.OrdinalIgnoreCase))
            return new UnauthorizedResult();

        var includeVideoInfo=false;
        if (req.Query.TryGetValue("includeVideoInfo", out var includeVideoInfoString))
        {
            includeVideoInfo = includeVideoInfoString.ToString().Equals("true",StringComparison.OrdinalIgnoreCase);
        }
        var playlist = await videoProvider.GetPlaylistInfo(playlistId, includeVideoInfo);
        return new OkObjectResult(playlist);

    }

    [Function("Manifest")]
    public async Task<IActionResult> RunManifest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manifest.json")]
        HttpRequest req,
        ILogger log)
    {
        var playlist = await videoProvider.GetPlaylistInfo("default", false);
        var origin = req.Headers["Origin"].ToString();
        origin = string.IsNullOrEmpty(origin) ? ".." : origin;
        var origin2 = string.IsNullOrEmpty(origin) ? "../" : origin;
        var manifest = new Manifest($"{playlist.Title} Playlist", playlist.Title,  origin2,
            "standalone", "#ffffff", "#03173d", false, 
            new []{new Icon( $"{origin}/{playlist.GetIconBase()}icon-512.png", "image/png", "512x512"), 
                new Icon($"{origin}/{playlist.GetIconBase()}icon-192.png", "image/png", "192x192")});
        return new OkObjectResult(manifest);
    }

    [Function("Hash")]
    public async Task<IActionResult> RunHash(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        if (!req.Query.TryGetValue("password", out var password)) return new UnauthorizedResult();
        if (!req.Query.TryGetValue("salty", out var salty)) return new UnauthorizedResult();
        if (!req.Query.TryGetValue("id", out var id)) return new UnauthorizedResult();
        if (!id.Equals("PlaylistLearner")) return new UnauthorizedResult();

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(salty),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return new OkObjectResult(hashed);
    }


}