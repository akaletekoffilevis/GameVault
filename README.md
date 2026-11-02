# GameVault

GameVault est un catalogue de jeux PC classés par année. L'interface est servie par ASP.NET Core (HTML, CSS, JavaScript) et une API REST minimaliste en C# fournit les données du catalogue.

## Structure

```text
GameVault.sln
src/
└── GameVault.Api/
    ├── Program.cs                  # Point d'entrée et configuration de l'application
    ├── Features/Games/
    │   ├── Game.cs                 # Modèle du jeu
    │   ├── GameCatalog.cs          # Catalogue initial (données en mémoire)
    │   ├── GameService.cs          # Filtres, recherche et tri
    │   └── GameEndpoints.cs        # Routes REST /api/games
    ├── wwwroot/                    # Pages, CSS, JavaScript et images
    ├── appsettings.json            # Configuration de production
    ├── appsettings.Development.json
    ├── web.config                  # Configuration IIS
    └── Properties/launchSettings.json
```

L'architecture est volontairement simple : un seul projet API, organisé par fonctionnalité (`Features/Games`). Les responsabilités sont séparées entre le modèle, les données, la logique métier et les routes HTTP.

## Prérequis

- SDK .NET 10

## Lancer en local

Depuis la racine du projet :

```bash
dotnet run --project src/GameVault.Api
```

Ouvrir ensuite `http://localhost:5080` dans le navigateur. Il faut lancer l'application .NET : ouvrir les fichiers HTML directement ne permet pas d'utiliser l'API.

## API

- `GET /api/games` : tous les jeux, triés par année décroissante.
- `GET /api/games?year=2022` : jeux d'une année.
- `GET /api/games?search=war` : recherche par titre, genre ou description.
- `GET /api/games/years` : années présentes dans le catalogue.

Les données sont définies dans `src/GameVault.Api/Features/Games/GameCatalog.cs`. Les images sont rangées dans `src/GameVault.Api/wwwroot/images/`.
