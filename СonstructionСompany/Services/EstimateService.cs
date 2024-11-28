using System;
using System.Collections.Generic;
using System.Linq;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class EstimateService
    {
        private readonly ApplicationDbContext _context;

        public EstimateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Estimate> GetAll()
        {
            return _context.Estimates.ToList();
        }

        public void Create(Estimate estimate)
        {
            _context.Estimates.Add(estimate);
            _context.SaveChanges();
        }

        public void Update(Estimate estimate)
        {
            var existingEstimate = _context.Estimates.Find(estimate.Id);
            if (existingEstimate != null)
            {
                existingEstimate.ObjectId = estimate.ObjectId;
                existingEstimate.TotalPrice = estimate.TotalPrice;
                // Обновить материалы и их количество
                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var estimate = _context.Estimates.Find(id);
            if (estimate != null)
            {
                _context.Estimates.Remove(estimate);
                _context.SaveChanges();
            }
        }
    }
}
