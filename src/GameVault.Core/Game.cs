namespace GameVault.Core;

public sealed record Game(
    string Id,
    string Title,
    int Year,
    string Genre,
    string Description,
    string ImageUrl,
    string StoreUrl);
