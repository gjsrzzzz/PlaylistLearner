using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaylistLearner;
using PlaylistLearner.Model;
using PlaylistYouTube.Service;

namespace PlaylistTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(typeof(Startup).Assembly).Build();
        
        services.AddSingleton<IVideoProvider, PlaylistYoutubeService>();
        services.AddSingleton<PlaylistService>();
        services.Configure<PlaylistOptions>(options=> configuration.Bind(options));
        services.Configure<PlaylistYouTubeOptions>(options=> configuration.Bind(options));
    }
}