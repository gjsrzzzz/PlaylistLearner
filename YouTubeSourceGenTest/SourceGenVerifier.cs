using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace YouTubeSourceGenTest;

public class SourceGenVerifier<TSourceGenerator> : IDisposable where TSourceGenerator : ISourceGenerator, new()
{
    public class Test : CSharpSourceGeneratorTest<TSourceGenerator, XUnitVerifier>
    {
        public Test()
        {
        }

        protected override CompilationOptions CreateCompilationOptions()
        {
            return new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithNullableContextOptions(NullableContextOptions.Enable)
                .WithSpecificDiagnosticOptions(GetNullableWarningsFromCompiler());
        }

        public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.CSharp13;

        private static ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
        {
            return ImmutableDictionary<string, ReportDiagnostic>.Empty
                .Add("CS8600", ReportDiagnostic.Warn) // Converting null literal or possible null value to non-nullable type
                .Add("CS8602", ReportDiagnostic.Warn) // Dereference of a possibly null reference
                .Add("CS8603", ReportDiagnostic.Warn) // Possible null reference return
                .Add("CS8604", ReportDiagnostic.Warn); // Possible null reference argument
        }

        protected override ParseOptions CreateParseOptions()
        {
            return CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion);
        }
    }

    public void Dispose()
    {
        // Clean up resources if needed
    }
}