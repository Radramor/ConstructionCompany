using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;


namespace СonstructionСompany
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; } = null!;
        public DbSet<AccountingForWorkPerformed> AccountingForWorkPerformeds { get; set; } = null!;
        public DbSet<Brigade> Brigades { get; set; } = null!;   
        public DbSet<Builder> Builders { get; set; } = null!;
        public DbSet<BuildingMaterial> BuildingMaterials { get; set; } = null!;
        public DbSet<BuildingMaterialDistribution> BuildingMaterialDistributions { get; set; } = null!;
        public DbSet<BuyingBuildingMaterials> BuyingBuildingMaterials { get; set; } = null!;
        public DbSet<Estimate> Estimates { get; set; } = null!;
        public DbSet<Model.Object> Objects { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
