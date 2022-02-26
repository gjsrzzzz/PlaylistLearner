using Jalindi.VideoUtil.Util;

namespace Jalindi.VideoUtil.Model;

public class VideoInfo
{
    public bool Valid { get; init; } = false;
    public string Channel { get; init; } = string.Empty;
    public Description Description { get; init; } = Description.Empty;
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Player { get; init; } = string.Empty;
    public TimeSpan Duration { get; init; } = TimeSpan.Zero;

    public string Dimension { get; init; } = string.Empty;
    public int Width { get; init; } = 0;
    public int Height { get; init; } = 0;
    public AspectRatio AspectRatio { get; init; } = AspectRatio.Unknown;
    public double ActualRatio => (double)(Height * 1000 / Width) / 1000;
    public int Index { get; init; } = 99999;

    public override string ToString()
    {
        return $"{Id}: {Title}";
    }
}