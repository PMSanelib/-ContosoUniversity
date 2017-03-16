using System;

namespace Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
    }

    public class UnitOfWork : IUnitOfWork
    {
        internal ApplicationDbContext Context;

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Begin()
        {
            Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            Context.SaveChanges();
            Context.Database.CurrentTransaction.Commit();
        }

        public void RollBack()
        {
            Context.Database.CurrentTransaction.Rollback();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}