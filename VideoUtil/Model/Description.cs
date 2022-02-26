using System.Text;
using System.Text.RegularExpressions;

namespace Jalindi.VideoUtil.Model;

public class Description
{
    public string FullDescription { get;set; }
    public string Remaining { get; set; }
    public string Brief(int maxLength)
    {
        return (FullDescription.Length > maxLength
            ? FullDescription.Substring(0, maxLength)
            : FullDescription);
    }
    public string BriefRemaining(int maxLength)
    {
        return (Remaining.Length > maxLength
            ? Remaining.Substring(0, maxLength)
            : Remaining);
    }

    public List<TimeCode> TimeCodes { get; set;} = new();

    public List<KeyValuePair<string, string>> Tags { get; set;} = new();

    public static readonly Description Empty = new();

    public Description()
    {
        FullDescription = string.Empty;
    }

    private static void Append(StringBuilder builder, string value)
    {
        if (builder.Length > 0) builder.AppendLine("");
        builder.Append(value);
    }
    
    public Description(string description, TimeSpan duration)
    {
        FullDescription = description;
        TimeCode? lastTimeCode = null;
        var builder = new StringBuilder();
        var reader = new StringReader(description);
        var line = reader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            var match = Regex.Match(line, @"^\s:?(\d+)?:?(\d+)?:?(\d+).*$");
            if (!match.Success)
            {
                ExtractTags(line, builder);
                line = reader.ReadLine();
                continue;
            }

            lastTimeCode = ExtractTimeCodes(duration, match, builder, line, lastTimeCode);
            if (lastTimeCode==null) ExtractTags(line, builder);
            line = reader.ReadLine();
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
        Append(builder, line);
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
                Append(builder, line);
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
    
    public bool GetBooleanTag(string tagName)
    {
        return TryGetTag(tagName, out var tagValue) && tagValue.Equals("true", StringComparison.OrdinalIgnoreCase);
    }
    
    public string? GetStringTag(string tagName)
    {
        if (TryGetTag(tagName, out var tagValue)) return tagValue;
        return null;
    }

    private bool TryGetTag(string tagName, out string tagValue)
    {
        tagValue = string.Empty;
        foreach (var pair in Tags.Where(pair => pair.Key.Equals(tagName, StringComparison.OrdinalIgnoreCase)))
        {
            tagValue = pair.Value.Trim();
            return true;
        }
        return false;
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