using Job_Requests.Models;
using Job_Requests.Models.ViewModels;

namespace Job_Requests.DataAccess.Services
{
    public interface IJobRequestService
    {
        Task<IEnumerable<JobRequest>> GetJobRequestsAsync();
		Task<JobRequestsPaginationVM> GetPaginatedJobRequestsAsync(int page, int pageSize);
		Task<int> GetTotalRequestsCountAsync();
		Task<JobRequest> GetJobRequestByIdAsync(int id);
        Task AddJobRequestAsync(JobRequest jobRequest);
        Task DeleteJobRequestAsync(JobRequest jobRequest);
        Task UpdateJobRequestAsync(JobRequest jobRequest);
        Task<bool> IsExistingJobRequest(int id);
    }
}
