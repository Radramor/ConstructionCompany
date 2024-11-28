
namespace СonstructionСompany.Model
{
    public class AccountingForWorkPerformed
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid BrigadeId { get; set; }
        public Brigade? Brigade { get; set; }
        public string? WorkDescription { get; set; }
        public Guid ObjectId { get; set; }
        public Object? Object { get; set; }
    }
}
