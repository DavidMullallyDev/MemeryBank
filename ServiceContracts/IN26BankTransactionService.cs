namespace ServiceContracts
{
    public interface IN26BankTransactionService
    {
        N26BankTransaction? GetN26BankTransactionByID(Guid? Id);

        N26BankTransaction AddBankTransaction(N26BankTransaction? personAddRequest);

        N26BankTransaction[] GetBankTransactionList();

        N26BankTransaction[] GetBankTransactionListByDate(int year, int month);

        List<N26BankTransaction>? GetFilteredBankTransactions();

        List<N26BankTransaction> GetSortedBankTransactions();      
    }
}

