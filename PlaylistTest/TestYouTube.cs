using System.Threading.Tasks;
using PlaylistLearner;
using PlaylistLearner.Playlists;
using PlaylistYouTube.Service;
using VideoUtil;
using VideoUtil.Model;
using VideoUtil.Util;
using Xunit;
using Xunit.Abstractions;

namespace PlaylistTest;


public class TestYouTube
{
    private readonly IVideoProvider videoProvider;
    private readonly PlaylistService playlistService;
    
    private readonly ITestOutputHelper output;

    public TestYouTube(ITestOutputHelper output, IVideoProvider videoProvider, PlaylistService playlistService)
    {
        this.output = output;
        this.videoProvider=videoProvider;
        this.playlistService = playlistService;
    }


    [Fact]
    public async void TestAccessToVideos()
    {
        await AssessVideo("4G_pZ2kYDJo");
        await AssessVideo("ue-P8DoJ29Q");
        await AssessVideo("xWW7E1OkVU8");
    }
    
    [Fact]
    public async void TestPlayList()
    {
        await AssessPlaylist("PLCuHcnCB_TDmXpp478UcjWkVDsUDNTjCh");
    }
    
    [Fact]
    public async void TestEasyGerman()
    {
        await AssessPlaylist("PLk1fjOl39-50WX8xiXwIBUcbdtMjlaZSj");
    }

    [Fact]
    public async void TestGeorgePetzold()
    {
        const string playListId = "PLCuHcnCB_TDnW4g-8UolN82O5RL2MROUU";
        await AssessPlaylist(playListId);
    }
    
    [Fact]
    public async void TestSourceGen()
    {
        var result = PlaylistGens.Test("hello");
        output.WriteLine(result);
    }

    private async Task AssessPlaylist(string playListId)
    {
        var playlistInfo = await videoProvider.GetPlaylistInfo(playListId);
        output.WriteLine(
            $"Playlist Title: {playlistInfo.Title}\nItems {playlistInfo.VideoIdList.Count}\n{playlistInfo.Description.Brief(50)}");
//        var videosInfo = await youtube.GetVideosInfo(playlistInfo.VideoIdList);
        var playlist = await playlistService.GetPlaylist(playListId);
    }


    private async Task AssessVideo(string videoId)
    {
        var videoInfo = await videoProvider.GetVideoInfo(videoId);
        CheckVideo(videoInfo);
    }

    private void CheckVideo(VideoInfo videoInfo)
    {
        Assert.True(videoInfo.Valid);
        output.WriteLine(
            $"Video Title: {videoInfo.Title}\nSize: {videoInfo.Width} {videoInfo.Height} {videoInfo.AspectRatio.RatioText()} {videoInfo.AspectRatio.Ratio()} {videoInfo.ActualRatio}");
        output.WriteLine($"Duration: {videoInfo.Duration}");
        output.WriteLine($"Description\n{videoInfo.Description.Brief(50)}");
    }
}