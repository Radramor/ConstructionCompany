using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class SupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAll()
        {
            return _context.Suppliers.Include(s => s.Bank).ToList();
        }

        public void Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            var existingSupplier = _context.Suppliers.Find(supplier.Id);
            if (existingSupplier != null)
            {
                existingSupplier.NameOfOrganization = supplier.NameOfOrganization;
                existingSupplier.FirstNameOfSupervisor = supplier.FirstNameOfSupervisor;
                existingSupplier.SecondNameOfSupervisor = supplier.SecondNameOfSupervisor;
                existingSupplier.PatronymicOfSupervisor = supplier.PatronymicOfSupervisor;
                existingSupplier.Phone = supplier.Phone;
                existingSupplier.INN = supplier.INN;
                existingSupplier.KPP = supplier.KPP;
                existingSupplier.OGRN = supplier.OGRN;
                existingSupplier.SettlementAccount = supplier.SettlementAccount;
                existingSupplier.CorrespondentAccount = supplier.CorrespondentAccount;
                existingSupplier.BIK = supplier.BIK;
                existingSupplier.BankId = supplier.BankId;
                existingSupplier.Bank = supplier.Bank;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}
