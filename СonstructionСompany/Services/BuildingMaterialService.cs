using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class BuildingMaterialService
    {
        private readonly ApplicationDbContext _context;

        public BuildingMaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BuildingMaterial> GetAll()
        {
            return _context.BuildingMaterials.Include(b => b.Supplier).ToList();
        }

        public void Create(BuildingMaterial buildingMaterial)
        {
            _context.BuildingMaterials.Add(buildingMaterial);
            _context.SaveChanges();
        }

        public void Update(BuildingMaterial buildingMaterial)
        {
            var existingBuildingMaterial = _context.BuildingMaterials.Find(buildingMaterial.Id);
            if (existingBuildingMaterial != null)
            {
                existingBuildingMaterial.Name = buildingMaterial.Name;
                existingBuildingMaterial.Unit = buildingMaterial.Unit;
                existingBuildingMaterial.Price = buildingMaterial.Price;
                existingBuildingMaterial.SupplierId = buildingMaterial.SupplierId;
                existingBuildingMaterial.Supplier = buildingMaterial.Supplier;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var buildingMaterial = _context.BuildingMaterials.Find(id);
            if (buildingMaterial != null)
            {
                _context.BuildingMaterials.Remove(buildingMaterial);
                _context.SaveChanges();
            }
        }
    }
}
