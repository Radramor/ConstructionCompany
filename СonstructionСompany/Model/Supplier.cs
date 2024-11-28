namespace СonstructionСompany.Model
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string? NameOfOrganization { get; set; }
        public string? FirstNameOfSupervisor { get; set; }
        public string? SecondNameOfSupervisor { get; set; }
        public string? PatronymicOfSupervisor { get; set; }
        public string? Phone {  get; set; }
        public string? INN { get; set; }
        public string? KPP { get; set; }
        public string? OGRN { get; set; }
        public string? SettlementAccount { get; set; }
        public string? CorrespondentAccount { get; set; }
        public string? BIK { get; set; }
        public Guid BankId { get; set; }
        public Bank? Bank { get; set; }
        public List<BuyingBuildingMaterials> BuyingBuildingMaterials { get; set; } = [];
        public List<BuildingMaterial> BuildingMaterials { get; set; } = [];
    }
}