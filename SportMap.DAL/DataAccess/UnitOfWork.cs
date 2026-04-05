using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions;
using SportMap.DAL.Abstractions.Repositories;
using SportMap.DAL.DataContext;
using SportMap.DAL.Repositories;

namespace SportMap.DAL.DataAccess
{
    public class UnitOfWork(AppDbContext dbContext, IPostRepository postRepo, IImageRepository imageRepo, IUserRepository userRepo, ILogger<UnitOfWork> logger) : IUnitOfWork
    {
        private bool disposed = false;

        #region Repositories
        public IPostRepository PostRepository => postRepo;
        public IImageRepository ImageRepository => imageRepo;
        public IUserRepository UserRepository => userRepo;
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
