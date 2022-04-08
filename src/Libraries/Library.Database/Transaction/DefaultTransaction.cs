using System.Transactions;
using Library.Database.Transaction.Abstractions;

namespace Library.Database.Transaction
{
    public class DefaultTransaction : ITransaction
    {
        private System.Transactions.Transaction _transaction;

        public DefaultTransaction(System.Transactions.Transaction transaction)
        {
            _transaction = transaction;
        }

        public void EnlistVolatile(Enlistable enlistmentNotification, EnlistmentOptions enlistmentOptions)
            => _transaction.EnlistVolatile(enlistmentNotification, enlistmentOptions);
    }
}