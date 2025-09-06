namespace TinyArcade.API.Models
{
    public class BaseModel
    {
        public string? Status { get; set; }
        public object? Results { get; set; }
        public static BaseModel Ok(object? results = null) => new() { Status = "success", Results = results };
        public static BaseModel Fail() => new() { Status = "unsuccessful" };    
    }
}
