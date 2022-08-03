using DDDTest.Domain.Aggregates.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DDDTest.DataAccess.Configurations {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            //builder.ToTable("Users", "DDDTest");

            builder.HasKey(d => d.Id);
            //builder.Property(o => o.Id).UseHiLo($"{nameof(User)}SequenceHiLo");

    


            builder.Property(p => p.UserName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Avatar).IsRequired();


            builder.HasIndex(p => p.UserName).IsUnique();
        }
    }
}
