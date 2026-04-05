using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Common;
using DomainLayer.Entities.Enums;

namespace DomainLayer.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public StatusType Status { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid? AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
