using TinyArcade.API.Models;

namespace TinyArcade.API.Services.Interfaces
{
    public interface IArcadeService
    {
        void AddConsole(ConsoleModel console);
        void AddEmulator(EmulatorModel emulator);
        void AddGame(GameModel game);
        List<ConsoleModel> GetConsoles();
        List<EmulatorModel> GetEmulators();
        List<GameModel> GetGames(int consoleId);
        bool PlayGame(GameModel game);
    }
}
