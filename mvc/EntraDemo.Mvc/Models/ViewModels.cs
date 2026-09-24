namespace EntraDemo.Mvc.Models;

public class LandingViewModel
{
    /// <summary>Required settings that are missing or still placeholders.</summary>
    public IReadOnlyList<string> MissingSettings { get; init; } = [];

    /// <summary>Result of calling the API without a token, if the user tried it.</summary>
    public string? ProbeResult { get; init; }
}

public class DashboardViewModel
{
    public Me? Me { get; init; }
    public IReadOnlyList<Todo> Todos { get; init; } = [];
    public string? ProfileError { get; init; }
    public string? TodosError { get; init; }
}

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
