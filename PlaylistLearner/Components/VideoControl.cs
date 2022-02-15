using System.Text.Json;
using Microsoft.JSInterop;

namespace PlaylistLearner.Components;

public class VideoControl
{
    private IJSRuntime JsRuntime { get; set; }
    public VideoControl(IJSRuntime ijsRuntime)
    {
        JsRuntime = ijsRuntime;
    }
    public async Task  Play()
    {
        await JsRuntime.InvokeVoidAsync("playVideo");
    }
    
    public async Task Close()
    {
        await JsRuntime.InvokeVoidAsync("closeVideo");
    }
    
    public async Task  Pause()
    {
        await JsRuntime.InvokeVoidAsync("pauseVideo");
    }
    
    public async Task  Back()
    {
        await JsRuntime.InvokeVoidAsync("backVideo");
    }
    
    public async Task  Forward()
    {
        await JsRuntime.InvokeVoidAsync("forwardVideo");
    }
    
    public async Task  Slow()
    {
        await JsRuntime.InvokeVoidAsync("slowSpeed");
    }
    
    public async Task  Mute()
    {
        await JsRuntime.InvokeVoidAsync("muteVideo");
    }
    
    public async Task  Unmute()
    {
        await JsRuntime.InvokeVoidAsync("unMuteVideo");
    }

    
    public async Task  UnMute()
    {
        await JsRuntime.InvokeVoidAsync("unMuteVideo");
    }
    
    public async Task  Medium()
    {
        await JsRuntime.InvokeVoidAsync("mediumSpeed");
    }
    
    public async Task  Normal()
    {
        await JsRuntime.InvokeVoidAsync("normalSpeed");
    }
    
    public async Task  SeekTo(int seconds, bool allowSeekAhead=true)
    {
        await JsRuntime.InvokeVoidAsync("seekTo", seconds, allowSeekAhead);
    }

    public async Task PrepareVideo(VideoPlayer videoPlayer, string paddingPercent, string videoId)
    {
        var dotNetObjectReference = DotNetObjectReference.Create(videoPlayer);
        await JsRuntime.InvokeVoidAsync("prepareVideo", dotNetObjectReference,paddingPercent, videoId);
    }

    public async Task PrepareVideo(VideoPlayer videoPlayer, string paddingPercent, string videoId, int itemStart, int itemEnd)
    {
        var dotNetObjectReference = DotNetObjectReference.Create(videoPlayer);
        await JsRuntime.InvokeVoidAsync("prepareVideo", dotNetObjectReference,paddingPercent, videoId,itemStart, itemEnd);
    }

    private static PlayerInfo? GetPlayerInfo(JsonElement eventData)
    {
        var info= eventData.GetProperty("target").GetProperty("playerInfo").Deserialize<PlayerInfo>();
        return info;
    }
    
    public bool TryGetPlayerState(JsonElement eventData, out PlayerState state)
    {
        state = PlayerState.Unstarted;
        var text = eventData.GetRawText();
        var playerInfo=GetPlayerInfo(eventData);
        if (!eventData.GetProperty("data").TryGetInt32(out var data)) return false;
        state = (PlayerState)Enum.ToObject(typeof(PlayerState), data);
        return true;
    }


}