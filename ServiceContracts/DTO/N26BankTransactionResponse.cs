using FileHelpers;

namespace ServiceContracts.DTO
{
    public class N26BankTransactionResponse
    {
        public Guid Id { get; set; }
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime BookingDate { get; set; }
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime ValueDate { get; set; }
        public string? PartnerName { get; set; }
        public string? PartnerIban { get; set; }
        public string? Type { get; set; }
        public string? PaymentReference { get; set; }
        public string? AccountName { get; set; }
        public decimal Amount { get; set; }
        public string? OriginalAmount { get; set; }
        public string? OriginlaCurrency { get; set; }
        public string? ExchangeRate { get; set; }
    }
}
