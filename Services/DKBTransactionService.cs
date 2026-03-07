using Entities;
using MemeryBank.BOL;
using ServiceContracts;

namespace Services
{
    public class DKBBankTransactionService : IDKBBankTransactionService, IDisposable
    {
        private List<DKBBankTransaction>? _dkbBankTransactions;

        public DKBBankTransactionService(bool initialize = true)
        {
            _dkbBankTransactions = [];

            if (initialize)
            {

            }
        }

        public DKBBankTransaction[] GetDKBBankTransactionList()
        {
            _dkbBankTransactions = [];
            foreach (var file in Directory.GetFiles(@"C:\Users\mulla\OneDrive\Desktop\DKBMonthyStatements", "*.csv"))
            {
                var transactions = Filehandler.ReadFile(true, file);
                if (transactions != null) _dkbBankTransactions.AddRange(transactions);
            }
            return [.._dkbBankTransactions];
        }

        /* Quota exceeded. Please try again later. */
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DKBBankTransaction[] GetDKBBankTransactionListByDate(int year, int month)
        {
            _dkbBankTransactions = [];
            foreach (var file in Directory.GetFiles(@"C:\Users\mulla\OneDrive\Desktop\DKBMonthyStatements", "*.csv"))
            {
                string monthStr = month < 10 ? $"0{month}" : month.ToString();
                if (file.Contains($"{year}{monthStr}"))
                {
                    var transactions = Filehandler.ReadFile(true, file);
                    if (transactions != null) _dkbBankTransactions.AddRange(transactions);
                }     
            }
            return [.. _dkbBankTransactions];
        }
    }
}

