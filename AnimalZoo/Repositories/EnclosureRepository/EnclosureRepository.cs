using AnimalZoo.Database;
using AnimalZoo.Models;
using AnimalZoo.Repositories.AnimalsReposiotry;
using Microsoft.EntityFrameworkCore;

namespace AnimalZoo.Repositories.EnclosureRepository
{
    public class EnclosureRepository : IEnclosure
    {
        private readonly ZooDbContext _context;
        public EnclosureRepository(ZooDbContext context)
        {
            _context = context;
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            _context.Add(enclosure);
            _context.SaveChanges();
        }


        public void AddEnlosureRange(IEnumerable<Enclosure> enlosure)
        {
            _context.Enclosures.AddRange(enlosure);
            _context.SaveChanges();
        }

        public void DeleteEnclosure(int id)
        {
            var enclosureToDelte = GetEnlosureById(id);
            _context.Remove(enclosureToDelte);
            _context.SaveChanges();
        }

        public List<Enclosure> GetAllEnclosure()
        {
            return _context.Enclosures.ToList();
        }

        public Enclosure GetEnlosureById(int id)
        {
            Enclosure? enclosure = _context.Enclosures.Find(id);
            if (enclosure == null)
            {
                throw new ArgumentNullException(nameof(enclosure));
            }
            return enclosure;
        }

        public void UpdateEnclosure(Enclosure enclosure)
        {
            _context.Entry(enclosure).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
