namespace EntraDemo.Mvc.Services;

/// <summary>Detects unconfigured settings so the landing page can explain what to fill in.</summary>
public static class SetupCheck
{
    public static IReadOnlyList<string> MissingSettings(IConfiguration config)
    {
        var required = new Dictionary<string, string?>
        {
            ["AzureAd:TenantId"] = config["AzureAd:TenantId"],
            ["AzureAd:ClientId"] = config["AzureAd:ClientId"],
            ["AzureAd:ClientSecret"] = config["AzureAd:ClientSecret"],
            ["TodoApi:Scopes"] = config["TodoApi:Scopes:0"],
        };

        return required
            .Where(kv => string.IsNullOrWhiteSpace(kv.Value) || kv.Value.Contains('<'))
            .Select(kv => kv.Key)
            .ToList();
    }
}
