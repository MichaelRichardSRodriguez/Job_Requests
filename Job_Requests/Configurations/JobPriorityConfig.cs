using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job_Requests.Configurations
{
    public class JobPriorityConfig : IEntityTypeConfiguration<JobPriority>
    {
        public void Configure(EntityTypeBuilder<JobPriority> builder)
        {
            builder.HasKey(jp => jp.JobPriorityId);
            builder.Property(jp => jp.PriorityLevel).IsRequired().HasMaxLength(20);
            builder.Property(jp => jp.PriorityDescription).IsRequired().HasMaxLength(300);
            builder.Property(jp => jp.Status).HasDefaultValue(RecordStatusEnum.Active);
            builder.Property(jp => jp.CreatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
