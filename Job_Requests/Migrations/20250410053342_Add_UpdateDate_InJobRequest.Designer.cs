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
    [Migration("20250410053342_Add_UpdateDate_InJobRequest")]
    partial class Add_UpdateDate_InJobRequest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Job_Requests.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Job_Requests.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DepartmentId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DepartmentName")
                        .IsUnique();

                    b.HasIndex("UpdatedUserId");

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

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("JobPriorityId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("PriorityLevel")
                        .IsUnique();

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("JobPriority");
                });

            modelBuilder.Entity("Job_Requests.Models.JobRequest", b =>
                {
                    b.Property<int>("JobRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobRequestId"));

                    b.Property<string>("AssignedTo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("JobPriorityId")
                        .HasColumnType("int");

                    b.Property<int>("JobTypeId")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("JobRequestId");

                    b.HasIndex("AssignedTo");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("JobPriorityId");

                    b.HasIndex("JobTypeId");

                    b.HasIndex("ReceivingDepartmentId");

                    b.HasIndex("RequestingDepartmentId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("JobRequests");
                });

            modelBuilder.Entity("Job_Requests.Models.JobType", b =>
                {
                    b.Property<int>("JobTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobTypeId"));

                    b.Property<string>("CreateUserId")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("JobTypeId");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("JobTypeName")
                        .IsUnique();

                    b.HasIndex("UpdateUserId");

                    b.ToTable("JobTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Job_Requests.Models.ApplicationUser", b =>
                {
                    b.HasOne("Job_Requests.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Job_Requests.Models.Department", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", "DepartmentAsCreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.Department", null)
                        .WithMany("ApplicationUserDepartment")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Job_Requests.Models.ApplicationUser", "DepartmentAsUpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DepartmentAsCreatedByUser");

                    b.Navigation("DepartmentAsUpdatedByUser");
                });

            modelBuilder.Entity("Job_Requests.Models.JobPriority", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobPriorityAsCreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobPriorityAsUpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("JobPriorityAsCreatedByUser");

                    b.Navigation("JobPriorityAsUpdatedByUser");
                });

            modelBuilder.Entity("Job_Requests.Models.JobRequest", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobRequestAsAssignedToUser")
                        .WithMany()
                        .HasForeignKey("AssignedTo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobRequestAsCreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.JobPriority", "JobPriority")
                        .WithMany("JobRequests")
                        .HasForeignKey("JobPriorityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.JobType", "JobType")
                        .WithMany("JobRequests")
                        .HasForeignKey("JobTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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

                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobRequestAsUpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("JobPriority");

                    b.Navigation("JobRequestAsAssignedToUser");

                    b.Navigation("JobRequestAsCreatedByUser");

                    b.Navigation("JobRequestAsUpdatedByUser");

                    b.Navigation("JobType");

                    b.Navigation("ReceivingDepartment");

                    b.Navigation("RequestingDepartment");
                });

            modelBuilder.Entity("Job_Requests.Models.JobType", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobTypeAsCreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Job_Requests.Models.ApplicationUser", "JobTypeAsUpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("JobTypeAsCreatedByUser");

                    b.Navigation("JobTypeAsUpdatedByUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Job_Requests.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Job_Requests.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Job_Requests.Models.Department", b =>
                {
                    b.Navigation("ApplicationUserDepartment");

                    b.Navigation("JobRequestsAsReceivingDepartment");

                    b.Navigation("JobRequestsAsRequestingDepartment");
                });

            modelBuilder.Entity("Job_Requests.Models.JobPriority", b =>
                {
                    b.Navigation("JobRequests");
                });

            modelBuilder.Entity("Job_Requests.Models.JobType", b =>
                {
                    b.Navigation("JobRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
