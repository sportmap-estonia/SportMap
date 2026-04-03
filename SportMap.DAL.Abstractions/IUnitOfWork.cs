using SportMap.DAL.Abstractions.Repositories;

namespace SportMap.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IImageRepository ImageRepository { get; }
        public void Save();
    }
}
