﻿using Job_Requests.Models;
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
			builder.Property(d => d.DepartmentDescription).IsRequired().HasMaxLength(300);
			builder.Property(d => d.Status).HasDefaultValue(RecordStatusEnum.Active).ValueGeneratedOnAdd();

			// Assuming that Employee has a foreign key reference to Department
			builder.HasMany(jr => jr.JobRequests)  // 'JobRequest' is a navigation property in Department
                   .WithOne(d => d.Department) // 'Department' is a navigation property in JobRequest
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevents deletion if related records exist in Employee

        }

	}
}
