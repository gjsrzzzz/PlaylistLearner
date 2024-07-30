namespace Jalindi.VideoUtil.Model;
public record Icon(string src, string type, string sizes );
public record HashValue(string value );

public record Manifest(string name, string short_name, string start_url, string display, string background_color,
    string theme_color,
    bool prefer_releated_applications, Icon[] icons)
{
    public string IconDir {
        get
        {
            if (icons.Length == 0) return string.Empty;
            int slash = icons[0].src.LastIndexOf('/');
            if (slash<0) return string.Empty;
            var path = icons[0].src.Substring(0, slash);
            slash = path.LastIndexOf('/');
            if (slash<0) return string.Empty;
            path = path.Substring(slash + 1);
            return path;
        }
    }
}
