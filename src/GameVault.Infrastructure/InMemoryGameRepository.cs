using GameVault.Core;

namespace GameVault.Infrastructure;

public sealed class InMemoryGameRepository : IGameRepository
{
    public IReadOnlyList<Game> GetAll() => GameCatalog.Games;
}
