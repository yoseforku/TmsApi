using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmsApi.Entities;

namespace TmsApi.Configurations;

public class CourseConfiguration 
    : IEntityTypeConfiguration<Course>
{
<<<<<<< HEAD
   /*  public void Configure(EntityTypeBuilder<Course> builder)
=======
    public void Configure(EntityTypeBuilder<Course> builder)
>>>>>>> 07650c5 (m6)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);


        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(150);

<<<<<<< HEAD

        builder.Property(c => c.MaxCapacity)
            .IsRequired();
    } */
    public void Configure(EntityTypeBuilder<Course> builder)
{
builder.HasKey(c => c.Id);
builder.Property(c => c.Code).IsRequired().HasMaxLength(10);
builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
builder.HasIndex(c => c.Code).IsUnique();
builder.HasMany(c => c.Enrollments).WithOne(e => e.Course).HasForeignKey(e => e.CourseId);
}

=======
        builder
        .HasIndex(c => c.Code).
        IsUnique();

        builder
        .HasMany(c => c.Enrollments)
        .WithOne(e => e.Course)
        .HasForeignKey(e => e.CourseId);

        builder.Property(c => c.MaxCapacity)
            .IsRequired();
    }
>>>>>>> 07650c5 (m6)
}