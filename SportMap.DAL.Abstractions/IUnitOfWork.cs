namespace SportMap.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public void Save();
    }
}
