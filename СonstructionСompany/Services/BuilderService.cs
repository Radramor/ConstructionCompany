using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using СonstructionСompany.Model;
using СonstructionСompany;

namespace СonstructionСompany.Services
{
    public class BuilderService
    {
        private readonly ApplicationDbContext _context;

        public BuilderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Builder> GetAll()
        {
            return _context.Builders.ToList();
        }

        public Builder? GetById(Guid id)
        {
            return _context.Builders.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Builder builder)
        {
            _context.Builders.Add(builder);
            _context.SaveChanges();
        }

        public void Update(Builder builder)
        {
            var existingBuilder = _context.Builders.FirstOrDefault(b => b.Id == builder.Id);
            if (existingBuilder == null) throw new InvalidOperationException("Строитель не найден.");

            existingBuilder.FirstName = builder.FirstName;
            existingBuilder.SecondName = builder.SecondName;
            existingBuilder.Patronymic = builder.Patronymic;
            existingBuilder.DateOfBirth = builder.DateOfBirth;
            existingBuilder.ResidentialAddress = builder.ResidentialAddress;
            existingBuilder.LengthOfService = builder.LengthOfService;
            existingBuilder.Specialties = builder.Specialties;

            _context.Builders.Update(existingBuilder);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var builder = _context.Builders.FirstOrDefault(b => b.Id == id);
            if (builder == null) throw new InvalidOperationException("Строитель не найден.");

            _context.Builders.Remove(builder);
            _context.SaveChanges();
        }
    }
}
