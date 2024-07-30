using System.Text.RegularExpressions;

namespace Jalindi.VideoUtil.Model;

public class TimeCodeMatcher
{
    public bool Success { get; }
    public bool Error { get; }
    public TimeSpan TimeSpan { get; }
    public string Text { get; }

    public TimeCodeMatcher(string item)
    {
        var match = Regex.Match(item, @"^\s*:?(\d+(?=[:\s]))?:?(\d+(?=[:\s]))?:?(\d+(?=[\s]))(.*)$");
        Text = string.Empty;
        Success = match.Success;
        if (!Success) return;
        Text = match.Groups[4].Value.Trim();
        try
        {
            TimeSpan = match.Groups[1].Success switch
            {
                true when match.Groups[2].Success && match.Groups[3].Success => new TimeSpan(
                    int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value)),
                true when !match.Groups[2].Success && match.Groups[3].Success => new TimeSpan(
                    0, int.Parse(match.Groups[1].Value), int.Parse(match.Groups[3].Value)),
                true when match.Groups[2].Success => new TimeSpan(0, int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value)),
                true => new TimeSpan(0, 0, int.Parse(match.Groups[1].Value)),
                _ => System.TimeSpan.MaxValue
            };
        }
        catch (Exception ex)
        {
            Error = true;
        }
    }
}