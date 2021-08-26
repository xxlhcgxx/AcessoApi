using System;

namespace Api.Domain.Entities
{
    public class TransferEntity : BaseEntity
    {
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public float Value { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class TransferSearchEntity
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class TransferLogEntity
    {
        public Guid transactionId { get; set; }
    }
}
