
using System.Reflection;
using Jalindi.VideoUtil;
using Jalindi.VideoUtil.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaylistYouTube.Service;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        
    })
    .ConfigureServices(services =>
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("local.setting.json", optional: true, reloadOnChange: true);
        builder.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        builder.AddEnvironmentVariables();
        var configuration = builder.Build();
        services.AddSingleton<IVideoProvider, PlaylistYoutubeService>();
        services.Configure<PlaylistYouTubeOptions>(options =>
        {
            configuration.Bind(options);
            configuration.GetSection("Value").Bind(options);
        });
    }).Build();

host.Run();