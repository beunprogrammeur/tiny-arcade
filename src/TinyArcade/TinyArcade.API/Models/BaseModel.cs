namespace TinyArcade.API.Models
{
    public class BaseModel
    {
        public string? Status { get; set; }
        public string? Bearer { get; set; }
        public static BaseModel Ok(string? bearer = null) => new() { Status = "success", Bearer = bearer };
        public static BaseModel Fail() => new() { Status = "unsuccessful" };    
    }
}
