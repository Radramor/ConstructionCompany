namespace СonstructionСompany.Model
{
    public class BuildingMaterial
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public decimal Price { get; set; }
        public List<BuyingBuildingMaterials> BuyingBuildingMaterials { get; set; } = [];
        public List<Warehouse> Warehouses { get; set; } = [];
        public List<Estimate> Estimates { get; set; } = [];
        public List<BuildingMaterialDistribution> BuildingMaterialsDistribution { get; set;} = [];
    
    }
}