using System.Transactions;
using Library.Database.Transaction.Abstractions;

namespace Library.Database.Transaction
{
    public class TransactionManager : ITransactionManager
    {
        public ITransaction CurrentTransaction
            => new DefaultTransaction(System.Transactions.Transaction.Current);

        public ITransactionScope CreateScope(TransactionScopeOption options = TransactionScopeOption.Required)
            => new DefaultTransactionScope(new TransactionScope(options));
    }
}