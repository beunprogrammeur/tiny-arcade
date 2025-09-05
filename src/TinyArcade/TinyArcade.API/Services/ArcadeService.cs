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
    }
}
