using Job_Requests.Configurations;
using Job_Requests.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Requests.DataAccess.Data
{
	public class ApplicationDbContext: DbContext
	{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        //DbSets for Entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobPriority> JobPriority { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new JobRequestConfig());
            modelBuilder.ApplyConfiguration(new JobTypeConfig());
            modelBuilder.ApplyConfiguration(new JobPriorityConfig());
		}


	}
}
