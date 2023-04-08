using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    await using Stream stream =
    await client.GetStreamAsync("https://localhost:7169/api/Category");
    var categories =
        await JsonSerializer.DeserializeAsync<List<Category>>(stream);

    foreach (var repo in categories ?? Enumerable.Empty<Category>())
        Console.Write(repo.title);
}