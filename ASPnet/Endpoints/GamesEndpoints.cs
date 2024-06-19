using ASPnet.Api.Data;
using ASPnet.Api.Dtos;
using ASPnet.Api.Mapping;
using ASPnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPnet.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGameById";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        //GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                    .Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync());

        //GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndPointName);

        //POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext gameStoreContext) =>
        {

            Game game = newGame.ToEntity();
            game.Id = gameStoreContext.Games.Count() + 1;

            gameStoreContext.Games.Add(game);
            await gameStoreContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game.ToGameDetailsDto());
        });

        //PUT /games/id
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        //DELETE /games/id
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