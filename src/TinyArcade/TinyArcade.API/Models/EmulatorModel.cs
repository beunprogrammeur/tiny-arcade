namespace TinyArcade.API.Models
{

    /// <summary>
    /// Model representing an emulator configuration.
    /// ArgumentList supports {{Rom}} and {{RomFolder}} placeholders.
    /// </summary>
    public class EmulatorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Exe { get; set; }
        public string Arguments { get; set; }
    }
}
