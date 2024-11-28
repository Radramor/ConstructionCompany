namespace СonstructionСompany.Model
{
    public class Brigade
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Builder> Builders { get; set; } = [];
        public List<AccountingForWorkPerformed> accountingForWorkPerformeds { get; set; } = [];   
    }
}