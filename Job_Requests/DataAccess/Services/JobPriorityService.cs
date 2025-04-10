using Job_Requests.DataAccess.Data;
using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
	public class JobPriorityService : IJobPriorityService
	{
		private readonly IRepository<JobPriority> _repository;
		private readonly ApplicationDbContext _context;
        public JobPriorityService(IRepository<JobPriority> repository, ApplicationDbContext context)
        {
            _repository = repository;
			_context = context;
        }
        public async Task AddPriorityLevelAsync(JobPriority priority)
		{
			await _repository.AddAsync(priority);
		}

		public async Task DeletePriorityLevelAsync(JobPriority priority)
		{
			await _repository.DeleteAsync(priority);
		}

		public async Task<JobPriorityPaginationVM> GetPaginatedPriorityLevelsAsync(int page, int pageSize, Expression<Func<JobPriority, bool>>? filter = null, bool tracked = false, params string[]? includeProperties)
		{
			// Get total count
			int totalJobPriorities = await _repository.GetTotalCountAsync();

			// Get total pages
			int totalPage = (int)Math.Ceiling((double)totalJobPriorities / pageSize);

			// Get All Records
			var jobPriorities = await _repository.GetPaginatedAsync(page, pageSize);

			// Instantiate pagination view model
			JobPriorityPaginationVM jobPriorityPaginationVM = new()
			{
				JobPriorities = jobPriorities.ToList(),
				CurrentPage = page,
				TotalPages = totalPage,
				PageSize = pageSize,
				RecordCount = (page - 1) * pageSize + jobPriorities.Count(),
				TotalRequests = totalJobPriorities
			};


			// Return View Model
			return jobPriorityPaginationVM;
		
		}

		public async Task<JobPriority> GetPriorityLevelByIdAsync(int id)
		{

            return await _repository.GetByIdAsync(id);
        }

        public async Task<JobPriority> GetPriorityLevelWithUserAsync(int id)
        {

            return await _repository.GetRecordAsync(jr => jr.JobPriorityId == id,
                                                    includeProperties: new string[] { "JobPriorityAsCreatedByUser", "JobPriorityAsUpdatedByUser" });
        }

        public async Task<IEnumerable<JobPriority>> GetPriorityLevelsAsync(Expression<Func<JobPriority, bool>>? filter = null,
																			bool tracked = false, 
																			params string[]? includeProperties)
		{
			return await _repository.GetAllAsync(filter, tracked, includeProperties);	
		}

		public async Task<int> GetTotalPriorityLevelCountAsync()
		{
			return await _repository.GetTotalCountAsync();
		}

		public async Task<bool> IsChangesMade(JobPriority priority)
		{
			var existingJobPriority = await _repository.GetRecordAsync(jp => jp.JobPriorityId == priority.JobPriorityId);

			if (existingJobPriority.PriorityDescription != priority.PriorityDescription
				|| existingJobPriority.PriorityLevel != priority.PriorityLevel)
			{
				return true;
			}

			return false;

		}

		public async Task<bool> IsExistingPriorityLevelId(int id)
		{
			return await _repository.AnyAsync(jp => jp.JobPriorityId == id);
		}

		public async Task<bool> IsExistingPriorityLevelWithDifferentId(int id, string priorityLevelName)
		{
			return await _repository.AnyAsync(jp => EF.Functions.Like(jp.PriorityLevel, priorityLevelName)
												&& jp.JobPriorityId != id);
		}

		public async Task UpdatePriorityLevelAsync(JobPriority priority)
		{
			await _repository.UpdateAsync(priority);
		}
	}
}
