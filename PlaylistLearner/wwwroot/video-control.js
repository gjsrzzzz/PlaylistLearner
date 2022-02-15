
var tag = document.createElement('script');
tag.id = 'iframe-youtube-script';
tag.src = 'https://www.youtube.com/iframe_api';
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var player;
var videoPlayer;
var playerX;
var playerHolder;

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
            'modestbranding':1
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
    var youTubeIFrame = document.getElementById('youtube-iframe');
    youTubeIFrame.style.position='absolute';
    console.log("Video Control ready "+JSON.stringify(player));
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

function prepareVideo(dotnetRef, paddingBottom) {
    videoPlayer=dotnetRef;
    playerX.style.paddingBottom=paddingBottom;
    alignVideo()

//        player.seekTo(0, true);

//        parent.insertBefore(playerX, iframeElement);

//        console.log("element "+JSON.stringify(element)+' ' +element);
    /*       player = new YT.Player('youtube-iframe', {
               playerVars: {
                   'playsinline': 1
               },
               events: {
                   'onReady': onPlayerReady,
                   'onStateChange': onPlayerStateChange
               }
           });*/
//        console.log("Created player "+JSON.stringify(player));
}

function loadVideoById(videoId,
                       startSeconds)
{
    console.log("loading video "+JSON.stringify(player));
    player.loadVideoById(videoId, startSeconds);
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
}
function unMuteVideo()
{
    player.unMute();
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
    player.setPlaybackRate(0.25);
}
function mediumSpeed()
{
    player.setPlaybackRate(0.5);
}
function normalSpeed()
{
    player.setPlaybackRate(1);
}
function seekTo(seconds, allowSeekAhead)
{
    player.seekTo(seconds, allowSeekAhead);
}

function onPlayerReady(event) {
    if (videoPlayer) {
//        console.log("Player ready "+JSON.stringify(event));
        videoPlayer.invokeMethodAsync('OnPlayerReady', event);
    }
}

function onPlayerStateChange(event) {
    console.log("Player State Change "+event.data);
    if (videoPlayer) {
        videoPlayer.invokeMethodAsync('OnPlayerStateChange', event);
    }
}
