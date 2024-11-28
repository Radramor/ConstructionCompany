namespace СonstructionСompany.Model
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public List<BuildingMaterialDistribution> MaterialDistributions { get; set; } = [];
        public List<BuildingMaterial> BuildingMaterials { get; set; } = new List<BuildingMaterial>();
        public List<int> CountMaterials { get; set; } = new List<int>();
        public List<BuyingBuildingMaterials> BuyingBuildingMaterials { get; set; } = [];
    }
}