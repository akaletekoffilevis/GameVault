# Instructions pour les agents IA sur GameVault

## Architecture
- Application de catalogue de jeux PC : interface HTML/CSS/JavaScript servie par ASP.NET Core, API REST minimale en C#.
- Un seul projet : `src/GameVault.Api/`. La logique est organisée par fonctionnalité dans `Features/Games/`.
- `Program.cs` configure les services, les fichiers statiques de `wwwroot/` et les routes.
- `Features/Games/Game.cs` définit le modèle ; `GameCatalog.cs` contient les données ; `GameService.cs` porte la recherche, les filtres et le tri ; `GameEndpoints.cs` déclare les routes `/api/games`.
- `wwwroot/index.html` est l'accueil ; `wwwroot/catalogue.html` consomme l'API ; `wwwroot/js/app.js` gère le rendu ; `wwwroot/css/style.css` contient les styles.
- Les images sont dans `wwwroot/images/`.

## Conventions
- Garder l'interface en français, responsive et accessible.
- Garder les liens de boutique vers des pages officielles.
- Ajouter les données de jeu dans `Features/Games/GameCatalog.cs` et les images dans `wwwroot/images/`.
- Ne pas placer de clés secrètes ou de jetons dans le JavaScript client.
- Réutiliser les API .NET intégrées ; éviter les dépendances externes sans besoin justifié.
- Ne pas modifier `wwwroot/index.html` ni `wwwroot/css/style.css` sans demande explicite.

## Vérification et exécution
- SDK .NET 10 requis.
- Compiler avec `dotnet build GameVault.sln`.
- Lancer depuis la racine avec `dotnet run --project src/GameVault.Api`.
- Vérifier les routes `GET /api/games`, `GET /api/games/years` et les interactions de `catalogue.html`.
- Les pages doivent être servies par ASP.NET Core ; ouvrir les fichiers en `file://` ne fournit pas l'API.
