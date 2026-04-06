using SportMap.DAL.Abstractions.Repositories;

namespace SportMap.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IImageRepository ImageRepository { get; }
        IUserRepository UserRepository { get; }
        IPlaceRepository PlaceRepository { get; }
        IPlaceTypeRepository PlaceTypeRepository { get; }
        public void Save();
    }
}
