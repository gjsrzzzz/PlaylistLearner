using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlaylistLearner;
using PlaylistLearner.Components;
using PlaylistLearner.Model;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<VideoControl>();
builder.Services.AddSingleton<PlaylistService>();
builder.Services.Configure<PlaylistOptions>(options=> builder.Configuration.Bind(options));

await builder.Build().RunAsync();