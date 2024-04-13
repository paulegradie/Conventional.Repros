using Conventional.Roslyn.Analyzers;
using Conventional.Roslyn.Conventions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SemanticModelNotFound.Tests;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SemanticModelShouldBeFoundAnalyzerConvention : SolutionDiagnosticAnalyzerConventionSpecification
{
    public SemanticModelShouldBeFoundAnalyzerConvention(string[] fileExemptions) : base(fileExemptions)
    {
    }

    protected override DiagnosticResult CheckNode(SyntaxNode node, Document document = null, SemanticModel? semanticModel = null)
    {
        if (semanticModel is null)
        {
            return DiagnosticResult.Failed("Semantic model should not be null here");
        }

        return DiagnosticResult.Succeeded();
    }

    protected override string FailureMessage { get; } = string.Empty;
}