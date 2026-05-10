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
               .HasColumnType("char(36)")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.Username)
               .IsRequired()
               .HasMaxLength(50)
               .HasCharSet("utf8mb4");

        builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(255)
               .HasCharSet("utf8mb4");

        builder.Property(e => e.PasswordHash)
               .IsRequired()
               .HasMaxLength(500)
               .HasCharSet("utf8mb4");

        builder.Property(e => e.CreatedAt)
               .IsRequired()
               .HasColumnType("datetime(6)");


        // Índices únicos
        builder.HasIndex(e => e.Email)
               .IsUnique()
               .HasDatabaseName("UX_Users_Email");

        builder.HasIndex(e => e.Username)
               .IsUnique()
               .HasDatabaseName("UX_Users_Username");
    }
}


