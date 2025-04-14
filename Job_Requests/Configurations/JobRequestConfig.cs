using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job_Requests.Configurations
{
	public class JobRequestConfig : IEntityTypeConfiguration<JobRequest>
	{
		public void Configure(EntityTypeBuilder<JobRequest> builder)
		{
			builder.HasKey(j => j.JobRequestId);
			builder.Property(j => j.JobDescription).IsRequired().HasMaxLength(int.MaxValue);
			builder.Property(j => j.Remarks).HasMaxLength(200);
			builder.Property(j => j.RequestDate).HasDefaultValueSql("GETDATE()");
			builder.Property(j => j.Status).HasDefaultValue(JobStatusEnum.Pending).ValueGeneratedOnAdd();
			builder.Property(j => j.JobTypeId).IsRequired();
			builder.Property(j => j.JobPriorityId).IsRequired(false);

			// One to Many Relation
			builder.HasOne(c => c.RequestingDepartment)
					.WithMany(j => j.JobRequestsAsRequestingDepartment)
					.HasForeignKey(j => j.RequestingDepartmentId)
					.OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(c => c.ReceivingDepartment)
					.WithMany(j => j.JobRequestsAsReceivingDepartment)
					.HasForeignKey(j => j.ReceivingDepartmentId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.JobType)
					.WithMany(j => j.JobRequests)
					.HasForeignKey(j => j.JobTypeId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.JobPriority)
					.WithMany(j => j.JobRequests)
					.HasForeignKey(j => j.JobPriorityId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(j => j.JobRequestAsCreatedByUser)
					.WithMany()
					.HasForeignKey(j => j.CreatedUserId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(j => j.JobRequestAsUpdatedByUser)
					.WithMany()
					.HasForeignKey(j => j.UpdatedUserId)
					.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(j => j.JobRequestAsAssignedToUser)
					.WithMany()
					.HasForeignKey(j => j.AssignedTo)
					.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
