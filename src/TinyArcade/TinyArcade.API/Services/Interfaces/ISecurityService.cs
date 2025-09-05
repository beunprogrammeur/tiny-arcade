namespace TinyArcade.API.Services.Interfaces
{
    public interface ISecurityService
    {
        bool Login(string userName, string password, out string jwt);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool SetRole(string userName, string role);
    }
}
