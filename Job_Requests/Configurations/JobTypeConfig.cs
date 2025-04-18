﻿using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job_Requests.Configurations
{
    public class JobTypeConfig : IEntityTypeConfiguration<JobType>
    {
        public void Configure(EntityTypeBuilder<JobType> builder)
        {
            builder.HasKey(jt => jt.JobTypeId);
            builder.Property(jt => jt.JobTypeName).IsRequired().HasMaxLength(20);
            builder.HasIndex(jt => jt.JobTypeName).IsUnique();
            builder.Property(jt => jt.JobTypeDescription).IsRequired().HasMaxLength(300);
            builder.Property(jt => jt.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(jt => jt.Status).HasDefaultValue(RecordStatusEnum.Active).ValueGeneratedOnAdd();

            // Configure On to Many Relationship
            builder.HasOne(jt => jt.JobTypeAsCreatedByUser)
                    .WithMany()
                    .HasForeignKey(jt => jt.CreateUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(jt => jt.JobTypeAsUpdatedByUser)
                    .WithMany()
                    .HasForeignKey(jt => jt.UpdateUserId)
                    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
