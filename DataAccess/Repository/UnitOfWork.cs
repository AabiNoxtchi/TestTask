using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IDisposable
    {
        public DbContext Context { get; private set; }

        private DbContextTransaction trans = null;

        public UnitOfWork()
        {
            Context = new TestTaskDbContext();
            trans = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (trans != null)
            {
                trans.Commit();
                trans = null;
            }
        }

        public void RollBack()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans = null;
            }
        }

        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
