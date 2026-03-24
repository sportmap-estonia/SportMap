namespace SportMap.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        //TODO: Add interfaces there
        public void Save();
    }
}
