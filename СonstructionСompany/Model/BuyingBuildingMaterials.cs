namespace СonstructionСompany.Model
{
    public class BuyingBuildingMaterials
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public DateTime Date { get; set; }
        public List<BuildingMaterial> BuildingMaterials { get; set; } = new List<BuildingMaterial>();
        public List<int> CountMaterials { get; set; } = new List<int>();
        public decimal TotalPrice { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        // Свойства для удобства отображения
        public string BuildingMaterialsString => string.Join(", ", BuildingMaterials.Select(m => m.Name));
        public string CountMaterialsString => string.Join(", ", CountMaterials);
    }
}