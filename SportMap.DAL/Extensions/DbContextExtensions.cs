using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportMap.DAL.Extensions
{
    public static class DbContextExtensions
    {
        extension<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity
        {
            public void ConfigureBaseModelFields()
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.XMin)
                    .HasColumnType("xid")
                    .IsRowVersion();
            }
        }
    }
}
