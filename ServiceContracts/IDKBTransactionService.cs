using Entities;

namespace ServiceContracts
{
    public interface IDKBBankTransactionService 
    {
        DKBBankTransaction[] GetDKBBankTransactionList();

        DKBBankTransaction[] GetDKBBankTransactionListByDate( int year, int month);
    }
}
