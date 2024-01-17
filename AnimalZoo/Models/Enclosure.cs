namespace AnimalZoo.Models
{
    public class Enclosure
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<string> Objects { get; set; } = new();
    }

    public class EnclosureRoot
    {
        public List<Enclosure> Enclosures { get; set; } = new();
    }
}
