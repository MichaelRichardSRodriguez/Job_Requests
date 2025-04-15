using Job_Requests.DataAccess.Services;
using Job_Requests.Models;
using Job_Requests.Models.Consts;
using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Job_Requests.Areas.Admin.Controllers
{
	[Area(StaticDetails.ROLE_ADMIN)]
	[Authorize(Roles = StaticDetails.ROLE_ADMIN + "," + StaticDetails.ROLE_MANAGER)]

	public class UserController : Controller
	{

		private readonly IUserRoleManagementService _userRoleManagementService;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(IUserRoleManagementService userRoleManagementService,
							UserManager<ApplicationUser> userManager)
		{
			_userRoleManagementService = userRoleManagementService;
			_userManager = userManager;

		}

        public async Task<IActionResult> Index()
		{

			var users = await _userRoleManagementService.GetUsersWithRolesAsync();

			foreach (var user in users)
			{
				user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
			}

			return View(users);

		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string id, string role)
		{
			// Step 1: Get the user by ID
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			try
			{
				// Step 2: Get current roles
				var currentRoles = await _userManager.GetRolesAsync(user);

				// Step 3: Remove current roles
				await _userManager.RemoveFromRolesAsync(user, currentRoles);

				// Step 4: Add the new role
				await _userManager.AddToRoleAsync(user, role);

				//// Optional: Update user's Role property if you're storing it in your custom ApplicationUser
				//user.Role = role;
				//await _userManager.UpdateAsync(user);

				TempData["success"] = "User Role updated successfully.";
			}
			catch (Exception ex)
			{

                TempData["error"] = $"Updating Failed. An unexpected error occured: {ex.Message}";
            }


			return RedirectToAction("Index");
		}


		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserStatus(string id)
		{
            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(id))
            {
                TempData["error"] = "Invalid user ID.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(currentUserId))
            {
                TempData["error"] = "Unable to determine current user.";
                return RedirectToAction("Index");
            }

            if (currentUserId == id)
			{
                TempData["error"] = "Modifying your own status is not allowed.";
                return RedirectToAction("Index");
            }


            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                TempData["error"] = "Target user not found.";
                return RedirectToAction("Index");
            }

			user.Status = user.Status == UserStatusEnum.Active 
							? UserStatusEnum.Inactive 
							: UserStatusEnum.Active;

			try
			{
                await _userManager.UpdateAsync(user);
                TempData["success"] = $"User Status updated {user.Status.ToString().ToUpper()} successfully.";
            }
			catch (Exception ex)
			{

                TempData["error"] = $"Updating Failed. An unexpected error occured: {ex.Message}";
            }

		
			return RedirectToAction("Index");
        }
    }
}
