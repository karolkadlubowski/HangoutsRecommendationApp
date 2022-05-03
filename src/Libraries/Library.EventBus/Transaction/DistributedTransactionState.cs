namespace Library.EventBus.Transaction
{
    public enum DistributedTransactionState
    {
        None = -1,
        Began = 0,
        InProgress = 1,
        Committed = 2,
        Rollbacked = 3
    }
}