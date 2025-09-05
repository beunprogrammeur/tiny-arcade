namespace TinyArcade.API.Models
{
    public class CredentialModel
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? OldPassword { get; set; }
        public string? Role { get; set; }
    }
}
