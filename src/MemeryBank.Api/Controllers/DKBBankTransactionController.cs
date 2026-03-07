using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Globalization;

namespace MemeryBank.Api.Controllers
{
    [Route("[controller]")]
    public class DKBBankTransactionController : Controller
    {
        private readonly IDKBBankTransactionService _dkbBanktransactionService;
        public DKBBankTransactionController(IDKBBankTransactionService dkbBankTransactionService)
        {
            _dkbBanktransactionService = dkbBankTransactionService;
        }

        [Route("[action]")]
        public IActionResult Index(int? year, int? month)
        {
            int y = year ?? DateTime.Now.Year;
            int m = month ?? DateTime.Now.Month;

            ViewBag.Method = "Index";
            ViewBag.CurrentSelectedYear = y;
            ViewBag.CurrentSelectedMonth = m;
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "DKB";

            List<DKBankTransactionResponse> dkbBankTransactionResponses = [];
            DKBBankTransaction[] dkbBankTransactions = [];
            if (year == null || month == null)
            {
                dkbBankTransactions = _dkbBanktransactionService.GetDKBBankTransactionList();
            }
            else
            {
                dkbBankTransactions = _dkbBanktransactionService.GetDKBBankTransactionListByDate(y, m);
            }

            foreach (DKBBankTransaction dkbBankTransaction in dkbBankTransactions)
            {
                DKBankTransactionResponse dkbBankTransactionResponse = new()
                {
                    Id = Guid.NewGuid(),
                    IBAN = dkbBankTransaction.IBAN,
                    Buchungsdatum = dkbBankTransaction.Buchungsdatum,
                    GläubigerID = dkbBankTransaction.GläubigerID,
                    Kundenreferenz = dkbBankTransaction.Kundenreferenz,
                    Mandatsreferenz = dkbBankTransaction.Mandatsreferenz,
                    Status = dkbBankTransaction.Status,
                    Umsatztyp = dkbBankTransaction.Umsatztyp,
                    Verwendungszweck = dkbBankTransaction.Verwendungszweck,
                    Wertstellung = dkbBankTransaction.Wertstellung,
                    Zahlungsempfänger = dkbBankTransaction.Zahlungsempfänger,
                    Zahlungspflichtige = dkbBankTransaction.Zahlungspflichtige,
                    Betrag = decimal.Parse(dkbBankTransaction.Betrag.Replace(".","").Replace(".", ","))
                };

                dkbBankTransactionResponses.Add(dkbBankTransactionResponse);
            }

            return View(dkbBankTransactionResponses);
        }
    }
}

