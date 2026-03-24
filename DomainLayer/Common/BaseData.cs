using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Common
{
    public abstract class BaseData
    {
        [Key] public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? ModifiedAt { get; init; }
        public DateTime? RemovedAt { get; init; }
    }
}
