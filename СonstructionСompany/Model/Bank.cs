namespace СonstructionСompany.Model
{
    public class Bank
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Supplier> Suppliers { get; set; } = [];
    }
}