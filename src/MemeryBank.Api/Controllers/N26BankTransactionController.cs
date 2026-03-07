using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace MemeryBank.Api.Controllers
{
    [Route("[controller]")]
    public class N26BankTransactionController : Controller
    {
        private readonly IN26BankTransactionService _n26BanktrasactionService;

        public N26BankTransactionController(IN26BankTransactionService n26BankTransactionService)
        {
            _n26BanktrasactionService = n26BankTransactionService;
        }

        [Route("[action]")]
        public IActionResult Index(int? year, int? month)
        {
            int y = year ?? DateTime.Now.Year;
            int m = month ?? DateTime.Now.Month;

            ViewBag.CurrentSelectedYear = y;
            ViewBag.CurrentSelectedMonth = m;
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "N26";

            List<N26BankTransactionResponse> n26BankTransactionResponses = [];
            N26BankTransaction[] n26BankTransactions = [];
            if (year == null || month == null)
            {
                n26BankTransactions = _n26BanktrasactionService.GetBankTransactionList();
            }
            else
            {
                n26BankTransactions = _n26BanktrasactionService.GetBankTransactionListByDate(y, m);
            }
          
                foreach (N26BankTransaction n26BankTransaction in n26BankTransactions)
                {
                    N26BankTransactionResponse n26BankTransactionResponse = new()
                    {
                        Id = Guid.NewGuid(),
                        BookingDate = (DateTime)n26BankTransaction.BookingDate,
                        ValueDate = (DateTime)n26BankTransaction.ValueDate,
                        PartnerName = n26BankTransaction.PartnerName,
                        PartnerIban = n26BankTransaction.PartnerIban,
                        Type = n26BankTransaction.Type,
                        PaymentReference = n26BankTransaction.PaymentReference,
                        AccountName = n26BankTransaction.AccountName,
                        Amount = decimal.Parse(n26BankTransaction.Amount.Replace(".", ",")),
                        OriginalAmount = n26BankTransaction.OriginalAmount,
                        OriginlaCurrency = n26BankTransaction.OriginalCurrency,
                        ExchangeRate = n26BankTransaction.ExchangeRate
                    };
                    n26BankTransactionResponses.Add(n26BankTransactionResponse);
                }
            
            return View(n26BankTransactionResponses);
        }
    }
}
