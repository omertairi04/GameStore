using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEnpoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();
        
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync(); // for async
        });

        // GET /games/<id>
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
        
                return game is null ? 
                    Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        
            }
        ).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            
            Game game = newGame.ToEntity();
            
            dbContext.Games.Add(game); // or dbContext.Add(game)
            await dbContext.SaveChangesAsync();
            // always return a dto rather than instance!
            return Results.CreatedAtRoute(GetGameEndpointName,
                new {id = game.Id}, 
                game.ToGameDetailsDto()
                );
        });

        // PUT /games/<id>
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            
            if (existingGame is null) return Results.NotFound();
    
            dbContext.Entry(existingGame)
                .CurrentValues.SetValues(updatedGame.ToEntity(id));
    
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        });

        // DELETE /games/<id>
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();
    
            return Results.NoContent();
        });

        return group;
    }

}