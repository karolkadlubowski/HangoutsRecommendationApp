using System.Transactions;
using Library.Database.Transaction.Abstractions;

namespace Library.Database.Transaction
{
    public abstract class Enlistable : IEnlistmentNotification
    {
        public abstract void Commit(IEnlistment enlistment);
        public abstract void Rollback(IEnlistment enlistment);
        public abstract void Prepare(IPreparingEnlistment enlistment);
        public abstract void InDoubt(IEnlistment enlistment);

        void IEnlistmentNotification.Commit(Enlistment enlistment)
        {
            Commit(new DefaultEnlistment(enlistment));
        }

        void IEnlistmentNotification.InDoubt(Enlistment enlistment)
        {
            InDoubt(new DefaultEnlistment(enlistment));
        }

        void IEnlistmentNotification.Prepare(PreparingEnlistment preparingEnlistment)
        {
            Prepare(new DefaultPreparingEnlistment(preparingEnlistment));
        }

        void IEnlistmentNotification.Rollback(Enlistment enlistment)
        {
            Rollback(new DefaultEnlistment(enlistment));
        }

        private class DefaultEnlistment : IEnlistment
        {
            private Enlistment _enlistment;

            public DefaultEnlistment(Enlistment enlistment)
            {
                _enlistment = enlistment;
            }

            public void Done()
            {
                _enlistment.Done();
            }
        }

        private class DefaultPreparingEnlistment : DefaultEnlistment, IPreparingEnlistment
        {
            private PreparingEnlistment _enlistment;

            public DefaultPreparingEnlistment(PreparingEnlistment enlistment) : base(enlistment)
            {
                _enlistment = enlistment;
            }

            public void Prepared()
            {
                _enlistment.Prepared();
            }
        }
    }
}