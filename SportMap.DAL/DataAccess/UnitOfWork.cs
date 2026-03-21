using SportMap.DAL.Abstractions;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private bool disposed = false;

        #region Repositories
        #endregion

        public void Save()
        {
            dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
