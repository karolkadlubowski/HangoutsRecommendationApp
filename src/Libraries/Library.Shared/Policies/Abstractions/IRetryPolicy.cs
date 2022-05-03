using System;
using System.Threading.Tasks;

namespace Library.Shared.Policies.Abstractions
{
    public interface IRetryPolicy
    {
        Task ExecutePolicyAsync(Func<Task> execute);
    }
}