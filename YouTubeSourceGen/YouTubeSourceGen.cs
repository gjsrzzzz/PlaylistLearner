using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace YouTubeSourceGen;

[Generator]
public class YouTubeSourceGen : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        // Find the main method
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
        if(context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.PlaylistId", out var playlistId))
        {
        }
        
        // Build up the source code
        string source = $@" // Auto-generated code
using System;

namespace PlaylistLearner.Playlists
{{
    public partial class PlaylistGens
    {{
        static partial void HelloFrom(string name, ref string result) =>
            result= $""Generator x says: Hi from '{{name}}' {playlistId}"";
        partial void GeneratedMethod(string name, ref string result) =>
            result= $""Generator x says: Hi from '{{name}}' {playlistId}"";
    }}
}}
";
        var typeName = mainMethod.ContainingType.Name;

        // Add the source code to the compilation
        context.AddSource($"{typeName}.g.cs", source);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }
}

class MySyntaxReceiver : ISyntaxReceiver
{
    public ClassDeclarationSyntax ClassToAugment { get; private set; }

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // Business logic to decide what we're interested in goes here
        if (syntaxNode is ClassDeclarationSyntax cds &&
            cds.Identifier.ValueText == "UserClass")
        {
            ClassToAugment = cds;
        }
    }
}