using TinyArcade.API.Models;

namespace TinyArcade.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}
