using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public class JobRequestService : IJobRequestService
    {
        private readonly IRepository<JobRequest> _repository;

        public JobRequestService(IRepository<JobRequest> repository)
        {
            _repository = repository;
        }
        public async Task AddJobRequestAsync(JobRequest jobRequest)
        {
            await _repository.AddAsync(jobRequest);
        }

        public async Task DeleteJobRequestAsync(JobRequest jobRequest)
        {
            await _repository.DeleteAsync(jobRequest);
        }

        public async Task<JobRequest> GetJobRequestByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

		public async Task<JobRequest> GetJobRequestWithUserAsync(int id)
		{
			return await _repository.GetRecordAsync(j => j.JobRequestId == id,
													includeProperties: new string[] {
														"RequestingDepartment",
														"ReceivingDepartment",
														"JobType",
														"JobRequestAsCreatedByUser",
														"JobRequestAsUpdatedByUser",
														"JobRequestAsAssignedToUser"});
		}

		public async Task<IEnumerable<JobRequest>> GetJobRequestsAsync()
        {
            return await _repository.GetAllAsync(includeProperties: new string[]{ "RequestingDepartment", "ReceivingDepartment", "JobType" });
        }

        public async Task UpdateJobRequestAsync(JobRequest jobRequest)
        {
            await _repository.UpdateAsync(jobRequest);
        }

        public async Task<bool> IsExistingJobRequest(int id)
        {
            return await _repository.AnyAsync(j => j.JobRequestId == id);
        }

		public async Task<JobRequestsPaginationVM> GetPaginatedJobRequestsAsync(int page, int pageSize, Expression<Func<JobRequest,bool>>? filter = null)
		{
            // Get Job Requests' Total Count
            int totalRequests = await _repository.GetTotalCountAsync(filter);

            // Get Job Requests' Total Pages
            int totalPages = (int)Math.Ceiling((double)totalRequests / pageSize);

            // Get Job Requests' Paginated Records
            var jobRequests = await _repository.GetPaginatedAsync(page,pageSize,filter,includeProperties: new string[] { "RequestingDepartment", "ReceivingDepartment", "JobType" });

            JobRequestsPaginationVM jobRequestsPaginationVM = new()
            {
                JobRequests = jobRequests.ToList(),
				CurrentPage = page,
				TotalPages = totalPages,
                PageSize = pageSize,
                RecordCount = (page - 1) * pageSize + jobRequests.Count(),
				TotalRequests = totalRequests
			};

            return jobRequestsPaginationVM;
		}

		public async Task<int> GetTotalRequestsCountAsync()
		{
            return await _repository.GetTotalCountAsync();
		}

		public async Task<bool> IsChangesMade(JobRequest jobRequest)
		{
			var existingJobRequest = await _repository.GetRecordAsync(d => d.JobRequestId == jobRequest.JobRequestId);

			if (existingJobRequest.ReceivingDepartmentId != jobRequest.ReceivingDepartmentId
				|| existingJobRequest.JobDescription != jobRequest.JobDescription
                || existingJobRequest.JobTypeId != jobRequest.JobTypeId)
			{
				return true;
			}

			return false;

		}


	}

}
