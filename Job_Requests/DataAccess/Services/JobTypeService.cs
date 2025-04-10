using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IRepository<JobType> _repository;

        public JobTypeService(IRepository<JobType> repository)
        {
            _repository = repository;
        }

        public async Task AddJobTypeAsync(JobType jobType)
        {
            await _repository.AddAsync(jobType);
        }

        public async Task DeleteJobTypeAsync(JobType jobType)
        {
            await _repository.DeleteAsync(jobType);
        }

        public async Task<IEnumerable<JobType>> GetJobTypesAsync(Expression<Func<JobType, bool>>? filter = null, 
                                                                bool tracked = false, 
                                                                params string[]? includeProperties)
		{
            return await _repository.GetAllAsync( filter, tracked, includeProperties);
        }

        public async Task<JobType> GetJobTypeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<JobType> GetJobTypeWithUserAsync(int id)
        {
            return await _repository.GetRecordAsync(jt => jt.JobTypeId == id, includeProperties: new string[] { "JobTypeAsCreatedByUser", "JobTypeAsUpdatedByUser" });
        }

        public async Task<bool> IsExistingJobTypeId(int id)
        {
            return await _repository.AnyAsync(jt => jt.JobTypeId == id);
        }

        public async Task<bool> IsExistingJobTypeWithDifferentId(int id, string jobTypeName)
        {
            return await _repository.AnyAsync(jt => EF.Functions.Like(jt.JobTypeName,jobTypeName)
                                                && jt.JobTypeId != id);
        }

        public async Task UpdateJobTypeAsync(JobType jobType)
        {
            await _repository.UpdateAsync(jobType);
        }

		public async Task<JobTypePaginationVM> GetPaginatedJobTypesAsync(int page, int pageSize, Expression<Func<JobType, bool>>? filter = null, bool tracked = false, params string[]? includeProperties)
		{
            // Get total count
            int totalJobTypes = await _repository.GetTotalCountAsync();

            // Get total pages
            int totalPages = (int)Math.Ceiling((double)totalJobTypes / pageSize);

            // Get All Records
            var jobTypes = await _repository.GetPaginatedAsync(page,pageSize);

            JobTypePaginationVM jobTypePaginationVM = new()
            {
                JobTypes = jobTypes.ToList(),
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                RecordCount = (page - 1) * pageSize + jobTypes.Count(),
                TotalJobTypes = totalJobTypes
            };

            return jobTypePaginationVM;
		}

		public async Task<int> GetTotalJobTypeCountAsync()
		{
			return await _repository.GetTotalCountAsync();
		}

		public async Task<bool> IsChangesMade(JobType jobType)
		{
			var existingJobType = await _repository.GetRecordAsync(jt => jt.JobTypeId == jobType.JobTypeId);

            if(existingJobType.JobTypeName != jobType.JobTypeName
                || existingJobType.JobTypeDescription != jobType.JobTypeDescription)
            {
                return true;
            }

            return false;
		}
	}
}
