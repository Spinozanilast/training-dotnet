using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{
    public async Task<GameSummary[]?> GetGamesAsync() 
        => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];
    
    public async Task AddGame(GameDetails game)
        => await httpClient.PostAsJsonAsync("games", game);

    public async Task UpdateGame(GameDetails updatedGame)
        => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

    public async Task DeleteGame(int id)
        => await httpClient.DeleteAsync($"games/{id}");

    public async Task<GameDetails> GetGame(int gameId)
        => await httpClient.GetFromJsonAsync<GameDetails>($"games/{gameId}")
        ?? throw new Exception("Could not find game");
}