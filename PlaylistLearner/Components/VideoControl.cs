using System.Text.Json;
using Microsoft.JSInterop;

namespace PlaylistLearner.Components;

public class VideoControl
{
    private IJSRuntime JsRuntime { get; set; }
    private VideoPlayer? VideoPlayer { get; set; } = null;
    private bool PlayerReady { get; set; } = false;
    private string? CurrentVideoId { get; set; }
    public bool Muted { get; set; } = false;
    public double Time { get; set; } = 0;
    public PlayerState State { get; set; } = PlayerState.Unstarted;
    public bool Ready { get; set; } = false;
    public bool HasStartedPlaying { get; set; } = false;
    public double ItemStart = 0;

    public VideoControl(IJSRuntime ijsRuntime)
    {
        JsRuntime = ijsRuntime;
        var dotNetObjectReference = DotNetObjectReference.Create(this);
        JsRuntime.InvokeVoidAsync("videoControlReady", dotNetObjectReference);
    }
    public async Task Play()
    {
        await JsRuntime.InvokeVoidAsync("playVideo");
    }
    
    public async Task RePlay()
    {
        await SeekTo(ItemStart);
        await Play();
    }
    
    public async Task Close()
    {
        await JsRuntime.InvokeVoidAsync("closeVideo");
        VideoPlayer = null;
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
    
    public async Task  SeekTo(double seconds, bool allowSeekAhead=true)
    {
        await JsRuntime.InvokeVoidAsync("seekTo", seconds, allowSeekAhead);
    }

    public async Task PrepareVideo(VideoPlayer videoPlayer, string paddingPercent, string videoId)
    {
        VideoPlayer = videoPlayer;
        await JsRuntime.InvokeVoidAsync("prepareVideo", paddingPercent, videoId);
        CurrentVideoId = videoId;
        ItemStart = 0;
    }

    public async Task PrepareVideo(VideoPlayer videoPlayer, string paddingPercent, string videoId, double itemStart, double itemEnd)
    {
        VideoPlayer = videoPlayer;
        await JsRuntime.InvokeVoidAsync("prepareVideo", paddingPercent, videoId,itemStart);//, itemEnd);
        CurrentVideoId = videoId;
        ItemStart = itemStart;
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

    [JSInvokable] 
    public async Task OnPlayerReady()
    {
        PlayerReady = true;
        await Console.Out.WriteLineAsync("Player is Ready");
    }
    
    [JSInvokable] // This is required in order to JS be able to execute it
    public void OnMuteChange(bool muted)
    {
        Muted = muted;  
        VideoPlayer?.Refresh();
    }
    
    [JSInvokable] // This is required in order to JS be able to execute it
    public async Task OnTimeChange(double time)
    {
        Time = time;
        VideoPlayer?.OnTimeChange();
    }
    
    [JSInvokable] // This is required in order to JS be able to execute it
    public async Task OnPlayerStateChange(JsonElement eventData)
    {
        if (!TryGetPlayerState(eventData, out var state)) return;
        State = state;
        await Console.Out.WriteLineAsync($"Player State Change {State}");
        if (State != PlayerState.Unstarted)
        {
            Ready = PlayerReady;
        }
        if (State == PlayerState.Playing)
        {
            HasStartedPlaying = true;
        }
        VideoPlayer?.OnPlayerStateChange();
    }
}