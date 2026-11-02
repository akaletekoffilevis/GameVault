using GameVault.Core;

namespace GameVault.Api.Endpoints;

public static class GameEndpoints
{
    public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/games");

        group.MapGet("", (int? year, string? search, GameService games) =>
            Results.Ok(games.Search(year, search)));

        group.MapGet("/years", (GameService games) =>
            Results.Ok(games.GetYears()));

        return app;
    }
}
