using System.Net;
using System.Net.Http.Json;
using GameVault.Core;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GameVault.Tests;

public sealed class GameEndpointsTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetGames_ReturnsCatalogOrderedByYearDescending()
    {
        var games = await _client.GetFromJsonAsync<List<Game>>("/api/games");

        Assert.NotNull(games);
        Assert.Equal(3, games.Count);
        Assert.Equal(new[] { 2022, 2021, 2015 }, games.Select(game => game.Year));
    }

    [Fact]
    public async Task GetGames_WithYear_ReturnsOnlyMatchingYear()
    {
        var games = await _client.GetFromJsonAsync<List<Game>>("/api/games?year=2021");

        Assert.NotNull(games);
        var game = Assert.Single(games);
        Assert.Equal("eFootball", game.Title);
    }

    [Theory]
    [InlineData("war", "God of War")]
    [InlineData("football", "eFootball")]
    [InlineData("los santos", "Grand Theft Auto V")]
    public async Task GetGames_WithSearch_MatchesTitleGenreOrDescription(string search, string expectedTitle)
    {
        var games = await _client.GetFromJsonAsync<List<Game>>(
            $"/api/games?search={Uri.EscapeDataString(search)}");

        Assert.NotNull(games);
        Assert.Contains(games, game => game.Title == expectedTitle);
    }

    [Fact]
    public async Task GetYears_ReturnsDistinctYearsDescending()
    {
        var years = await _client.GetFromJsonAsync<List<int>>("/api/games/years");

        Assert.Equal(new[] { 2022, 2021, 2015 }, years);
    }

    [Fact]
    public async Task GetRoot_ServesIndexPage()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/html", response.Content.Headers.ContentType?.MediaType);
    }
}
