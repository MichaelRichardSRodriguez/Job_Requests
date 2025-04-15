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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Job_Requests.Models.Consts;

namespace Job_Requests.Controllers
{
	[Area(StaticDetails.ROLE_USER)]
	[Authorize(Roles = StaticDetails.ROLE_USER + "," + StaticDetails.ROLE_MANAGER + "," + StaticDetails.ROLE_ADMIN)]
	public class JobRequestController : Controller
	{
		private readonly IJobRequestService _jobRequestService;
		private readonly IDepartmentService _departmentService;
		private readonly IJobTypeService _jobTypeService;
		private readonly UserManager<ApplicationUser> _userManager;

		public JobRequestController(IJobRequestService service,
									IDepartmentService departmentService,
									IJobTypeService jobTypeService,
									UserManager<ApplicationUser> userManager)
		{
			_jobRequestService = service;
			_departmentService = departmentService;
			_jobTypeService = jobTypeService;
			_userManager = userManager;


		}

		// GET: JobRequest
		public async Task<IActionResult> Index(int page = 1)
		{
			int pageSize = 10;

			var currentUser = await _userManager.GetUserAsync(User);

			var roles = await _userManager.GetRolesAsync(currentUser);
			var role = roles.FirstOrDefault();

			var departmentId = currentUser?.DepartmentId;

			return View(await _jobRequestService.GetPaginatedJobRequestsAsync(page, pageSize,
															  filter: jr => jr.Status != JobStatusEnum.Cancelled
															  && jr.Status != JobStatusEnum.Completed
															  && jr.RequestingDepartmentId == departmentId));


		}

		// GET: JobRequest/ManageIndex
		public async Task<IActionResult> ManageIndex(int page = 1)
		{
			int pageSize = 10;
			return View(await _jobRequestService.GetPaginatedJobRequestsAsync(page, pageSize,
																			  filter: jr => jr.Status != JobStatusEnum.Cancelled && jr.Status != JobStatusEnum.Completed));
		}

		// GET: JobRequest/Details/5
		public async Task<IActionResult> Details(int id)
		{

			var jobRequest = await _jobRequestService.GetJobRequestWithUserAsync(id);

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
			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser == null)
			{
				return NotFound();
			}

			var departmentId = currentUser.DepartmentId;
			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var receivingDepartments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active
																					 && d.DepartmentId != departmentId);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			JobRequestVM jobRequestVM = new()
			{
				JobRequests = new JobRequest(),
				Departments = departments.Select(d => new SelectListItem
				{
					Text = d.DepartmentName,
					Value = d.DepartmentId.ToString()
				}).ToList(),
				ReceivingDepartments = receivingDepartments.Select(d => new SelectListItem
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

			jobRequestVM.JobRequests.RequestingDepartmentId = departmentId;

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
					var createdBy = await _userManager.GetUserAsync(User);

					if (createdBy == null)
					{
						TempData["error"] = "User not found.";
						return View(jobRequestVM);
					}

					jobRequest.CreatedUserId = createdBy.Id;

					await _jobRequestService.AddJobRequestAsync(jobRequest);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["error"] = $"Saving Failed. An unexpected error occured: {ex.Message}";
				}
			}

			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser == null)
			{
				return NotFound();
			}

			var departmentId = currentUser.DepartmentId;

			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var receivingDepartments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active
																		 && d.DepartmentId != departmentId);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			jobRequestVM.Departments = departments.Select(d => new SelectListItem
			{
				Text = d.DepartmentName,
				Value = d.DepartmentId.ToString()
			});

			jobRequestVM.ReceivingDepartments = receivingDepartments.Select(d => new SelectListItem
			{
				Text = d.DepartmentName,
				Value = d.DepartmentId.ToString()
			}).ToList();


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

			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser == null)
			{
				return NotFound();
			}

			var departmentId = currentUser.DepartmentId;

			var jobRequest = await _jobRequestService.GetJobRequestByIdAsync(id);

			if (jobRequest == null)
			{
				return NotFound();
			}

			var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
			var receivingDepartments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active
															 && d.DepartmentId != departmentId);
			var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

			JobRequestVM jobRequestVM = new()
			{
				JobRequests = jobRequest,
				Departments = departments.Select(d => new SelectListItem
				{
					Text = d.DepartmentName,
					Value = d.DepartmentId.ToString()
				}).ToList(),
				ReceivingDepartments = receivingDepartments.Select(d => new SelectListItem
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

					if (!await _jobRequestService.IsChangesMade(jobRequestVM.JobRequests))
					{
						TempData["error"] = "Unable to Update, no changes made.";


						var departments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active);
						var receivingDepartments = await _departmentService.GetDepartmentsAsync(d => d.Status == RecordStatusEnum.Active
																					 && d.DepartmentId != jobRequestVM.JobRequests.RequestingDepartmentId);
						var jobTypes = await _jobTypeService.GetJobTypesAsync(d => d.Status == RecordStatusEnum.Active);

						jobRequestVM.Departments = departments.Select(d => new SelectListItem
						{
							Text = d.DepartmentName,
							Value = d.DepartmentId.ToString()
						});

						jobRequestVM.ReceivingDepartments = receivingDepartments.Select(d => new SelectListItem
						{
							Text = d.DepartmentName,
							Value = d.DepartmentId.ToString()
						}).ToList();

						jobRequestVM.JobTypes = jobTypes.Select(d => new SelectListItem
						{
							Text = d.JobTypeName,
							Value = d.JobTypeId.ToString()
						}).ToList();


						return View(jobRequestVM);

					}

					var updatedBy = await _userManager.GetUserAsync(User);

					if (updatedBy == null)
					{
						TempData["error"] = "User not found.";
						return View(jobRequestVM);
					}

					var jobRequestFromDb = await _jobRequestService.GetJobRequestByIdAsync(id);


					jobRequestFromDb.ReceivingDepartmentId = jobRequestVM.JobRequests.ReceivingDepartmentId;
					jobRequestFromDb.JobDescription = jobRequestVM.JobRequests.JobDescription;
					jobRequestFromDb.JobTypeId = jobRequestVM.JobRequests.JobTypeId;
					jobRequestFromDb.UpdatedUserId = updatedBy.Id;
					jobRequestFromDb.UpdateDate = DateTime.Now;

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

			return View(jobRequestVM);
		}


		// GET: JobRequest/ManageRequest/5
		public async Task<IActionResult> Manage(int id)
		{

			var jobRequest = await _jobRequestService.GetJobRequestWithUserAsync(id);
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

		// POST: JobRequest/ManageRequest/5
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
				return RedirectToAction(nameof(ManageIndex));
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
