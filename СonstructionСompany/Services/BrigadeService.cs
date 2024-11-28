using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class BrigadeService
    {
        private readonly ApplicationDbContext _context;

        public BrigadeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Brigade> GetAll()
        {
            return _context.Brigades.ToList();
        }

        public Brigade GetById(Guid id)
        {
            return _context.Brigades.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Brigade brigade)
        {
            _context.Brigades.Add(brigade);
            _context.SaveChanges();
        }

        public void Update(Brigade brigade)
        {
            _context.Brigades.Update(brigade);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var brigadeToDelete = GetById(id);
            if (brigadeToDelete != null)
            {
                _context.Brigades.Remove(brigadeToDelete);
                _context.SaveChanges();
            }
        }
    }
}
