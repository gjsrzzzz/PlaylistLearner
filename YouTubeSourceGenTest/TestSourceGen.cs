using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using Xunit;
using YouTubeSourceGen;

namespace YouTubeSourceGenTest;

public class TestSourceGen
{
    [Fact]
    public void TestGenerator()
    {
        var code = string.Empty;
        var generatedCode = @"namespace HelloGenerated
{
  public class HelloGenerator
  {
    public static void Test() => System.Console.WriteLine(""Hello Generator"");
  }
}";
        var tester=new CSharpSourceGeneratorTest<YouTubeSourceGen.YouTubeSourceGen, XUnitVerifier>()
        {
            TestState = 
            {
                Sources = { code },
                GeneratedSources =
                {
                    (typeof(YouTubeSourceGen.YouTubeSourceGen), "GeneratedFileName", SourceText.From(generatedCode, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                },
            },
        }.RunAsync();
    }

}