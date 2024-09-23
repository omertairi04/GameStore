using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.RealeaseDate
        };
    }
    
    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.RealeaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name, // ! - wont be null
            game.Price,
            game.ReleaseDate
        );
    }
    
    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.GenreId, // ! - wont be null
            game.Price,
            game.ReleaseDate
        );
    }
}