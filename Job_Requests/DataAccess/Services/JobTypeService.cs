using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
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
            return await _repository.GetAllAsync(filter, tracked, includeProperties);
        }

        public async Task<JobType> GetJobTypeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsExistingJobTypeId(int id)
        {
            return await _repository.AnyAsync(jt => jt.JobTypeId == id);
        }

        public async Task<bool> IsExistingJobTypeWithDifferentId(int id, string jobTypeName)
        {
            return await _repository.AnyAsync(jt => EF.Functions.Like(jt.JobTypeName,jobTypeName)
                                                && jt.JobTypeId == id);
        }

        public async Task UpdateJobTypeAsync(JobType jobType)
        {
            await _repository.UpdateAsync(jobType);
        }
    }
}
