using Jalindi.VideoUtil.Util;

namespace Jalindi.VideoUtil.Model;

public class VideoInfo
{
    public bool Valid { get; set; } = false;
    public string Channel { get; set; } = string.Empty;
    public Description Description { get; set; } = Description.Empty;
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = TimeSpan.Zero;

    public string Dimension { get; set; } = string.Empty;
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public AspectRatio AspectRatio { get; set; } = AspectRatio.Unknown;
    public double ActualRatio => (double)(Height * 1000 / Width) / 1000;
    public int Index { get; set; } = 99999;

    public override string ToString()
    {
        return $"{Id}: {Title}";
    }
}