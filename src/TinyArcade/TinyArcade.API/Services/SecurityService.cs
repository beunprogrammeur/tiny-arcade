using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ITokenService _tokenService;
        public SecurityService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        public bool Login(string userName, string password, out string jwt)
        {
            jwt = string.Empty;

            // This is a stub. Replace with real authentication logic.
            switch (userName)
            {
                case "TestUser" when password == "TestPassword":
                    jwt = _tokenService.GenerateToken(new Models.UserModel() { Username = "TestUser", Role = "User"});
                    return true;
                case "TestAdmin" when password == "TestPassword":
                    jwt = _tokenService.GenerateToken(new Models.UserModel() { Username = "TestAdmin", Role = "Admin" });
                    return true;
                default:
                    break;
            }

            return false;
        }
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool SetRole(string userName, string role)
        {
            throw new NotImplementedException();
        }
    }
}
