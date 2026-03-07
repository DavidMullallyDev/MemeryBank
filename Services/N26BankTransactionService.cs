using MemeryBank.BOL;
using ServiceContracts;

namespace Services
{
    public class N26BankTransactionService : IN26BankTransactionService
    {
        private readonly List<N26BankTransaction> _n26BankTrnasactions;

        public N26BankTransactionService(bool initialize = true)
        {
            _n26BankTrnasactions = [];

            if (initialize)
            {
              
            }
        }

        public N26BankTransaction AddBankTransaction(N26BankTransaction? personAddRequest)
        {
            throw new NotImplementedException();
        }

        public N26BankTransaction[] GetBankTransactionList()
        {   
            foreach(var file in Directory.GetFiles(@"C:\Users\mulla\OneDrive\Desktop\N26 Monthly BankStatements", "*.csv"))
            {
                var transactions = Filehandler.ReadFile(file);
                if(transactions != null) _n26BankTrnasactions.AddRange(transactions);
            }
            return [.. _n26BankTrnasactions];
        }

        public N26BankTransaction[] GetBankTransactionListByDate(int year, int month)
        {
            string monthstr = month < 10 ? "0" + month.ToString() : month.ToString();
            string file = Path.Combine(@"C:\Users\mulla\OneDrive\Desktop\N26 Monthly BankStatements", year.ToString() + $"{monthstr}.csv");
            N26BankTransaction[]? transactions = [];
            if (File.Exists(file))
            {
                transactions = Filehandler.ReadFile(file);
            }
            return transactions ?? [];
        }
        public List<N26BankTransaction>? GetFilteredBankTransactions()
        {
            throw new NotImplementedException();
        }

        public N26BankTransaction? GetN26BankTransactionByID(Guid? Id)
        {
            throw new NotImplementedException();
        }

        public List<N26BankTransaction> GetSortedBankTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
