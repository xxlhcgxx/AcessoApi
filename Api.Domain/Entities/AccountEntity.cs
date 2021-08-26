namespace Api.Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public int id { get; set; }
        public string accountNumber { get; set; }
        public float balance { get; set; }
    }
}
