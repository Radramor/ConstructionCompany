using System;
using System.Collections.Generic;
using System.Linq;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class BankService
    {
        private readonly ApplicationDbContext _context;

        public BankService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Bank> GetAll()
        {
            return _context.Banks.ToList();
        }

        public void Create(Bank bank)
        {
            _context.Banks.Add(bank);
            _context.SaveChanges();
        }

        public void Update(Bank bank)
        {
            var existingBank = _context.Banks.Find(bank.Id);
            if (existingBank != null)
            {
                existingBank.Name = bank.Name;
                existingBank.Address = bank.Address;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var bank = _context.Banks.Find(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
                _context.SaveChanges();
            }
        }
    }
}
