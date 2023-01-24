using EnterpriseArchitecture.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(120);

        builder
            .HasOne<UserOperationClaim>(u => u.UserOperationClaim)
            .WithOne(uo => uo.User)
            .HasForeignKey<UserOperationClaim>(uo => uo.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}