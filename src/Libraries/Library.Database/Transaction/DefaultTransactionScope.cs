using System.Transactions;
using Library.Database.Transaction.Abstractions;

namespace Library.Database.Transaction
{
    public class DefaultTransactionScope : ITransactionScope
    {
        private TransactionScope _scope;

        public DefaultTransactionScope(TransactionScope scope)
        {
            _scope = scope;
        }

        public void Complete() => _scope.Complete();

        public void Dispose() => _scope.Dispose();
    }
}