using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// connections from appsettings.json
var connString = builder.Configuration.GetConnectionString("GameStore"); 
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEnpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();