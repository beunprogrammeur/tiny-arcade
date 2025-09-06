using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class UserContext : IUserContext
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public object Id { get; set; }
    }
}
