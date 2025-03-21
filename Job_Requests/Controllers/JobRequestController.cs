using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Requests.DataAccess.Data;
using Job_Requests.Models;
using Job_Requests.DataAccess.Services;
using Job_Requests.Models.ViewModels;
using Job_Requests.Models.Enums;

namespace Job_Requests.Controllers
{
    public class JobRequestController : Controller
    {
        private readonly IJobRequestService _jobRequestService;
        private readonly IDepartmentService _departmentService;

        public JobRequestController(IJobRequestService service,IDepartmentService departmentService)
        {
            _jobRequestService = service;
            _departmentService = departmentService;
        }

        // GET: JobRequest
        public async Task<IActionResult> Index()
        {
            var jobRequests = await _jobRequestService.GetJobRequestsAsync();
            return View(jobRequests);
        }

        // GET: JobRequest/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);

            if (jobRequest == null)
            {
                return NotFound();
            }

            var departments = await _departmentService.GetDepartmentsAsync();
            JobRequestVM jobRequestVM = new()
            {
                JobRequests = jobRequest,
                Departments = departments.Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                })
            };

            return View(jobRequestVM);
        }

        // GET: JobRequest/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetDepartmentsAsync();

            JobRequestVM jobRequestVM = new()
            {
                JobRequests = new JobRequest(),
                Departments = departments.Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                }).ToList()
            };
            return View(jobRequestVM);

        }

        // POST: JobRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobRequestVM jobRequestVM)
        {

            if (ModelState.IsValid)
            {
                var jobRequest = jobRequestVM.JobRequests;

                await _jobRequestService.AddJobRequestAsync(jobRequest);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentService.GetDepartmentsAsync();
            jobRequestVM.Departments = departments.Select(d => new SelectListItem 
            {
                Text = d.DepartmentName,
                Value = d.DepartmentId.ToString()
            });

            return View(jobRequestVM);
        }

        // GET: JobRequest/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);

            if (jobRequest == null)
            {
                return NotFound();
            }

			var departments = await _departmentService.GetDepartmentsAsync();
			JobRequestVM jobRequestVM = new()
            {
                JobRequests = jobRequest,
                Departments = departments.Select(d => new SelectListItem
                {
                    Text= d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                })
            };


            //ViewData["DepartmentId"] = new SelectList(await _departmentService.GetDepartmentsAsync(), "DepartmentId", "DepartmentName", jobRequest.DepartmentId);

            return View(jobRequestVM);
        }

        // POST: JobRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobRequestVM jobRequestVM)
        {
            if (id != jobRequestVM.JobRequests.JobRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _jobRequestService.UpdateJobRequestAsync(jobRequestVM.JobRequests);
					TempData["success"] = "Job Request Updated Successfully.";
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _jobRequestService.IsExistingJobRequest(jobRequestVM.JobRequests.JobRequestId))
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
            
            return View(jobRequestVM);
        }

        // GET: JobRequest/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);

            if (jobRequest == null)
            {
                return NotFound();
            }

            var departments = await _departmentService.GetDepartmentsAsync();

            JobRequestVM jobRequestVM = new()
            {
                JobRequests = jobRequest,
                Departments = departments.Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                })
            };

            return View(jobRequestVM);
        }

        // POST: JobRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);
            if (jobRequest != null)
            {
                await _jobRequestService.DeleteJobRequestAsync(jobRequest);
				TempData["success"] = "Job Request Deleted Successfully.";
			}

            return RedirectToAction(nameof(Index));
        }

        // GET: JobRequest/Manage/5
        public async Task<IActionResult> Manage(int id)
        {

            var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);
            if (jobRequest == null)
            {
                return NotFound();
            }

            var departments = await _departmentService.GetDepartmentsAsync();
            JobRequestVM jobRequestVM = new()
            {
                JobRequests = jobRequest,
                Departments = departments.Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                })
            };


            //ViewData["DepartmentId"] = new SelectList(await _departmentService.GetDepartmentsAsync(), "DepartmentId", "DepartmentName", jobRequest.DepartmentId);


            //// Convert Enum to SelectListItem
            //var statusList = Enum.GetValues(typeof(JobStatusEnum))
            //                     .Cast<JobStatusEnum>()
            //                     .Select(s => new SelectListItem
            //                     {
            //                         Text = s.ToString(),
            //                         Value = ((int)s).ToString()
            //                     })
            //                     .ToList();

            //// Pass the list to the view via ViewBag
            //ViewBag.JobStatusList = statusList;

            return View(jobRequestVM);
        }

        // POST: JobRequest/Manage/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, JobRequestVM jobRequestVM)
        {
            if (id != jobRequestVM.JobRequests.JobRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var jobRequestFromDb = await _jobRequestService.GetJobRequestByIdAsync(id);

                    if (jobRequestFromDb == null)
                    {
                        return BadRequest();
                    }

                    jobRequestFromDb.Status = jobRequestVM.JobRequests.Status;
                    jobRequestFromDb.Remarks = jobRequestVM.JobRequests.Remarks;

                    if (jobRequestFromDb.Status == JobStatusEnum.Completed)
                    { 
                        jobRequestFromDb.DateCompleted = DateTime.Now;
                    }
					else 
					{
						jobRequestFromDb.DateCompleted = null;

						if (jobRequestFromDb.Status == JobStatusEnum.InProgress)
						    jobRequestFromDb.Remarks = null;
					}


                    await _jobRequestService.UpdateJobRequestAsync(jobRequestFromDb);
					TempData["success"] = "Job Request Updated Successfully.";

				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _jobRequestService.IsExistingJobRequest(jobRequestVM.JobRequests.JobRequestId))
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

            var departments = await _departmentService.GetDepartmentsAsync();
            jobRequestVM.Departments = departments.Select(d => new SelectListItem
            {
                Text = d.DepartmentName,
                Value = d.DepartmentId.ToString()
            });

            return View(jobRequestVM);
        }

    }
}
