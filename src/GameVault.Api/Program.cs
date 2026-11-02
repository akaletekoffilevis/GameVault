using GameVault.Api.Features.Games;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGameEndpoints();

app.Run();
