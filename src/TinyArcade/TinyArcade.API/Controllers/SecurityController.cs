using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyArcade.API.Models;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] CredentialModel credentials)
        {
            if(_securityService.Login(credentials.UserName, credentials.Password, out string jwt))
            {
                return Ok(BaseModel.Ok(jwt));
            }

            return Unauthorized(BaseModel.Fail());
        }

        [HttpGet("IsLoggedIn")]
        [Authorize]
        public IActionResult IsLoggedIn()
        {
            return Ok(BaseModel.Ok());
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] CredentialModel credentials)
        {
            return BadRequest("not implemented");
        }

        [HttpPost("SetRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult SetRole([FromBody] CredentialModel credentials)
        {
            return BadRequest("not implemented");
        }
    }
}
