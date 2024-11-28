namespace СonstructionСompany.Model
{
    public class Builder
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ResidentialAddress { get; set; }
        public int LengthOfService { get; set; }
        public string? Specialties { get; set; }
        public Guid BrigadeId { get; set; }
        public Brigade? Brigade { get; set; }
    }
}
