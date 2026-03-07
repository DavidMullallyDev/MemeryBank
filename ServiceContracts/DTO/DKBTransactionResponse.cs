namespace ServiceContracts.DTO
{
    public class DKBankTransactionResponse
    {
        public Guid Id { get; set; }
        public DateTime? Buchungsdatum { get; set; }
        public string? Wertstellung { get; set; }
        public string? Status { get; set; }
        public string? Zahlungspflichtige { get; set; }
        public string? Zahlungsempfänger { get; set; }
        public string? Verwendungszweck { get; set; }
        public string? Umsatztyp { get; set; }
        public string? IBAN { get; set; }
        public decimal? Betrag { get; set; }
        public string? GläubigerID { get; set; }
        public string? Mandatsreferenz { get; set; }
        public string? Kundenreferenz { get; set; }
    }
}
