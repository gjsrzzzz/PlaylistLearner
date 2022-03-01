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
        int order = 0;
        var reader = new StringReader(description);
        var line = reader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            var match = Regex.Match(line, @"^\s*:?(\d+(?=[:\s]))?:?(\d+(?=[:\s]))?:?(\d+(?=[\s]))(.*)$");
            if (!match.Success)
            {
                if (lastTimeCode==null) ExtractAndAddTags(line, builder);
                else
                {
                    if (TryExtractTag(line, out var key, out var value))
                    {
                        if (key.Equals("Alt", StringComparison.OrdinalIgnoreCase))
                        {
                            lastTimeCode.AltName = value;
                        }
                        if (key.Equals("Key", StringComparison.OrdinalIgnoreCase))
                        {
                            lastTimeCode.Key = value;
                        }
                        if (key.Equals("Lesson", StringComparison.OrdinalIgnoreCase))
                        {
                            lastTimeCode.Lesson = value;
                        }
                        if (key.Equals("Ignore", StringComparison.OrdinalIgnoreCase))
                        {
                            lastTimeCode.Ignore = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                        if (key.Equals("Order", StringComparison.OrdinalIgnoreCase) &&
                            int.TryParse(value, out var newOrder))
                        {
                            lastTimeCode.Order = newOrder;
                            order = newOrder + 1;
                        }
                    }
                    else
                    {
                        lastTimeCode.Description = line.Trim();
                    }
 //                   int dash = line.IndexOf('-');
 //                   lastTimeCode.Extra = dash > 0 ? line.Substring(dash+1).Trim() : string.Empty;
                }
                line = reader.ReadLine();
                continue;
            }

            if (line.Contains("Guapea"))
            {
                int a = 1;
            }
            lastTimeCode = ExtractTimeCode(duration, match, builder, line, lastTimeCode);
            if (lastTimeCode != null) //ExtractAndAddTags(line, builder);
          //  else
            {
                lastTimeCode.Order = order;
                order++;
            }
            line = reader.ReadLine();
        }

        Remaining = builder.ToString();
    }

    private void ExtractAndAddTags(string line, StringBuilder builder)
    {
        if (TryExtractTag(line, out var key, out var value))
        {
            Tags.Add(new KeyValuePair<string, string>(key, value));
            return;
        }
        Append(builder, line);
    }

    private bool TryExtractTag(string line, out string key, out string value)
    {
        int colon = line.IndexOf(":", StringComparison.OrdinalIgnoreCase);
        if (colon > 0)
        {
            key = line.Substring(0, colon).Trim();
            value = line.Substring(colon + 1).Trim();
            return true;
        }

        key=string.Empty;
        value = string.Empty;
        return false;
    }

    private TimeCode? ExtractTimeCode(TimeSpan duration, Match match, StringBuilder builder, string line, TimeCode? lastTimeCode)
    {
        var text = match.Groups[4].Value.Trim();
        TimeCode? timeCode = null;
        try
        {
            var timeSpan = match.Groups[1].Success switch
            {
                true when match.Groups[2].Success && match.Groups[3].Success => new TimeSpan(
                    int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value)),
                true when !match.Groups[2].Success && match.Groups[3].Success => new TimeSpan(
                    0, int.Parse(match.Groups[1].Value),int.Parse(match.Groups[3].Value)),
                true when match.Groups[2].Success => new TimeSpan(0, int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value)),
                true => new TimeSpan(0, 0, int.Parse(match.Groups[1].Value)),
                _ => TimeSpan.MaxValue
            };
            if (timeSpan.Equals(TimeSpan.MaxValue))
            {
                Append(builder, line);
                return null;
            }

            if (lastTimeCode != null)
            {
                lastTimeCode.End = timeSpan;
            }

            if (text.Equals("End", StringComparison.OrdinalIgnoreCase)) return null;
            timeCode = new TimeCode { Start = timeSpan, End = duration, Name = text };
            TimeCodes.Add(timeCode);
        }
        catch (Exception e)
        {
        }

        return timeCode;
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
    
    public string[]? GetStringsTag(string tagName)
    {
        return GetStringTag(tagName)?.Split('|');
    }
    
    public TEnum GetEnumTag<TEnum>(string tagName, TEnum defaultValue)  where TEnum : struct
    {
        if (TryGetTag(tagName, out var tagValue))
        {
            tagValue = tagValue.Replace(" ", "");
            if (Enum.TryParse<TEnum>(tagValue, out var tagEnum))
            {
                return tagEnum;
            }
        }
        return defaultValue;
    }

    private bool TryGetTag(string tagName, out string tagValue)
    {
        tagName = tagName.Replace(" ", "");
        tagValue = string.Empty;
        foreach (var pair in Tags.Where(pair => pair.Key.Replace(" ", "").Equals(tagName, StringComparison.OrdinalIgnoreCase)))
        {
            tagValue = pair.Value.Trim();
            return true;
        }
        return false;
    }


}

public class TimeCode
{
    public int Order { get; set; } = 0;
    public TimeSpan Start { get; init; }
    public TimeSpan End { get; set; }
    public string Name { get; init; }
    public string Key { get; set; }
    public bool Ignore { get; set; }
    public string AltName { get; set; }
    public string Description { get; set; }
    public string Lesson { get; set; }

    public override string ToString()
    {
        return $"{Start}-{End} {Name}";
    }
}