using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IJobRequestService
    {
        Task<IEnumerable<JobRequest>> GetJobRequestsAsync();
		Task<JobRequestsPaginationVM> GetPaginatedJobRequestsAsync(int page, int pageSize, Expression<Func<JobRequest, bool>>? filter = null);
		Task<int> GetTotalRequestsCountAsync();
		Task<JobRequest> GetJobRequestByIdAsync(int id);
        Task AddJobRequestAsync(JobRequest jobRequest);
        Task DeleteJobRequestAsync(JobRequest jobRequest);
        Task UpdateJobRequestAsync(JobRequest jobRequest);
        Task<bool> IsExistingJobRequest(int id);
    }
}
