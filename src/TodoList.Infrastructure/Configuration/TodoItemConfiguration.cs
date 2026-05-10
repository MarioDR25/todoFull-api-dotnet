using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Configuration;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.HasKey(t => t.Id);
        
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();


        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(t => t.Description)
               .HasMaxLength(200);

        builder.Property(t => t.IsCompleted)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(t => t.CreatedAt)
               .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.UserId)
               .HasDatabaseName("IX_TodoItems_UserId");

        builder.HasIndex(e => new { e.UserId, e.IsCompleted })
               .HasDatabaseName("IX_TodoItems_UserId_IsCompleted");
    }
}


