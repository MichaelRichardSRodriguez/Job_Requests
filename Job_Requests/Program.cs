
using Job_Requests.DataAccess.Data;
using Job_Requests.DataAccess.Repositories;
using Job_Requests.DataAccess.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Job_Requests.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Services for DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => 
				options.UseSqlServer(builder.Configuration
				.GetConnectionString("JobRequestConnection")));

// Add Services for Razor Pages
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).
				AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

// Configures the application cookie settings for authentication and authorization.
builder.Services.ConfigureApplicationCookie(option =>
{
	option.LoginPath = $"/Identity/Account/Login"; // Redirect to login page if not authenticated
	option.LogoutPath = $"/Identity/Account/Logout"; // Redirect to logout page after user logs out
	option.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Redirect to access denied page for unauthorized users
});

// Add Services for Micro Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add Services for Micro Services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IJobTypeService, JobTypeService>();
builder.Services.AddScoped<IJobPriorityService, JobPriorityService>();
builder.Services.AddScoped<IJobRequestService, JobRequestService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IRoleManagementService, RoleManagementService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication and Authorization 
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "/{area=Guest}/{controller=Home}/{action=Index}/{id?}");

app.Run();
