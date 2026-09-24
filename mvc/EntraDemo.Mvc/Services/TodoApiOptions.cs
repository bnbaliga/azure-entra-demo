namespace EntraDemo.Mvc.Services;

public class TodoApiOptions
{
    public const string SectionName = "TodoApi";

    public string BaseUrl { get; set; } = "http://localhost:5016";

    /// <summary>Delegated scopes to request for the API, e.g. api://&lt;api-client-id&gt;/access_as_user.</summary>
    public string[] Scopes { get; set; } = [];
}
