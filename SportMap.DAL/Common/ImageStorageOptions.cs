namespace SportMap.DAL.Common
{
    public class ImageStorageOptions
    {
        public string BasePath { get; set; } = "/data/images";
        public long MaxFileSizeBytes { get; set; } = 8_388_608;
    }
}
