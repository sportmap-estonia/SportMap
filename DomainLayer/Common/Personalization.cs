
namespace DomainLayer.Common
{
    public class Personalization : BaseData
    {
        public Guid UserId { get; set; }
        public Guid BirthdatePrivacyId { get; set; }

        public User User { get; set; } = null!;
        public PrivacyType BirthdatePrivacyType { get; set; } = null!;
    }
}