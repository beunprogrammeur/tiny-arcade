using TinyArcade.API.DatabaseModels;

namespace TinyArcade.API.Services.Interfaces
{
    public interface IDatabaseService
    {
        void AddConsole(DBConsole console);
        void AddEmulator(DBEmulator dBEmulator);
        void AddGame(DBGame game);
        void AddUser(string username, string passwordHash, string role);
        bool FindUser(string username);
        DBConsole? GetConsole(int consoleId);
        IList<DBConsole> GetConsoles();
        DBEmulator GetEmulatorByGameId(int id);
        IList<DBEmulator> GetEmulators();
        DBGame? GetGame(int gameId);
        IList<DBGame> GetGames(int consoleId);
        DBUser? GetUser(string userName);
        void Initialise();
        void SetUserPassword(string userName, string passwordHash);
        void SetUserRole(string userName, string? role);
    }
}
