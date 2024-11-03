using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.DataAccess.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todos").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("TodoId");
            builder.Property(x => x.Title).HasColumnName("Title").IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.StartDate).HasColumnName("StartDate").IsRequired();
            builder.Property(x => x.EndDate).HasColumnName("EndDate").IsRequired();
            builder.Property(x => x.Priority).HasColumnName("Priority").IsRequired();
            builder.Property(x => x.Completed).HasColumnName("Completed").IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Todos)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany() 
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
