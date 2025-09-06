using Microsoft.AspNetCore.Mvc;
using TinyArcade.API.Models;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArcadeController : ControllerBase
    {
        private readonly IArcadeService _arcadeService;
        public ArcadeController(IArcadeService arcadeService)
        {
            _arcadeService = arcadeService;
        }

        [HttpGet("GetConsoles")]
        public IActionResult GetConsoles()
        {
            return Ok(BaseModel.Ok(_arcadeService.GetConsoles()));
        }

        [HttpPost("AddConsole")]
        public IActionResult AddConsole([FromBody] ConsoleModel console)
        {
            _arcadeService.AddConsole(console);
            return Ok(BaseModel.Ok());
        }

        [HttpGet("GetGames")]
        public IActionResult GetGames([FromQuery]int consoleId)
        {
            return Ok(BaseModel.Ok(_arcadeService.GetGames(consoleId)));
        }

        [HttpPost("AddGame")]
        public IActionResult Addgame([FromBody]GameModel game)
        {
            _arcadeService.AddGame(game);
            return Ok(BaseModel.Ok());
        }
    }
}
