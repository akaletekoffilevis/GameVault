namespace GameVault.Api.Features.Games;

public sealed class GameService
{
    private readonly IReadOnlyList<Game> _games;

    public GameService()
        : this(GameCatalog.Games)
    {
    }

    public GameService(IReadOnlyList<Game> games) => _games = games;

    public IReadOnlyList<Game> Search(int? year, string? search)
    {
        IEnumerable<Game> games = _games;

        if (year.HasValue)
        {
            games = games.Where(game => game.Year == year.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var query = search.Trim();
            games = games.Where(game =>
                game.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                game.Genre.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                game.Description.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        return games
            .OrderByDescending(game => game.Year)
            .ThenBy(game => game.Title)
            .ToList();
    }

    public IReadOnlyList<int> GetYears() =>
        _games
            .Select(game => game.Year)
            .Distinct()
            .OrderByDescending(year => year)
            .ToList();
}
