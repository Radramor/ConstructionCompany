using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СonstructionСompany.Model
{
    public class Object
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<AccountingForWorkPerformed> AccountingForWorkPerformeds { get; set; } = [];
        public List<Estimate> Estimates { get; set; } = [];
        public List<BuildingMaterialDistribution> BuildingMaterialDistributions { get; set; } = [];

    }
}
