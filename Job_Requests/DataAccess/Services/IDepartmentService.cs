using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync(Expression<Func<Department, bool>>? filter = null,
                                                    bool tracked = false,
                                                    params string[]? includeProperties);
        Task<DepartmentPaginationVM> GetPaginatedDepartmentsAsync(int page, int pageSize);

        Task<int> GetTotalDepartmentCountAsync();
		Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentWithUserAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task<bool> IsExistingDepartmentNameWithDifferentId(int id,string departmentName);
        Task<bool> IsExistingDepartmentId(int id);
        Task<bool> IsChangesMade(Department department);


	}
}
