using System.Text;
using System.Text.RegularExpressions;

namespace VideoUtil.Model;

public class Description
{
    public string FullDescription { get; }
    public string Remaining { get; }
    public string Brief(int maxLength)
    {
        return (FullDescription.Length > maxLength
            ? FullDescription.Substring(0, maxLength)
            : FullDescription);
    }

    public List<TimeCode> TimeCodes { get; } = new();

    public List<KeyValuePair<string, string>> Tags { get; } = new();

    public static readonly Description Empty = new();

    private Description()
    {
        FullDescription = string.Empty;
    }

    public Description(string description, TimeSpan duration)
    {
        FullDescription = description;
        TimeCode? lastTimeCode = null;
        var builder = new StringBuilder();
        foreach (var line in description.Split('\n'))
        {
            var match = Regex.Match(line, @"^\s:?(\d+)?:?(\d+)?:?(\d+).*$");
            if (!match.Success)
            {
                ExtractTags(line, builder);
                continue;
            }

            lastTimeCode = ExtractTimeCodes(duration, match, builder, line, lastTimeCode);
            if (lastTimeCode==null) ExtractTags(line, builder);
        }

        Remaining = builder.ToString();
    }

    private void ExtractTags(string line, StringBuilder builder)
    {
        int colon = line.IndexOf(":", StringComparison.OrdinalIgnoreCase);
        if (colon > 0)
        {
            var key = line.Substring(0,colon);
            var value = line.Substring(colon + 1);
            Tags.Add(new KeyValuePair<string, string>(key, value));
            return;
        }
        builder.AppendLine(line);
    }

    private TimeCode? ExtractTimeCodes(TimeSpan duration, Match match, StringBuilder builder, string line,
        TimeCode? lastTimeCode)
    {
        var text = match.Groups[4].Value.Trim();
        try
        {
            var timeSpan = match.Groups[1].Success switch
            {
                true when match.Groups[2].Success && match.Groups[3].Success => new TimeSpan(
                    int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value)),
                true when match.Groups[2].Success => new TimeSpan(0, int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value)),
                true => new TimeSpan(0, 0, int.Parse(match.Groups[1].Value)),
                _ => TimeSpan.Zero
            };
            if (timeSpan.Equals(TimeSpan.Zero))
            {
                builder.AppendLine(line);
                return lastTimeCode;
            }

            if (lastTimeCode != null)
            {
                lastTimeCode.End = timeSpan;
            }

            if (text.Equals("End", StringComparison.OrdinalIgnoreCase)) return lastTimeCode;
            lastTimeCode = new TimeCode { Start = timeSpan, End = duration, Name = text };
            TimeCodes.Add(lastTimeCode);
        }
        catch (Exception e)
        {
        }

        return lastTimeCode;
    }
}

public class TimeCode
{
    public TimeSpan Start { get; init; }
    public TimeSpan End { get; set; }
    public string Name { get; init; }
    public string Extra { get; init; }

    public override string ToString()
    {
        return $"{Start}-{End} {Name}";
    }
}