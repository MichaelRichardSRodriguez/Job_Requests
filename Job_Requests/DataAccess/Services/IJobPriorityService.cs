using Job_Requests.Models;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
	public interface IJobPriorityService
	{
		Task<IEnumerable<JobPriority>> GetPriorityLevelsAsync(Expression<Func<JobPriority,bool>>? filter = null,
																bool tracked = false,
																params string[]? includeProperties);
		Task<JobPriority> GetPriorityLevelByIdAsync(int id);
		Task AddPriorityLevelAsync(JobPriority priority);
		Task DeletePriorityLevelAsync(JobPriority priority);
		Task UpdatePriorityLevelAsync(JobPriority priority);
		Task<bool> IsExistingPriorityLevelWithDifferentId(int id, string priorityLevelName);
		Task<bool> IsExistingPriorityLevelId(int id);

	}
}
