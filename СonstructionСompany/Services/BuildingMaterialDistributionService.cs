using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class BuildingMaterialDistributionService
    {
        private readonly ApplicationDbContext _context;

        public BuildingMaterialDistributionService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все распределения строительных материалов.
        /// </summary>
        public List<BuildingMaterialDistribution> GetAll()
        {
            return _context.BuildingMaterialDistributions
                .Include(d => d.Object) // Подключение объекта
                .Include(d => d.Warehouse) // Подключение склада
                .Include(d => d.BuildingMaterials) // Подключение материалов
                .ToList();
        }

        /// <summary>
        /// Получить распределение по ID.
        /// </summary>
        /// <param name="id">ID распределения.</param>
        public BuildingMaterialDistribution? GetById(Guid id)
        {
            return _context.BuildingMaterialDistributions
                .Include(d => d.Object)
                .Include(d => d.Warehouse)
                .Include(d => d.BuildingMaterials)
                .FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Создать новое распределение строительных материалов.
        /// </summary>
        /// <param name="distribution">Распределение для добавления.</param>
        public void Create(BuildingMaterialDistribution distribution)
        {
            if (distribution == null)
                throw new ArgumentNullException(nameof(distribution));

            _context.BuildingMaterialDistributions.Add(distribution);
            _context.SaveChanges();
        }

        /// <summary>
        /// Обновить существующее распределение строительных материалов.
        /// </summary>
        /// <param name="distribution">Распределение для обновления.</param>
        public void Update(BuildingMaterialDistribution distribution)
        {
            if (distribution == null)
                throw new ArgumentNullException(nameof(distribution));

            _context.BuildingMaterialDistributions.Update(distribution);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удалить распределение строительных материалов по ID.
        /// </summary>
        /// <param name="id">ID распределения.</param>
        public void Delete(Guid id)
        {
            var distribution = _context.BuildingMaterialDistributions.Find(id);
            if (distribution == null)
                throw new KeyNotFoundException("Распределение не найдено.");

            _context.BuildingMaterialDistributions.Remove(distribution);
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавить материалы и их количество в распределение.
        /// </summary>
        /// <param name="distributionId">ID распределения.</param>
        /// <param name="materials">Список материалов для добавления.</param>
        /// <param name="counts">Список количеств для каждого материала.</param>
        public void AddMaterials(Guid distributionId, List<BuildingMaterial> materials, List<int> counts)
        {
            var distribution = GetById(distributionId);
            if (distribution == null)
                throw new KeyNotFoundException("Распределение не найдено.");

            if (materials == null || counts == null || materials.Count != counts.Count)
                throw new ArgumentException("Списки материалов и количеств должны быть одинаковой длины.");

            for (int i = 0; i < materials.Count; i++)
            {
                distribution.BuildingMaterials.Add(materials[i]);
                distribution.CountMaterials.Add(counts[i]);
            }

            _context.BuildingMaterialDistributions.Update(distribution);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удалить материалы из распределения.
        /// </summary>
        /// <param name="distributionId">ID распределения.</param>
        /// <param name="materialIds">Список ID материалов для удаления.</param>
        public void RemoveMaterials(Guid distributionId, List<Guid> materialIds)
        {
            var distribution = GetById(distributionId);
            if (distribution == null)
                throw new KeyNotFoundException("Распределение не найдено.");

            foreach (var materialId in materialIds)
            {
                var index = distribution.BuildingMaterials.FindIndex(m => m.Id == materialId);
                if (index >= 0)
                {
                    distribution.BuildingMaterials.RemoveAt(index);
                    distribution.CountMaterials.RemoveAt(index);
                }
            }

            _context.BuildingMaterialDistributions.Update(distribution);
            _context.SaveChanges();
        }

        /// <summary>
        /// Получить краткое описание материалов в распределении.
        /// </summary>
        /// <param name="distributionId">ID распределения.</param>
        public string GetMaterialsSummary(Guid distributionId)
        {
            var distribution = GetById(distributionId);
            if (distribution == null)
                throw new KeyNotFoundException("Распределение не найдено.");

            return string.Join(", ", distribution.BuildingMaterials
                .Select((m, index) => $"{m.Name} ({(distribution.CountMaterials.Count > index ? distribution.CountMaterials[index] : 0)})"));
        }
    }
}
