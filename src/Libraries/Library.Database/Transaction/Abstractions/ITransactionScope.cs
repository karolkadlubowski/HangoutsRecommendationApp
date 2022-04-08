using System;

namespace Library.Database.Transaction.Abstractions
{
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}