
var tag = document.createElement('script');
tag.id = 'iframe-youtube-script';
tag.src = 'https://www.youtube.com/iframe_api';
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var player;
var videoPlayer;
var playerX;
var playerHolder;
var muted=false;
var isSlow=false;
var lastMuted=false;

function poll( )
{
    if (!isSlow && player!==undefined && player.isMuted!==undefined && player.isMuted()!==undefined) {
        muted = player.isMuted();
    }
    
    if (videoPlayer!==undefined) {
        if (lastMuted!==muted) {
//            console.log("Sending muted= "+muted);
            videoPlayer.invokeMethodAsync('OnMuteChange', muted);
            lastMuted=muted;
        }
    }
    setTimeout(poll, 500);
}

function onYouTubeIframeAPIReady() {
    console.log("Video Initializing "+window.location);
    playerX = document.getElementById('player-div');
    playerHolder = document.getElementById('player-holder');

    player = new YT.Player('youtube-iframe', {
        height: '100%',
        width: '100%',
        videoId: 'pgd4jcPJHow',
        playerVars: {
            'playsinline': 1,
            'origin': window.location,
            'autoplay': 1,
            'controls':1,
            'modestbranding':1,
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
    var youTubeIFrame = document.getElementById('youtube-iframe');
    youTubeIFrame.style.position='absolute';
    console.log("Video Control ready "+JSON.stringify(player));
    setTimeout(poll, 500);
}

function closeVideo() {

}

function alignVideo()
{
    var iframeElement = document.getElementById('youtube-iframe-container');
    if (iframeElement !== undefined) {
        var playerXRect = playerX.getBoundingClientRect();
        console.log("player rect", playerXRect.width, playerXRect.height);
        iframeElement.style.width=playerXRect.width+"px";
        iframeElement.style.height=playerXRect.height+"px";

        var rect = iframeElement.parentElement.getBoundingClientRect();
        console.log(rect.top, rect.right, rect.bottom, rect.left);
        var bodyRect = document.body.getBoundingClientRect();
        var offsetTop = rect.top - bodyRect.top;
        var offsetLeft = rect.left - bodyRect.left;
        console.log(bodyRect.top, bodyRect.right, bodyRect.bottom, bodyRect.left);
        playerHolder.style.top = offsetTop + "px";
//        playerHolder.style.left=offsetLeft+"px";
//        playerHolder.style.width=rect2.width+"px";
        playerHolder.style.display = null;
    }
}

function prepareVideo(dotnetRef, paddingBottom, videoId, startSeconds, endSeconds) {
    videoPlayer=dotnetRef;
    playerX.style.paddingBottom=paddingBottom;
    alignVideo();
    loadVideoById(videoId, startSeconds, endSeconds);
}

function loadVideoById(videoId,
                       startSeconds, endSeconds)
{
    console.log("loading video ",startSeconds, endSeconds);
    if (endSeconds>0)
    {
        loadVideoById({'videoId': videoId,
            'startSeconds': startSeconds,
            'endSeconds': endSeconds});
    }
    else {
        player.loadVideoById(videoId, startSeconds);
    }
}

function playVideo()
{
    player.playVideo();
}
function pauseVideo()
{
    player.pauseVideo();
}
function stopVideo()
{
    player.stopVideo();
}
function muteVideo()
{
    player.mute();
    muted=true;
}
function unMuteVideo()
{
    player.unMute();
    muted=false;
}
function backVideo()
{
    var time=player.getCurrentTime()-10;
    if (time<0) time=0;
    player.seekTo(time, true);
}

function forwardVideo()
{
    var time=player.getCurrentTime()+10;
    if (time>=player.getDuration()) time=player.getDuration();
    player.seekTo(time, true);
}
function slowSpeed()
{
    muted=player.isMuted();
    player.mute();
    player.setPlaybackRate(0.25);
    isSlow=true;
}
function mediumSpeed()
{
    player.mute();
    player.setPlaybackRate(0.5);
    isSlow=true;
}
function normalSpeed()
{
    if (!muted)
    {
        unMuteVideo();
    }
    player.setPlaybackRate(1);
    isSlow=false;
    muted = player.isMuted();
}
function seekTo(seconds, allowSeekAhead)
{
    player.seekTo(seconds, allowSeekAhead);
}

function onPlayerReady(event) {
    muteVideo();
    console.log("Player ready");
}

function onPlayerStateChange(event) {
    console.log("Player State Change "+event.data);
    if (videoPlayer) {
        videoPlayer.invokeMethodAsync('OnPlayerStateChange', event);
    }
}
