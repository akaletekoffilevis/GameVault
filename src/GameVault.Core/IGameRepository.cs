namespace GameVault.Core;

public interface IGameRepository
{
    IReadOnlyList<Game> GetAll();
}
