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
		private readonly IJobTypeService _jobTypeService;

		public JobRequestController(IJobRequestService service, IDepartmentService departmentService, IJobTypeService jobTypeService)
		{
			_jobRequestService = service;
			_departmentService = departmentService;
			_jobTypeService = jobTypeService;
		}

		// GET: JobRequest
		public async Task<IActionResult> Index(int page = 1)
		{
			int pageSize = 10;
			return View(await _jobRequestService.GetPaginatedJobRequestsAsync(page,pageSize));
		}

		// GET: JobRequest
		public async Task<IActionResult> Assign(int page = 1)
		{
			int pageSize = 10;
			return View(await _jobRequestService.GetPaginatedJobRequestsAsync(page, pageSize,
                                                                              filter: jr => jr.Status != JobStatusEnum.Cancelled && jr.Status != JobStatusEnum.Completed));
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
			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			JobRequestVM jobRequestVM = new()
			{
				JobRequests = new JobRequest(),
				Departments = departments.Select(d => new SelectListItem
				{
					Text = d.DepartmentName,
					Value = d.DepartmentId.ToString()
				}).ToList(),
				JobTypes = jobTypes.Select(d => new SelectListItem
				{
					Text = d.JobTypeName,
					Value = d.JobTypeId.ToString()
				}).ToList()
			};
			return View(jobRequestVM);

		}

		// POST: JobRequest/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(JobRequestVM jobRequestVM)
		{

			if (ModelState.IsValid)
			{
				try
				{
					var jobRequest = jobRequestVM.JobRequests;

					await _jobRequestService.AddJobRequestAsync(jobRequest);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["error"] = $"Saving Failed. Error Encountered. {ex.Message}";
				}
			}

			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			jobRequestVM.Departments = departments.Select(d => new SelectListItem
			{
				Text = d.DepartmentName,
				Value = d.DepartmentId.ToString()
			});

			jobRequestVM.JobTypes = jobTypes.Select(d => new SelectListItem
			{
				Text = d.JobTypeName,
				Value = d.JobTypeId.ToString()
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

			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			JobRequestVM jobRequestVM = new()
			{
				JobRequests = jobRequest,
				Departments = departments.Select(d => new SelectListItem
				{
					Text = d.DepartmentName,
					Value = d.DepartmentId.ToString()
				}).ToList(),
				JobTypes = jobTypes.Select(d => new SelectListItem
				{
					Text = d.JobTypeName,
					Value = d.JobTypeId.ToString()
				}).ToList()
			};


			//ViewData["DepartmentId"] = new SelectList(await _departmentService.GetDepartmentsAsync(), "DepartmentId", "DepartmentName", jobRequest.DepartmentId);

			return View(jobRequestVM);
		}

		// POST: JobRequest/Edit/5
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
			var jobTypes = await _departmentService.GetDepartmentsAsync();

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
				return RedirectToAction(nameof(Assign));
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
