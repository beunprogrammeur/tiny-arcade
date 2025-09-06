namespace TinyArcade.API.DatabaseModels
{
    public class DBUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
