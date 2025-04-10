using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Requests.DataAccess.Data;
using Job_Requests.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Job_Requests.Models.Enums;
using Job_Requests.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Job_Requests.Models.Consts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NuGet.Protocol.Core.Types;

namespace Job_Requests.Areas.Admin.Controllers
{
    [Area(StaticDetails.ROLE_ADMIN)]
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class JobPriorityController : Controller
    {
        private readonly IJobPriorityService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public JobPriorityController(IJobPriorityService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: JobPriority
        public async Task<IActionResult> Index(int page = 1)
        {

            int pageSize = 10;

            return View(await _service.GetPaginatedPriorityLevelsAsync(page, pageSize));
        }

        // GET: JobPriority/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var jobPriority = await _service.GetPriorityLevelWithUserAsync(id);

            if (jobPriority == null)
            {
                return NotFound();
            }

            return View(jobPriority);
        }

        // GET: JobPriority/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPriority/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPriority jobPriority)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _service.IsExistingPriorityLevelWithDifferentId(jobPriority.JobPriorityId, jobPriority.PriorityLevel))
                    {
                        ModelState.AddModelError("PriorityLevel", "Existing Priority Level");
                        return View(jobPriority);
                    }

                    // Get User
					var createdBy = await _userManager.GetUserAsync(User);

					if (createdBy == null)
					{
						TempData["error"] = "User not found.";
						return View(jobPriority);
					}

					jobPriority.CreatedUserId = createdBy.Id;

					await _service.AddPriorityLevelAsync(jobPriority);

                    TempData["success"] = "Priority Level Created Successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    TempData["error"] = $"Saving Failed. Error Encountered. {ex.Message}";

                }
            }

            return View(jobPriority);
        }

        // GET: JobPriority/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var jobPriority = await _service.GetPriorityLevelByIdAsync(id);

            if (jobPriority == null)
            {
                return NotFound();
            }
            return View(jobPriority);
        }

        // POST: JobPriority/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobPriority jobPriority)
        {
            if (id != jobPriority.JobPriorityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _service.IsExistingPriorityLevelWithDifferentId(id, jobPriority.PriorityLevel))
                    {
                        ModelState.AddModelError("PriorityLevel", "Existing Priority Level");
                        return View(jobPriority);
                    }

                    if (!await _service.IsChangesMade(jobPriority))
                    {
						TempData["error"] = "Unable to Update, no changes made.";
                        return View(jobPriority);
					}

					// Get User
					var updatedBy = await _userManager.GetUserAsync(User);

					if (updatedBy == null)
					{
						TempData["error"] = "User not found.";
						return View(jobPriority);
					}

                    //Get Current Values
					var jobPriorityFromDb = await _service.GetPriorityLevelByIdAsync(id);

                    if (jobPriorityFromDb == null)
                    {
                        return NotFound();
                    }

                    //Assign New Values
                    jobPriorityFromDb.PriorityDescription = jobPriority.PriorityDescription;
                    jobPriorityFromDb.PriorityLevel = jobPriority.PriorityLevel;
                    jobPriorityFromDb.UpdatedUserId = updatedBy.Id;
					jobPriorityFromDb.UpdatedDate = DateTime.Now;

                    await _service.UpdatePriorityLevelAsync(jobPriorityFromDb);

                    TempData["success"] = "Priority Level Updated Successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.IsExistingPriorityLevelId(jobPriority.JobPriorityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobPriority);
        }

        // GET: JobPriority/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var jobPriority = await _service.GetPriorityLevelByIdAsync(id);

            if (jobPriority == null)
            {
                return NotFound();
            }

            return View(jobPriority);
        }

        // POST: JobPriority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPriority = await _service.GetPriorityLevelByIdAsync(id);
            if (jobPriority != null)
            {
                await _service.DeletePriorityLevelAsync(jobPriority);
                TempData["success"] = "Priority Level Deleted Successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: JobPriority/Manage/5
        public async Task<IActionResult> Manage(int id)
        {

            var jobPriority = await _service.GetPriorityLevelByIdAsync(id);
            if (jobPriority == null)
            {
                return NotFound();
            }
            return View(jobPriority);
        }

        // GET: JobPriority/Manage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, JobPriority jobPriority)
        {
            if (id != jobPriority.JobPriorityId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var jobPriorityFromDb = await _service.GetPriorityLevelByIdAsync(id);

                    if (jobPriorityFromDb == null)
                    {
                        return NotFound();
                    }

                    if (jobPriority.Status == RecordStatusEnum.Active)
                    {
                        jobPriorityFromDb.Status = RecordStatusEnum.Inactive;
                        TempData["success"] = "Priority Level Deactivated Successfully.";
                    }
                    else
                    {
                        jobPriorityFromDb.Status = RecordStatusEnum.Active;
                        TempData["success"] = "Priority Level Activated Successfully.";
                    }

                    await _service.UpdatePriorityLevelAsync(jobPriorityFromDb);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return View(jobPriority);
        }

    }
}
