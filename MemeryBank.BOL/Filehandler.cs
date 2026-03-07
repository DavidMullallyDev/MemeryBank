using Entities;
using FileHelpers;
namespace MemeryBank.BOL
{
    public class Filehandler
    {
        public static N26BankTransaction[]? ReadFile(string filename)
        {
            if (File.Exists(filename))
            {
                var engine = new FileHelperEngine<N26BankTransaction>();
                return engine.ReadFile(filename);
            }
            return null;
        }

        public static DKBBankTransaction[]? ReadFile(bool isN26,string filename)
        {
            if (File.Exists(filename))
            {
                var engine = new FileHelperEngine<DKBBankTransaction>();
                return engine.ReadFile(filename);
            }
            return null;
        }
    }
}
