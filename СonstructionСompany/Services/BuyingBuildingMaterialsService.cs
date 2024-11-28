using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    internal class BuyingBuildingMaterialsService
    {
        private readonly ApplicationDbContext _context;

        public BuyingBuildingMaterialsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BuyingBuildingMaterials> GetAll()
        {
            return _context.BuyingBuildingMaterials
                .Include(b => b.Supplier)
                .Include(b => b.Warehouse)
                .Include(b => b.BuildingMaterials)
                .ToList();
        }

        public void Create(BuyingBuildingMaterials buying)
        {
            _context.BuyingBuildingMaterials.Add(buying);
            _context.SaveChanges();
        }

        public void Update(BuyingBuildingMaterials buying)
        {
            _context.BuyingBuildingMaterials.Update(buying);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var buying = _context.BuyingBuildingMaterials.Find(id);
            if (buying != null)
            {
                _context.BuyingBuildingMaterials.Remove(buying);
                _context.SaveChanges();
            }
        }
    }
}
