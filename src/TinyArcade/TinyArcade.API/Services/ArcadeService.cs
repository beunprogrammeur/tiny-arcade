using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Models;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class ArcadeService : IArcadeService
    {
        private readonly IDatabaseService _databaseService;
        public ArcadeService(IDatabaseService databaseService) 
        {
            _databaseService = databaseService;
        }

        public void PlayGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public List<ConsoleModel> GetConsoles()
        {
            return [.. _databaseService.GetConsoles().Select(c => new ConsoleModel()
            {
                Id = c.Id,
                Name = c.Name,
            })];
        }

        public void AddConsole(ConsoleModel console)
        {
            _databaseService.AddConsole(new DBConsole()
            {
                Name = console.Name,
            });
        }

        public List<GameModel> GetGames(int consoleId)
        {
            return [.. _databaseService.GetGames(consoleId).Select(g => new GameModel()
            {
                ConsoleId = g.Id,
                Name = g.Name
            })];
        }

        public void AddGame(GameModel game)
        {
            _databaseService.AddGame(new DBGame()
            {
                ConsoleId = game.ConsoleId,
                Name = game.Name,
                Description = ""
            });
        }
    }
}
