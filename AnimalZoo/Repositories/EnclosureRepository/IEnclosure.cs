using AnimalZoo.Models;

namespace AnimalZoo.Repositories.EnclosureRepository
{
    public interface IEnclosure
    {
        List<Enclosure> GetAllEnclosure();
        Enclosure GetEnlosureById(int id);
        void AddEnclosure(Enclosure animal);
        void UpdateEnclosure(Enclosure animal);
        void DeleteEnclosure(int id);
        void AddEnlosureRange(IEnumerable<Enclosure> enlosure);
    }
}
