using Library.Database.Transaction;
using Library.Database.Transaction.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Database.DI
{
    public static class TransactionManagerDIConfig
    {
        public static IServiceCollection AddTransactionManager(this IServiceCollection services)
            => services.AddTransient<ITransactionManager, TransactionManager>();
    }
}