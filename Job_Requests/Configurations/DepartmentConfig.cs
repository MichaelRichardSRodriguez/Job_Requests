using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job_Requests.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(d => d.DepartmentId);
            builder.Property(d => d.DepartmentName).IsRequired().HasMaxLength(100);
			builder.HasIndex(d => d.DepartmentName).IsUnique();
			builder.Property(d => d.DepartmentDescription).IsRequired().HasMaxLength(300);
            builder.Property(d => d.Status).HasDefaultValue(RecordStatusEnum.Active).ValueGeneratedOnAdd();
            builder.Property(d => d.CreatedDate).HasDefaultValueSql("GETDATE()");

            // Assuming that Employee has a foreign key reference to Department
            builder.HasMany(jr => jr.JobRequestsAsRequestingDepartment)  // 'JobRequest' is a navigation property in Department
                   .WithOne(d => d.RequestingDepartment) // 'Department' is a navigation property in JobRequest
                   .HasForeignKey(e => e.RequestingDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevents deletion if related records exist in Employee

            builder.HasMany(jr => jr.JobRequestsAsReceivingDepartment)  // 'JobRequest' is a navigation property in Department
                   .WithOne(d => d.ReceivingDepartment) // 'Department' is a navigation property in JobRequest
                   .HasForeignKey(e => e.ReceivingDepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevents deletion if related records exist in Employee

            builder.HasMany(a => a.ApplicationUserDepartment)  // 'ApplicationUser' is a navigation property in Department
                   .WithOne() // 'Department' is a navigation property in ApplicationUser
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevents deletion if related records exist in Employee

            builder.HasOne(d => d.DepartmentAsCreatedByUser)
                    .WithMany()
                    .HasForeignKey(a => a.CreatedUserId)
                    .OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(d => d.DepartmentAsUpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

		}

    }
}
