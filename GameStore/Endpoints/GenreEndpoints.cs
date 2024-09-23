using GameStore.Data;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext
                .Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync();
        });

        return group;
    }
}