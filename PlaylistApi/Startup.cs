using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaylistYouTube.Service;
[assembly: FunctionsStartup(typeof(PlaylistApi.Startup))]

namespace PlaylistApi;

public class Startup: FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<IVideoProvider, PlaylistYoutubeService>();
        builder.Services.AddOptions<PlaylistYouTubeOptions>().Configure<IConfiguration>((options, configuration)=> configuration.Bind(options));
    }
}
