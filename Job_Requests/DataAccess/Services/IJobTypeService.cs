using Job_Requests.Models;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IJobTypeService
    {

        Task<IEnumerable<JobType>> GetJobTypesAsync(Expression<Func<JobType, bool>>? filter = null,
                                                   bool tracked = false,
                                                   params string[]? includeProperties);
        Task<JobType> GetJobTypeByIdAsync(int id);
        Task AddJobTypeAsync(JobType jobType);
        Task DeleteJobTypeAsync(JobType jobType);
        Task UpdateJobTypeAsync(JobType jobType);
        Task<bool> IsExistingJobTypeWithDifferentId(int id, string jobTypeName);
        Task<bool> IsExistingJobTypeId(int id);
    }
}
