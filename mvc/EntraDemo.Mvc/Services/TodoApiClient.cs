using System.Net.Http.Headers;
using EntraDemo.Mvc.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Abstractions;

namespace EntraDemo.Mvc.Services;

public class ApiException(int statusCode, string message) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}

/// <summary>
/// Calls EntraDemo.Api on behalf of the signed-in user. Access tokens are acquired
/// server-side by Microsoft.Identity.Web (using the app's client secret and the
/// user's cached refresh token) and sent as "Authorization: Bearer ...". Tokens never
/// reach the browser.
/// </summary>
public class TodoApiClient(
    HttpClient http,
    IAuthorizationHeaderProvider authorizationHeaderProvider,
    IOptions<TodoApiOptions> options)
{
    public Task<Me> GetMeAsync() => SendAsync<Me>(HttpMethod.Get, "api/me");

    public Task<List<Todo>> GetTodosAsync() => SendAsync<List<Todo>>(HttpMethod.Get, "api/todos");

    public Task<Todo> AddTodoAsync(string title) =>
        SendAsync<Todo>(HttpMethod.Post, "api/todos", new { title });

    public Task<Todo> SetDoneAsync(Guid id, bool done) =>
        SendAsync<Todo>(HttpMethod.Patch, $"api/todos/{id}", new { done });

    public Task DeleteTodoAsync(Guid id) => SendAsync<object>(HttpMethod.Delete, $"api/todos/{id}");

    /// <summary>Calls the API with no Authorization header, to demonstrate that it is rejected.</summary>
    public async Task<string> CallAnonymouslyAsync(string path)
    {
        using var response = await http.GetAsync(path.TrimStart('/'));
        var challenge = response.Headers.WwwAuthenticate.ToString();
        return $"{(int)response.StatusCode} {response.ReasonPhrase}" +
            (string.IsNullOrEmpty(challenge) ? "" : $": {challenge}");
    }

    private async Task<T> SendAsync<T>(HttpMethod method, string path, object? body = null)
    {
        using var request = new HttpRequestMessage(method, path);
        if (body is not null)
        {
            request.Content = JsonContent.Create(body);
        }

        // Returns "Bearer <token>" from the token cache, refreshing it if needed. Throws
        // MicrosoftIdentityWebChallengeUserException when the user must sign in again;
        // [AuthorizeForScopes] on the controller turns that into a sign-in redirect.
        var authorization = await authorizationHeaderProvider.CreateAuthorizationHeaderForUserAsync(options.Value.Scopes);
        request.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization);

        using var response = await http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var challenge = response.Headers.WwwAuthenticate.ToString();
            var detail = string.IsNullOrEmpty(challenge) ? await response.Content.ReadAsStringAsync() : challenge;
            throw new ApiException((int)response.StatusCode, $"{(int)response.StatusCode} {response.ReasonPhrase}: {detail}");
        }

        return response.Content.Headers.ContentLength == 0 || response.StatusCode == System.Net.HttpStatusCode.NoContent
            ? default!
            : (await response.Content.ReadFromJsonAsync<T>())!;
    }
}
