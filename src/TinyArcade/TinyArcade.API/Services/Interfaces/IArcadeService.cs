using TinyArcade.API.Models;

namespace TinyArcade.API.Services.Interfaces
{
    public interface IArcadeService
    {
        void AddConsole(ConsoleModel console);
        void AddGame(GameModel game);
        List<ConsoleModel> GetConsoles();
        List<GameModel> GetGames(int consoleId);
    }
}
