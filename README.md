# GameVault

GameVault est un catalogue de jeux PC classés par année. L'interface est servie par ASP.NET Core (HTML, CSS, JavaScript) et une API REST minimaliste en C# fournit les données du catalogue.

## Architecture

Le projet suit une architecture **Onion** : les dépendances pointent toujours vers le centre (`GameVault.Core`).

```text
GameVault.sln
src/
├── GameVault.Core/              # Centre : domaine + application + ports
│   ├── Game.cs                  # Modèle du jeu
│   ├── IGameRepository.cs       # Port de persistance
│   └── GameService.cs           # Cas d'usage : recherche, filtres, tri
├── GameVault.Infrastructure/    # Adaptateurs : dépend de Core
│   ├── GameCatalog.cs           # Catalogue initial (données en mémoire)
│   └── InMemoryGameRepository.cs
└── GameVault.Api/               # Périphérie : dépend de Core + Infrastructure
    ├── Program.cs               # Composition root (DI, fichiers statiques, routes)
    ├── Endpoints/GameEndpoints.cs
    ├── wwwroot/                 # Pages, CSS, JavaScript et images
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── web.config
    └── Properties/launchSettings.json
tests/
└── GameVault.Tests/             # Tests d'intégration de l'API
    └── GameEndpointsTests.cs
```

Règles de dépendance :

```text
GameVault.Infrastructure ──> GameVault.Core
GameVault.Api ──> GameVault.Core
GameVault.Api ──> GameVault.Infrastructure
```

## Prérequis

- SDK .NET 10

## Lancer en local

Depuis la racine du projet :

```bash
dotnet run --project src/GameVault.Api
```

Ouvrir ensuite `http://localhost:5080` dans le navigateur. Il faut lancer l'application .NET : ouvrir les fichiers HTML directement ne permet pas d'utiliser l'API.

## Tests

```bash
dotnet test GameVault.sln
```

## API

- `GET /api/games` : tous les jeux, triés par année décroissante.
- `GET /api/games?year=2022` : jeux d'une année.
- `GET /api/games?search=war` : recherche par titre, genre ou description.
- `GET /api/games/years` : années présentes dans le catalogue.

Les données sont définies dans `src/GameVault.Infrastructure/GameCatalog.cs`. Les images sont rangées dans `src/GameVault.Api/wwwroot/images/`.
