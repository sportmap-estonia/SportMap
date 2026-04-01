using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Common
{
    public interface IEntity {
        Guid Id { get; init; }
        DateTime CreatedAt { get; init; }
        DateTime? ModifiedAt { get; init; }
        DateTime? RemovedAt { get; init; }
        uint XMin { get; init; }
    }

    public abstract class BaseEntity : IEntity
    {
        [Key] public Guid Id { get; init; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] public DateTime? ModifiedAt { get; init; }
        public DateTime? RemovedAt { get; init; }
        public uint XMin { get; init; }
    }
}
