namespace AnimalZoo.Models
{

    public class Animal
    {
        public int Id { get; set; }
        public string Species { get; set; } = string.Empty;
        public string Food { get; set; } = string.Empty;
        public int Amount { get; set; }

        public Enclosure? Enclosure { get; set; }

    }

    public class AnimalsRoot
    {
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }

}
