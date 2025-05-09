﻿// <auto-generated />
using System;
using Job_Requests.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Job_Requests.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250323120912_AddRequestingAndReceivingDepartment_InJobRequests")]
    partial class AddRequestingAndReceivingDepartment_InJobRequests
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Job_Requests.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Job_Requests.Models.JobPriority", b =>
                {
                    b.Property<int>("JobPriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobPriorityId"));

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("PriorityDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PriorityLevel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("JobPriorityId");

                    b.ToTable("JobPriority");
                });

            modelBuilder.Entity("Job_Requests.Models.JobRequest", b =>
                {
                    b.Property<int>("JobRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobRequestId"));

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceivingDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RequestingDepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("JobRequestId");

                    b.HasIndex("ReceivingDepartmentId");

                    b.HasIndex("RequestingDepartmentId");

                    b.ToTable("JobRequests");
                });

            modelBuilder.Entity("Job_Requests.Models.JobType", b =>
                {
                    b.Property<int>("JobTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobTypeId"));

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("JobTypeDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("JobTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("JobTypeId");

                    b.ToTable("JobTypes");
                });

            modelBuilder.Entity("Job_Requests.Models.JobRequest", b =>
                {
                    b.HasOne("Job_Requests.Models.Department", "ReceivingDepartment")
                        .WithMany("JobRequestsAsReceivingDepartment")
                        .HasForeignKey("ReceivingDepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Job_Requests.Models.Department", "RequestingDepartment")
                        .WithMany("JobRequestsAsRequestingDepartment")
                        .HasForeignKey("RequestingDepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReceivingDepartment");

                    b.Navigation("RequestingDepartment");
                });

            modelBuilder.Entity("Job_Requests.Models.Department", b =>
                {
                    b.Navigation("JobRequestsAsReceivingDepartment");

                    b.Navigation("JobRequestsAsRequestingDepartment");
                });
#pragma warning restore 612, 618
        }
    }
}
