namespace PlaylistLearner.Components;

public enum PlayerState
{
    Unstarted=-1,
    Ended=0, Playing=1, Paused=2, Buffering=3, VideoCued=4
}

public class VideoData 
{
    public string video_id { get; set; }
    public string author { get; set; }
    public string title { get; set; }
    public string video_quality { get; set; }
}
    
public class PlayerInfo 
{
    public string videoUrl { get; set; }
    public VideoData videoData { get; set; }
}