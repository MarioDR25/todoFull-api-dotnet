using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

        builder.Property(e => e.Username)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(e => e.PasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
               .IsRequired();


        // Índices únicos
        builder.HasIndex(e => e.Email)
               .IsUnique()
               .HasDatabaseName("UX_Users_Email");

        builder.HasIndex(e => e.Username)
               .IsUnique()
               .HasDatabaseName("UX_Users_Username");
    }
}


