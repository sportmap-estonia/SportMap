namespace SportMap.DAL.Abstractions
{
    public class ImageStorageException : Exception
    {
        public ImageStorageException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
