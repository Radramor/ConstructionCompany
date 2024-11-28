using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;

namespace СonstructionСompany.Services
{
    public class AccountingService
    {
        private readonly ApplicationDbContext _context;

        public AccountingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AccountingForWorkPerformed> GetAll()
        {
            return _context.AccountingForWorkPerformeds.ToList();
        }

        public void Create(AccountingForWorkPerformed record)
        {
            _context.AccountingForWorkPerformeds.Add(record);
            _context.SaveChanges();
        }

        public void Update(AccountingForWorkPerformed record)
        {
            var existingRecord = _context.AccountingForWorkPerformeds.Find(record.Id);
            if (existingRecord != null)
            {
                existingRecord.Date = record.Date;
                existingRecord.BrigadeId = record.BrigadeId;
                existingRecord.WorkDescription = record.WorkDescription;
                existingRecord.ObjectId = record.ObjectId;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var record = _context.AccountingForWorkPerformeds.Find(id);
            if (record != null)
            {
                _context.AccountingForWorkPerformeds.Remove(record);
                _context.SaveChanges();
            }
        }
    }
}
