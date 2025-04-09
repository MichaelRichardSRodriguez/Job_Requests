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

namespace Job_Requests.Areas.Admin.Controllers
{
    [Area(StaticDetails.ROLE_ADMIN)]
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class JobTypeController : Controller
    {

        private readonly IJobTypeService _service;

        public JobTypeController(IJobTypeService service)
        {
            _service = service;
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

            var jobType = await _service.GetJobTypeByIdAsync(id);

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

                    TempData["error"] = $"Saving Failed. Error Encountered. {ex.Message}";

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

                    jobType.UpdatedDate = DateTime.Now;

                    await _service.UpdateJobTypeAsync(jobType);
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

                    if (jobTypeFromDb.Status == RecordStatusEnum.Active)
                    {
                        //jobType.Status = RecordStatusEnum.Inactive;
                        jobTypeFromDb.Status = RecordStatusEnum.Inactive;
                        TempData["success"] = "Department Deactivated Successfully.";
                    }
                    else
                    {
                        //jobType.Status = RecordStatusEnum.Active;
                        jobTypeFromDb.Status = RecordStatusEnum.Active;
                        TempData["success"] = "Department Activated Successfully.";
                    }

                    await _service.UpdateJobTypeAsync(jobTypeFromDb);
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Update Failed. \n{ex.Message}";
                    return View(jobType);

                }

                return RedirectToAction(nameof(Index));
            }

            return View(jobType);
        }

    }
}
