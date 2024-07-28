using System;
using System.Text.Json;
using FluentAssertions;
using Jalindi.VideoUtil.Model;
using Xunit;
using Xunit.Abstractions;

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
        var playlistInfo =  JsonSerializer.Deserialize<PlaylistInfo>(stream,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        playlistInfo.Should().NotBeNull();
        playlistInfo.VideoInfoList.Should().NotBeNull();
        playlistInfo.VideoInfoList.Should().NotBeEmpty();
        foreach (var info in playlistInfo.VideoInfoList)
        {
            output.WriteLine($"{info.Title}");
        }
    }
}