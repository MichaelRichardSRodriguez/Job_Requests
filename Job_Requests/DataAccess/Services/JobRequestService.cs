using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
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
            return await _repository.GetRecordAsync(j => j.JobRequestId == id, includeProperties: new string[] { "Department" });
        }

        public async Task<IEnumerable<JobRequest>> GetJobRequestsAsync()
        {
            return await _repository.GetAllAsync(includeProperties: new string[]{ "Department"});
        }

        public async Task UpdateJobRequestAsync(JobRequest jobRequest)
        {
            await _repository.UpdateAsync(jobRequest);
        }

        public async Task<bool> IsExistingJobRequest(int id)
        {
            return await _repository.AnyAsync(j => j.JobRequestId == id);
        }

    }

}
