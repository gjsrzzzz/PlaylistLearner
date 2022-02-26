using System.ComponentModel;

namespace Jalindi.VideoUtil.Model;
public record Icon(string src, string type, string sizes );
public record Manifest(string name, string short_name, string start_url, string display, string background_color,
    string theme_color,
    bool prefer_releated_applications, Icon[] icons);
