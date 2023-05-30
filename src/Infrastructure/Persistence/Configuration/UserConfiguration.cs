//using Farsight.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Farsight.Infrastructure.Persistence.Configuration;
//public class UserConfiguration : IEntityTypeConfiguration<User>
//{
//    public void Configure(EntityTypeBuilder<User> builder)
//    {
//        builder.ToTable("Users");

//        builder.Property(x => x.Id)
//        .ValueGeneratedOnAdd()
//        .HasValueGenerator<VogenValueGenerator<UserId>>()
//        .UseIdentityAlwaysColumn();
//        builder.HasKey(x => x.Id);
//    }
//}
