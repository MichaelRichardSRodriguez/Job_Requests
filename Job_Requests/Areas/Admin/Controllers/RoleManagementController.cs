using Job_Requests.Models.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Job_Requests.Areas.Admin.Controllers
{
    [Area(StaticDetails.ROLE_ADMIN)]
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
	public class RoleManagementController : Controller
	{

		private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagementController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles);
		}

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
					TempData["success"] = "Role Created Successfully.";
					return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Index", _roleManager.Roles.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
				TempData["success"] = "Role Deleted Successfully.";
			}

			return Json(new { success = true });
		}

    }
}
