using System;
using System.Text.Json;
using FluentAssertions;
using Jalindi.VideoUtil.Model;
using Xunit;

namespace PlaylistTest;

public class TestAppData(ITestOutputHelper output)
{
    private const string AppDataResource = "PlaylistTest.Resource.ApiData.json";
    [Fact]
    public void GetAppData()
    {
        foreach (var file in this.GetType().Assembly.GetManifestResourceNames())
        {
            output.WriteLine($"{file}");
        }

        GetType().Assembly.GetManifestResourceStream(AppDataResource).Should().NotBeNull();
    }
    
    [Fact]
    public void ReadAppData()
    {
        var stream = GetType().Assembly.GetManifestResourceStream(AppDataResource);
        stream.Should().NotBeNull();
        var myOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var other = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var playlistInfo =  JsonSerializer.Deserialize<PlaylistInfo>(stream
            , SourceGenerationContext.Default.PlaylistInfo);
        playlistInfo.Should().NotBeNull();
        playlistInfo.VideoInfoList.Should().NotBeNull();
        playlistInfo.VideoInfoList.Should().NotBeEmpty();
        foreach (var info in playlistInfo.VideoInfoList)
        {
            output.WriteLine($"{info.Title}");
        }
    }
}