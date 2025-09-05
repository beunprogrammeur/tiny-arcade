using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Models;

namespace TinyArcade.API.Services.Interfaces
{
    public interface IDatabaseService
    {
        void AddUser(string username, string passwordHash, string role);
        bool FindUser(string username);
        IList<ConsoleModel> GetConsoles();
        IList<GameModel> GetGames(int consoleId);
        DBUser? GetUser(string userName);
        void Initialise();
        void SetUserPassword(string userName, string passwordHash);
        void SetUserRole(string userName, string? role);
    }
}
