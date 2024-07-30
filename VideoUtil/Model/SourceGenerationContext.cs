using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jalindi.VideoUtil.Model;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(PlaylistInfo))]
public partial class SourceGenerationContext  : JsonSerializerContext
{
    
}