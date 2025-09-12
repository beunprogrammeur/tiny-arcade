using Microsoft.VisualBasic;
using System.Diagnostics;
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

        public List<ConsoleModel> GetConsoles() =>
            [.. _databaseService.GetConsoles()
                .Select(c => new ConsoleModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })];
        

        public void AddConsole(ConsoleModel console) => 
            _databaseService.AddConsole(new DBConsole()
            {
                Name = console.Name,
            });

        public List<GameModel> GetGames(int consoleId) => 
            [.. _databaseService.GetGames(consoleId)
                .Select(g => new GameModel()
                {
                    ConsoleId = g.Id,
                    Name = g.Name
                })];
        
        public void AddGame(GameModel game) => 
            _databaseService.AddGame(new DBGame()
            {
                ConsoleId = game.ConsoleId,
                Name = game.Name,
                Description = ""
            });

        public List<EmulatorModel> GetEmulators() => 
            [.. _databaseService.GetEmulators()
                .Select(e => new EmulatorModel() 
                {
                    Id = e.Id,
                    Name = e.Name,
                    Exe = e.Path,
                    Arguments = e.Arguments
                })];

        public void AddEmulator(EmulatorModel emulator) => 
            _databaseService.AddEmulator(new DBEmulator()
            {
                Name = emulator.Name,
                Path = emulator.Exe,
                Arguments = emulator.Arguments
            });

        public bool PlayGame(GameModel game)
        {
            DBGame? dbGame = _databaseService.GetGame(game.Id);
            if(dbGame == null)
            {
                return false;
            }

            DBEmulator? dbEmulator = _databaseService.GetEmulatorByGameId(dbGame.Id);
            if (dbEmulator == null)
            {
                return false;
            }

            DBConsole? dbConsole = _databaseService.GetConsole(dbGame.ConsoleId);
            if(dbConsole == null)
            {
                return false;
            }

            Process? process = Process.Start(dbEmulator.Path, BuildArguments(dbEmulator.Arguments, dbGame, dbConsole));

            // TODO: keep the process object, 
            // don't start if one is already running
            // have a way to stop it (another endpoint?)

            return true;
        }

        private string BuildArguments(string template, DBGame game, DBConsole console) =>
            template.Replace(Business.Constants.PlaceHolderRom, game.Name)
                    .Replace(Business.Constants.PlaceHolderRomFolder, console.RomFolder);
    }
}
