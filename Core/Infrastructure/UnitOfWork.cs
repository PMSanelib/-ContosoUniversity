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
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Begin()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.Database.CurrentTransaction.Commit();
        }

        public void RollBack()
        {
            _context.Database.CurrentTransaction.Rollback();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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