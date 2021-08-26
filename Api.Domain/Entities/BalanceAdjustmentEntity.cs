namespace Api.Domain.Entities
{
    public class BalanceAdjustmentEntity : BaseEntity
    {
        public string accountNumber { get; set; }
        public float value { get; set; }
        public string type { get; set; }
    }
}
