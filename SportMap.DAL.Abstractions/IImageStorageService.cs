namespace SportMap.DAL.Abstractions
{
    public interface IImageStorageService
    {
        Task WriteAsync(Stream content, string path);
        Task<Stream> ReadStreamAsync(string path);
        Task DeleteAsync(string path);
    }
}
