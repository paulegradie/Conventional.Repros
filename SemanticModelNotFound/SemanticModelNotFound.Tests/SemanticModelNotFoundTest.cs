using Conventional;
using Conventional.Roslyn;
using SemanticModelOrigin;

namespace SemanticModelNotFound.Tests;

public class Reproductions
{
    /// <summary>
    /// This result set leads me to believe that the `TryGetSemanticModel` does not function as expected.
    /// Replacing it with `GetSemanticModelAsync` seems to resolve this from my testing here
    /// </summary>
    [Test]
    public async Task GetSemanticModelAsyncWorks_ShouldPass()
    {
        ConventionConfiguration.DefaultFailureAssertionCallback = Assert.Fail;
        ConventionConfiguration.DefaultWarningAssertionCallback = Assert.Inconclusive;
        var models = await new GetSemanticModelFromADifferentProject().UseGetSemanticModelAsync();
        if (models.Count == 0 || models.Any(x => x is null)) Assert.Fail();
    }

    [Test]
    public async Task TryGetSemanticModelDoesNotWork_ShouldFail()
    {
        ConventionConfiguration.DefaultFailureAssertionCallback = Assert.Fail;
        ConventionConfiguration.DefaultWarningAssertionCallback = Assert.Inconclusive;
        var models = await new GetSemanticModelFromADifferentProject().UseTryGetSemanticModel();
        if (models.Count == 0 || models.Any(x => x is null)) Assert.Fail();
    }

    [Test]
    public void ConventionalDoesNotFindSemanticModels_ShouldFail()
    {
        ConventionConfiguration.DefaultFailureAssertionCallback = Assert.Fail;
        ConventionConfiguration.DefaultWarningAssertionCallback = Assert.Inconclusive;
        ThisCodebase.MustConformTo(new SemanticModelShouldBeFoundAnalyzerConvention([]));
    }
}