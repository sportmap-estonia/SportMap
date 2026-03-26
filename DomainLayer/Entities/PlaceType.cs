using DomainLayer.Common;

namespace DomainLayer.Entities
{
    public class PlaceType : BaseData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
    }
}