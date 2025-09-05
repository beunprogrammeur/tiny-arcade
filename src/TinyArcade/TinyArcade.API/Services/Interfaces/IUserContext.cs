namespace TinyArcade.API.Services.Interfaces
{
    public interface IUserContext
    {
        string Role { get; set; }
        string UserName { get; set; }
        object Id { get; set; }
    }
}