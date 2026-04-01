using SportMap.DAL.Abstractions.Repositories;

namespace SportMap.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        //TODO: Add interfaces there
        IPostRepository PostRepository { get; }
        public void Save();
    }
}
