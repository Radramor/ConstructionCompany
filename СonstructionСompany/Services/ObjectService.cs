using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using СonstructionСompany;
using СonstructionСompany.Model;



namespace ConstructionCompany.Services
{
    public class ObjectService
    {
        private readonly ApplicationDbContext _context;

        public ObjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<СonstructionСompany.Model.Object> GetAll()
        {
            return _context.Objects.ToList();
        }

        public void Create(СonstructionСompany.Model.Object obj)
        {
            _context.Objects.Add(obj);
            _context.SaveChanges();
        }

        public void Update(СonstructionСompany.Model.Object obj)
        {
            var existingObject = _context.Objects.Find(obj.Id);
            if (existingObject != null)
            {
                existingObject.Name = obj.Name;
                existingObject.Address = obj.Address;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var obj = _context.Objects.Find(id);
            if (obj != null)
            {
                _context.Objects.Remove(obj);
                _context.SaveChanges();
            }
        }
    }
}
