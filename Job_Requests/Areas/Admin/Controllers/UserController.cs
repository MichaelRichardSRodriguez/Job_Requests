using Job_Requests.DataAccess.Services;
using Job_Requests.Models;
using Job_Requests.Models.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Job_Requests.Areas.Admin.Controllers
{
	[Area(StaticDetails.ROLE_ADMIN)]
	[Authorize(Roles = StaticDetails.ROLE_ADMIN + "," + StaticDetails.ROLE_MANAGER)]

	public class UserController : Controller
	{

		private readonly IRoleManagementService _roleManagementService;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(IRoleManagementService roleManagementService,
							UserManager<ApplicationUser> userManager)
		{
			_roleManagementService = roleManagementService;
			_userManager = userManager;

		}

        public async Task<IActionResult> Index()
		{

			var users = await _roleManagementService.GetUsersWithRolesAsync();

			foreach (var user in users)
			{
				user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
			}

			return View(users);

		}
	}
}
