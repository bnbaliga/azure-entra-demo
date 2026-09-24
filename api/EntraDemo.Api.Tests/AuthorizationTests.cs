using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace EntraDemo.Api.Tests;

public class AuthorizationTests(TestAuthFactory factory) : IClassFixture<TestAuthFactory>
{
    private HttpClient ClientWithToken(string? token)
    {
        var client = factory.CreateClient();
        if (token is not null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }

    [Fact]
    public async Task Health_IsPublic()
    {
        var response = await ClientWithToken(null).GetAsync("/api/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/api/me")]
    [InlineData("/api/todos")]
    public async Task ProtectedEndpoints_WithoutToken_Return401(string path)
    {
        var response = await ClientWithToken(null).GetAsync(path);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task MalformedToken_Returns401()
    {
        var response = await ClientWithToken("not-a-jwt").GetAsync("/api/me");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ExpiredToken_Returns401()
    {
        var token = TestAuthFactory.CreateToken(expires: DateTime.UtcNow.AddHours(-1));
        var response = await ClientWithToken(token).GetAsync("/api/me");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TokenForAnotherAudience_Returns401()
    {
        var token = TestAuthFactory.CreateToken(audience: "some-other-api");
        var response = await ClientWithToken(token).GetAsync("/api/me");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TokenWithoutApiScope_IsRejected()
    {
        var token = TestAuthFactory.CreateToken(scope: "User.Read");
        var response = await ClientWithToken(token).GetAsync("/api/me");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task ValidToken_ReturnsCallerIdentity()
    {
        var response = await ClientWithToken(TestAuthFactory.CreateToken()).GetAsync("/api/me");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("Test User", body.GetProperty("name").GetString());
        Assert.Equal("33333333-3333-3333-3333-333333333333", body.GetProperty("objectId").GetString());
    }

    [Fact]
    public async Task Todos_AreIsolatedPerUser()
    {
        var alice = ClientWithToken(TestAuthFactory.CreateToken(objectId: Guid.NewGuid().ToString()));
        var bob = ClientWithToken(TestAuthFactory.CreateToken(objectId: Guid.NewGuid().ToString()));

        var created = await alice.PostAsJsonAsync("/api/todos", new { title = "Alice's task" });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        var id = (await created.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("id").GetString();

        var aliceTodos = await alice.GetFromJsonAsync<JsonElement>("/api/todos");
        Assert.Equal(1, aliceTodos.GetArrayLength());

        var bobTodos = await bob.GetFromJsonAsync<JsonElement>("/api/todos");
        Assert.Equal(0, bobTodos.GetArrayLength());

        var bobDelete = await bob.DeleteAsync($"/api/todos/{id}");
        Assert.Equal(HttpStatusCode.NotFound, bobDelete.StatusCode);

        var aliceUpdate = await alice.PatchAsJsonAsync($"/api/todos/{id}", new { done = true });
        Assert.Equal(HttpStatusCode.OK, aliceUpdate.StatusCode);

        var aliceDelete = await alice.DeleteAsync($"/api/todos/{id}");
        Assert.Equal(HttpStatusCode.NoContent, aliceDelete.StatusCode);
    }

    [Fact]
    public async Task CreateTodo_WithBlankTitle_Returns400()
    {
        var client = ClientWithToken(TestAuthFactory.CreateToken());
        var response = await client.PostAsJsonAsync("/api/todos", new { title = "  " });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
