namespace СonstructionСompany.Model
{
    public class BuildingMaterialDistribution
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid ObjectId { get; set; }
        public Object? Object { get; set; }
        public List<BuildingMaterial> BuildingMaterials { get; set; } = [];
        public List<int> CountMaterials { get; set; } = [];
        public Guid WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public string BuildingMaterialsString => string.Join(", ", BuildingMaterials.Select(m => m.Name));
        public string CountMaterialsString => string.Join(", ", CountMaterials);
    }
}