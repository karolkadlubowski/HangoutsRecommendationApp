namespace Library.Database.Transaction.Abstractions
{
    public interface IEnlistable
    {
        void Commit(IEnlistment enlistment);
        void Rollback(IEnlistment enlistment);
        void Prepare(IPreparingEnlistment enlistment);
        void InDoubt(IEnlistment enlistment);
    }
}