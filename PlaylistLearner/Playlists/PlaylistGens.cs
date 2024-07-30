using System.Runtime.CompilerServices;

namespace PlaylistLearner.Playlists;

public partial class PlaylistGens
{
    public string Test2(string name)
    {
        string result = string.Empty;
//        dynamic yy= this;
        GeneratedMethod(name, ref result);
        return result;
    }
    partial void GeneratedMethod(string name, ref string result);
    
    public static string Test(string name)
    {
        string result = string.Empty;
        HelloFrom(name,  ref result);
        
        return result;
    }
    static partial void HelloFrom(string name, ref string result);
}