using FileHelpers;

[DelimitedRecord(",")]
[IgnoreFirst(1)]
public class N26BankTransaction
{
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
    public DateTime? BookingDate { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
    public DateTime? ValueDate { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? PartnerName { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? PartnerIban { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? Type { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? PaymentReference { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? AccountName { get; set; }

    // Keep amounts as strings for robust parsing later
    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? Amount { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? OriginalAmount { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? OriginalCurrency { get; set; }

    [FieldQuoted('"', QuoteMode.OptionalForBoth)]
    [FieldTrim(TrimMode.Both)]
    public string? ExchangeRate { get; set; }
}
