using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyArcade.API.Business;
using TinyArcade.API.Models;
using TinyArcade.API.Services;
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
        [Authorize(Roles = Constants.RoleAdmin)]
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
        [Authorize(Roles = Constants.RoleAdmin)]
        public IActionResult Addgame([FromBody]GameModel game)
        {
            _arcadeService.AddGame(game);
            return Ok(BaseModel.Ok());
        }

        [HttpDelete("DeleteGame")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteGame([FromBody] GameModel game)
        {
            return base.BadRequest("not implemented");
        }


        [HttpGet("GetEmulators")]
        [Authorize(Roles = Constants.RoleAdmin)]
        public IActionResult GetEmulators()
        {
            return Ok(BaseModel.Ok(_arcadeService.GetEmulators()));
        }

        [HttpPost("AddEmulator")]
        [Authorize(Roles = Constants.RoleAdmin)]
        public IActionResult AddEmulator([FromBody]EmulatorModel emulator)
        {
            _arcadeService.AddEmulator(emulator);
            return Ok(BaseModel.Ok());
        }

        [HttpPost("PlayGame")]
        public IActionResult PlayGame([FromBody]GameModel game)
        {
            if(_arcadeService.PlayGame(game))
            {
                return Ok(BaseModel.Ok());
            }
            else
            {
                return BadRequest(BaseModel.Fail());
            }
        }
    }
}
