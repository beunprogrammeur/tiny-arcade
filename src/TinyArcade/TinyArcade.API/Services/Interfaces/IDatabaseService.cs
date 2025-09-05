using TinyArcade.API.Models;

namespace TinyArcade.API.Services.Interfaces
{
    public interface IDatabaseService
    {
        IList<ConsoleModel> GetConsoles();
        IList<GameModel> GetGames(int consoleId);
        void Initialise();
    }
}
