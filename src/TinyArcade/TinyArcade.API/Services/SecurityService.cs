using System.Security.Cryptography;
using System.Text;
using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ITokenService _tokenService;
        private readonly IDatabaseService _databaseService;
        private readonly IUserContext _userContext;
        private readonly HashAlgorithm _hashAlgorithm;

        private const string _defaultUserRole = "User";
        private readonly IReadOnlyCollection<string> _allowedRoles = ["User", "Admin"];

        public SecurityService(ITokenService tokenService, IDatabaseService databaseService, IUserContext userContext)
        {
            _tokenService = tokenService;
            _databaseService = databaseService;
            _userContext = userContext;
            _hashAlgorithm = SHA256.Create();
        }

        public bool Login(string userName, string password, out string jwt)
        {
            jwt = string.Empty;

            DBUser user = _databaseService.GetUser(userName);

            if (user != null && ValidatePassword(password, user.PasswordHash))
            {
                jwt = _tokenService.GenerateToken(new Models.UserModel() { Username = user.Name, Role = user.Role });
                return true;
            }

            return false;
        }
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!ValidatePassword(oldPassword, _databaseService.GetUser(_userContext.UserName).PasswordHash))
            {
                return false;
            }

            _databaseService.SetUserPassword(_userContext.UserName, HashPassword(newPassword));
            return true;
        }

        public bool SetRole(string userName, string? role)
        {
            if (!_allowedRoles.Contains(role) || !_databaseService.FindUser(userName))
            {
                return false;
            }

            _databaseService.SetUserRole(userName, role);
            return true;
        }

        public bool CreateUser(string userName, string password)
        {
            if (_databaseService.FindUser(userName))
            {
                return false;
            }

            string hashedPassword = HashPassword(password);

            _databaseService.AddUser(userName, hashedPassword, _defaultUserRole);
            return true;
        }

        private bool ValidatePassword(string password, string passwordHash)
        {
            return passwordHash == HashPassword(password);
        }
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(_hashAlgorithm
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
