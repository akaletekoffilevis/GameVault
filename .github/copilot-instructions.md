# Instructions pour les agents IA sur GameVault

## Architecture
- Application de catalogue de jeux PC : interface HTML/CSS/JavaScript servie par ASP.NET Core, API REST minimale en C#.
- Architecture Onion en trois projets sous `src/` :
  - `GameVault.Core` : domaine et application (`Game`, `IGameRepository`, `GameService`), sans dépendance externe.
  - `GameVault.Infrastructure` : adaptateurs (`GameCatalog`, `InMemoryGameRepository`), dépend de `Core`.
  - `GameVault.Api` : présentation (`Program.cs`, `Endpoints/GameEndpoints.cs`, `wwwroot/`), dépend de `Core` et `Infrastructure`.
- Les dépendances pointent toujours vers `GameVault.Core`.
- `GameVault.Api/Program.cs` est la composition root : c'est là qu'on enregistre `IGameRepository`, `GameService` et les middlewares.
- La logique métier ne doit pas être écrite dans les endpoints ; elle appartient à `GameService`.

## Tests
- Les tests d'intégration sont dans `tests/GameVault.Tests/` et utilisent `WebApplicationFactory`.
- Ils couvrent le tri, le filtre par année, la recherche, la liste des années et la page d'accueil.
- Le projet de tests est marqué `IsPublishable=false` : il ne doit pas apparaître dans `dotnet publish`.

## Conventions
- Garder l'interface en français, responsive et accessible.
- Garder les liens de boutique vers des pages officielles.
- Ajouter les données de jeu dans `src/GameVault.Infrastructure/GameCatalog.cs` et les images dans `src/GameVault.Api/wwwroot/images/`.
- Ne pas placer de clés secrètes ou de jetons dans le JavaScript client.
- Réutiliser les API .NET intégrées ; éviter les dépendances externes sans besoin justifié.
- Ne pas modifier `wwwroot/index.html` ni `wwwroot/css/style.css` sans demande explicite.
- Ne pas référencer `Infrastructure` depuis `Core`.

## Vérification et exécution
- SDK .NET 10 requis.
- Compiler avec `dotnet build GameVault.sln`.
- Lancer depuis la racine avec `dotnet run --project src/GameVault.Api`.
- Tester avec `dotnet test GameVault.sln`.
- Vérifier les routes `GET /api/games`, `GET /api/games/years` et les interactions de `catalogue.html`.
- Les pages doivent être servies par ASP.NET Core ; ouvrir les fichiers en `file://` ne fournit pas l'API.
