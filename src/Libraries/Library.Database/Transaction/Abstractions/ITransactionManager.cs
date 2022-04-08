using System.Transactions;

namespace Library.Database.Transaction.Abstractions
{
    public interface ITransactionManager
    {
        ITransaction CurrentTransaction { get; }
        ITransactionScope CreateScope(TransactionScopeOption options = TransactionScopeOption.Required);
    }
}