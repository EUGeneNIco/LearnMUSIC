using LearnMUSIC.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMUSIC.Infrastructure.Persistence.Configurations
{
  public class SongSheetConfiguration : IEntityTypeConfiguration<SongSheet>
  {
    public void Configure(EntityTypeBuilder<SongSheet> builder)
    {
      builder.HasOne(r => r.KeySignature)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(r => r.Genre)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

      builder.Property(b => b.KeySignatureId)
                .IsRequired();

      builder.Property(b => b.GenreId)
                .IsRequired();
    }
  }
}
