namespace TinyArcade.API.Models
{
    public class BaseModel
    {
        public string? Status { get; set; }
        public string? Bearer { get; set; }
        public object Results { get; set; }
        public static BaseModel Ok(object results = null, string? bearer = null) => new() { Status = "success", Bearer = bearer, Results = results };
        public static BaseModel Fail() => new() { Status = "unsuccessful" };    
    }
}
