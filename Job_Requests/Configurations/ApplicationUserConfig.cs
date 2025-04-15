using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job_Requests.Configurations
{
	public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
			builder.Property(u => u.MiddleName).IsRequired(false).HasMaxLength(50);
			builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
			builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);
			builder.Property(u => u.Status).HasDefaultValue(UserStatusEnum.Active).ValueGeneratedOnAdd();
        }
	}
}
