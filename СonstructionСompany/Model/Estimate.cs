namespace СonstructionСompany.Model
{
    public class Estimate
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public Object? Object { get; set; }
        public List<BuildingMaterial> BuildingMaterials { get; set; } = [];
        public List<int> CountMaterials { get; set; } = [];
        public decimal TotalPrice { get; set; }
        public string BuildingMaterialsString
        {
            get
            {
                // Объединяем имена строительных материалов в строку, разделенную запятой
                return string.Join(", ", BuildingMaterials.Select(b => b.Name));
            }
        }
        public string CountMaterialsString
        {
            get
            {
                return string.Join(", ", CountMaterials.Select(b => b));
            }
        }
    }
}