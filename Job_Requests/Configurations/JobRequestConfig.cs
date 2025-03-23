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

			// One to Many Relation
			builder.HasOne(c => c.RequestingDepartment).WithMany(j => j.JobRequestsAsRequestingDepartment).HasForeignKey(j => j.RequestingDepartmentId);
			builder.HasOne(c => c.ReceivingDepartment).WithMany(j => j.JobRequestsAsReceivingDepartment).HasForeignKey(j => j.ReceivingDepartmentId);



			//            public int JobRequestId { get; set; }
			//public int DepartmentId { get; set; }
			//// public int EmployeeId { get; set; }
			//public string JobDescription { get; set; }
			//public DateTime? RequestDate { get; set; }
			//public DateTime? DateCompleted { get; set; }
			//public JobStatus Status { get; set; }

		}
    }
}
