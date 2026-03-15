using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class UnitOfWork(AppDbContext dbContext) : IDisposable
    {
        private readonly AppDbContext _context = dbContext;
        private bool disposed = false;

        #region Repositories
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
