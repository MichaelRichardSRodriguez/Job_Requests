using Job_Requests.Models;

namespace Job_Requests.DataAccess.Services
{
    public interface IJobRequestService
    {
        Task<IEnumerable<JobRequest>> GetJobRequestsAsync();
        Task<JobRequest> GetJobRequestByIdAsync(int id);
        Task AddJobRequestAsync(JobRequest jobRequest);
        Task DeleteJobRequestAsync(JobRequest jobRequest);
        Task UpdateJobRequestAsync(JobRequest jobRequest);
        Task<bool> IsExistingJobRequest(int id);
    }
}
