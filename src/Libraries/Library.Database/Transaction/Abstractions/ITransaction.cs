using System.Transactions;

namespace Library.Database.Transaction.Abstractions
{
    public interface ITransaction
    {
        void EnlistVolatile(Enlistable enlistmentNotification, EnlistmentOptions enlistmentOptions);
    }
}