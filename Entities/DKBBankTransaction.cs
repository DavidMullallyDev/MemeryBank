using FileHelpers;
namespace Entities
{
    [DelimitedRecord(";")]
    [IgnoreFirst(5)]
    public class DKBBankTransaction
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(ConverterKind.Date, "dd.MM.yy")]
        public DateTime? Buchungsdatum { get; set; }
        public string? Wertstellung { get; set; }
        public string? Status { get; set; }
        public string? Zahlungspflichtige { get; set; }
        public string? Zahlungsempfänger { get; set; }
        public string? Verwendungszweck { get; set; }
        public string? Umsatztyp { get; set; }
        public string? IBAN { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldTrim(TrimMode.Both)]
        public string? Betrag { get; set; }
        public string? GläubigerID { get; set; }
        public string? Mandatsreferenz { get; set; }
        public string? Kundenreferenz { get; set; }
    }
}
