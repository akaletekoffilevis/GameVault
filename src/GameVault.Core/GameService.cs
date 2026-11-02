namespace GameVault.Core;

public sealed class GameService
{
    private readonly IGameRepository _repository;

    public GameService(IGameRepository repository) => _repository = repository;

    public IReadOnlyList<Game> Search(int? year, string? search)
    {
        IEnumerable<Game> games = _repository.GetAll();

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
        _repository
            .GetAll()
            .Select(game => game.Year)
            .Distinct()
            .OrderByDescending(year => year)
            .ToList();
}
