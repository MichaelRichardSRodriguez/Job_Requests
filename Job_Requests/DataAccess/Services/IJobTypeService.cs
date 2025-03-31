using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IJobTypeService
    {

        Task<IEnumerable<JobType>> GetJobTypesAsync(Expression<Func<JobType, bool>>? filter = null,
                                                   bool tracked = false,
                                                   params string[]? includeProperties);

		Task<JobTypePaginationVM> GetPaginatedJobTypesAsync(int page, int pageSize,Expression<Func<JobType, bool>>? filter = null,
												   bool tracked = false,
												   params string[]? includeProperties);
		Task<int> GetTotalJobTypeCountAsync();
		Task<JobType> GetJobTypeByIdAsync(int id);
        Task AddJobTypeAsync(JobType jobType);
        Task DeleteJobTypeAsync(JobType jobType);
        Task UpdateJobTypeAsync(JobType jobType);
        Task<bool> IsExistingJobTypeWithDifferentId(int id, string jobTypeName);
        Task<bool> IsExistingJobTypeId(int id);
    }
}
