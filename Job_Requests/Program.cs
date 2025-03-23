
using Job_Requests.DataAccess.Data;
using Job_Requests.DataAccess.Repositories;
using Job_Requests.DataAccess.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add Services for DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => 
				options.UseSqlServer(builder.Configuration
				.GetConnectionString("JobRequestConnection")));


// Add Services for Micro Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add Services for Micro Services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IJobTypeService, JobTypeService>();
builder.Services.AddScoped<IJobPriorityService, JobPriorityService>();
builder.Services.AddScoped<IJobRequestService, JobRequestService>();


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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
