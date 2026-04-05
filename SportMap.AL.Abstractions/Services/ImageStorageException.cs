namespace SportMap.AL.Abstractions.Services
{
    public class ImageStorageException : Exception
    {
        public ImageStorageException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
