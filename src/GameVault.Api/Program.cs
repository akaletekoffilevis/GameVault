using GameVault.Api.Endpoints;
using GameVault.Core;
using GameVault.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();
builder.Services.AddSingleton<GameService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGameEndpoints();

app.Run();

public partial class Program { }
