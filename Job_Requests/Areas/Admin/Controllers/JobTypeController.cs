using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Requests.DataAccess.Data;
using Job_Requests.Models;
using Job_Requests.Models.Enums;
using Job_Requests.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Job_Requests.Models.Consts;
using Microsoft.AspNetCore.Identity;

namespace Job_Requests.Areas.Admin.Controllers
{
    [Area(StaticDetails.ROLE_ADMIN)]
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class JobTypeController : Controller
    {

        private readonly IJobTypeService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobTypeController(IJobTypeService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: JobType
        public async Task<IActionResult> Index(int page = 1)
        {

            int pageSize = 10;

            return View(await _service.GetPaginatedJobTypesAsync(page, pageSize));
        }

        // GET: JobType/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var jobType = await _service.GetJobTypeWithUserAsync(id);

            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // GET: JobType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobType jobType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _service.IsExistingJobTypeWithDifferentId(jobType.JobTypeId, jobType.JobTypeName))
                    {
                        ModelState.AddModelError("JobTypeName", "Existing Job Type Name");
                        return View(jobType);
                    }

                    await _service.AddJobTypeAsync(jobType);
                    TempData["success"] = "Job Type Created Successfully.";

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    TempData["error"] = $"Saving Failed. An unexpected error occured: {ex.Message}";

                }
            }

            return View(jobType);
        }

        // GET: JobType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var jobType = await _service.GetJobTypeByIdAsync(id);

            if (jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        // POST: JobType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobType jobType)
        {
            if (id != jobType.JobTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (await _service.IsExistingJobTypeWithDifferentId(jobType.JobTypeId, jobType.JobTypeName))
                    {
                        ModelState.AddModelError("JobTypeName", "Existing Job Type Name");
                        return View(jobType);
                    }

                    if (!await _service.IsChangesMade(jobType))
                    {
                        TempData["error"] = "Unable to Update, no changes made.";
                        return View(jobType);
                    }

                    var updatedBy = await _userManager.GetUserAsync(User);

                    if (updatedBy == null)
                    {
						TempData["error"] = "User not found.";
						return View(jobType);
					}

                    var jobTypeFromDb = await _service.GetJobTypeByIdAsync(id);

                    if (jobTypeFromDb == null)
                    {
                        return NotFound();
                    }

                    jobTypeFromDb.JobTypeName = jobType.JobTypeName;
                    jobTypeFromDb.JobTypeDescription = jobType.JobTypeDescription;
                    jobTypeFromDb.UpdatedDate = DateTime.UtcNow;
                    jobTypeFromDb.UpdateUserId = updatedBy.Id;

                    await _service.UpdateJobTypeAsync(jobTypeFromDb);
                    TempData["success"] = "Job Type Updated Successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.IsExistingJobTypeId(jobType.JobTypeId))
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
            return View(jobType);
        }

        // GET: JobType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var jobType = await _service.GetJobTypeByIdAsync(id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // POST: JobType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobType = await _service.GetJobTypeByIdAsync(id);

            if (jobType != null)
            {
                await _service.DeleteJobTypeAsync(jobType);
                TempData["success"] = "Job Type Deleted Successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: JobType/Edit/5
        public async Task<IActionResult> Manage(int id)
        {

            var jobType = await _service.GetJobTypeByIdAsync(id);

            if (jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, JobType jobType)
        {
            if (id != jobType.JobTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var jobTypeFromDb = await _service.GetJobTypeByIdAsync(id);


                    jobTypeFromDb.Status = jobType.Status == RecordStatusEnum.Active
                                            ? RecordStatusEnum.Inactive
                                            : RecordStatusEnum.Active;

                    TempData["success"] = jobType.Status == RecordStatusEnum.Active
                                        ? "Job Type Deactivated Successfully."
                                        : "Job Type Activated Successfully.";

                    await _service.UpdateJobTypeAsync(jobTypeFromDb);
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Update Failed. An unexpected error occured: \n{ex.Message}";
                    return View(jobType);

                }

                return RedirectToAction(nameof(Index));
            }

            return View(jobType);
        }

    }
}
