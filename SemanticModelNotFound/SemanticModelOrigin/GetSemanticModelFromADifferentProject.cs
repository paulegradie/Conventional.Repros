using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace SemanticModelOrigin;

public class GetSemanticModelFromADifferentProject
{
    public async Task<List<SemanticModel>> UseTryGetSemanticModel()
    {
        var soln = await GetSolution();
        var models = new List<SemanticModel>();
        foreach (var proj in soln.Projects)
        {
            foreach (var document in proj.Documents)
            {
                if (!document.TryGetSemanticModel(out var model)) continue;
                models.Add(model);
            }
        }

        return models;
    }

    public async Task<List<SemanticModel>> UseGetSemanticModelAsync()
    {
        var soln = await GetSolution();
        var models = new List<SemanticModel>();
        foreach (var proj in soln.Projects)
        {
            foreach (var document in proj.Documents)
            {
                var model = document.GetSemanticModelAsync().Result;
                if (model is null) continue;
                models.Add(model);
            }
        }

        return models;
    }

    private static async Task<Solution> GetSolution()
    {
        if (!MSBuildLocator.IsRegistered)
        {
            MSBuildLocator.RegisterDefaults();
        }

        var workspace = MSBuildWorkspace.Create();
        var soln = await workspace.OpenSolutionAsync(@"G:\code\Conventional.Repros\SemanticModelNotFound\SemanticModelNotFound.sln");
        return soln;
    }
}