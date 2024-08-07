﻿using System.Text;

namespace Jalindi.VideoUtil.Model;

public class Description
{
    public string FullDescription { get;set; }
    public string Remaining { get; set; }
    public int FirstOrder { get; set; }
    public int NextOrder { get; set; }
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

    public List<TimeCode> TimeCodes { get; set;} = [];

    public List<KeyValuePair<string, string>> Tags { get; set;} = [];

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
    
    public Description(string description, TimeSpan duration, int order)
    {
        var inOrder = order;
        FirstOrder = order;
        FullDescription = description;
        TimeCode? lastTimeCode = null;
        var builder = new StringBuilder();
        var reader = new StringReader(description);
        var line = reader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            if (line.Contains("Sacala"))
            {
                int a = 1;
            }
            var match = new TimeCodeMatcher(line);
            if (!match.Success)
            {
                if (TryExtractTag(line, out var tagKey, out var tagValue) && tagKey.Equals("Item"))
                {
                    lastTimeCode = new TimeCode() { Name = tagValue, Order = order, ItemOnly=true };
                    TimeCodes.Add(lastTimeCode);
                    order++;
                }
                else if (lastTimeCode==null) ExtractAndAddTags(line, builder);
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
                        if (key.Equals("Description", StringComparison.OrdinalIgnoreCase))
                        {
                            lastTimeCode.Description = value;
                        }
                        if (key.Equals("Link", StringComparison.OrdinalIgnoreCase))
                        {
                            int comma = value.LastIndexOf(",",StringComparison.InvariantCulture);
                            if (comma > 0)
                            {
                                var timeCode = value.Substring(comma + 1).Trim();
                                value = value.Substring(0, comma).Trim();
                                int dash = timeCode.LastIndexOf("-",StringComparison.InvariantCulture);
                                if (dash > 0)
                                {
                                    var start = new TimeCodeMatcher(timeCode.Substring(0, dash).Trim()+" x");
                                    var end = new TimeCodeMatcher(timeCode.Substring(dash + 1).Trim()+" x");
                                    if (start.Success && end.Success && !start.Error && !end.Error)
                                    {
                                        lastTimeCode.Start = start.TimeSpan;
                                        lastTimeCode.End = end.TimeSpan;
                                    }
                                }
                            }
                            lastTimeCode.AltLink = value;
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
        if (inOrder == order) order++;
        NextOrder = order;
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

    private TimeCode? ExtractTimeCode(TimeSpan duration, TimeCodeMatcher match, StringBuilder builder, string line, TimeCode? lastTimeCode)
    {
        TimeCode? timeCode = null;
        try
        {
            if (match.Error) return timeCode;
            var timeSpan = match.TimeSpan;
            if (timeSpan.Equals(TimeSpan.MaxValue))
            {
                Append(builder, line);
                return null;
            }

            if (lastTimeCode is { ItemOnly: false })
            {
                lastTimeCode.End = timeSpan;
            }

            if (match.Text.Equals("End", StringComparison.OrdinalIgnoreCase)) return null;
            timeCode = new TimeCode { Start = timeSpan, End = duration, Name = match.Text };
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
        var values = new List<string>();
        tagName = tagName.Replace(" ", "");
        foreach (var pair in Tags.Where(pair => pair.Key.Replace(" ", "").Equals(tagName, StringComparison.OrdinalIgnoreCase)))
        {
            var tagValues = pair.Value.Trim();
            foreach (var tagValue in tagValues.Split('|'))
            {
                values.Add(tagValue);
            }
        }
        return values.Count==0?null:values.ToArray();
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
    public bool ItemOnly { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public bool Ignore { get; set; }
    public string AltName { get; set; }
    public string Description { get; set; }
    public string Lesson { get; set; }
    public string AltLink { get; set; }

    public override string ToString()
    {
        return $"{Start}-{End} {Name}";
    }
}