using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
	public class RoleManagementService : IRoleManagementService
	{

		private readonly IRepository<ApplicationUser> _repository;

        public RoleManagementService(IRepository<ApplicationUser> repository)
        {
			_repository = repository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersWithRolesAsync(Expression<Func<ApplicationUser, bool>>? filter = null,
													bool tracked = false, 
													params string[]? includeProperties)
		{

			return await _repository.GetAllAsync(filter,tracked,includeProperties: new string[] { "Department" });

		}

	}
}
