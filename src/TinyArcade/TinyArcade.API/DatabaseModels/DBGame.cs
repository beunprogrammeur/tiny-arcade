namespace TinyArcade.API.DatabaseModels
{
    public class DBGame
    {
        public int Id { get; set; }
        public int ConsoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
