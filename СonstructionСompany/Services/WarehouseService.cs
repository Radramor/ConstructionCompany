using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using СonstructionСompany.Model;
using СonstructionСompany;

namespace ConstructionCompany.Services
{
    public class WarehouseService
    {
        private readonly ApplicationDbContext _context;

        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить все склады
        public List<Warehouse> GetAll()
        {
            return _context.Warehouses
                .Include(w => w.BuildingMaterials) // Подключаем строительные материалы для склада
                .ToList();
        }

        // Получить склад по ID
        public Warehouse GetById(Guid id)
        {
            return _context.Warehouses
                    .Include(w => w.BuildingMaterials)
                    .FirstOrDefault(w => w.Id == id);
        }

        // Создать новый склад
        public void Create(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            warehouse.BuildingMaterials = warehouse.BuildingMaterials ?? new List<BuildingMaterial>();
            warehouse.CountMaterials = warehouse.CountMaterials ?? new List<int>();

            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();
        }

        // Обновить существующий склад
        public void Update(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            _context.Warehouses.Update(warehouse);
            _context.SaveChanges();
        }

        // Удалить склад
        public void Delete(Guid id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                _context.SaveChanges();
            }
        }
    }
}
