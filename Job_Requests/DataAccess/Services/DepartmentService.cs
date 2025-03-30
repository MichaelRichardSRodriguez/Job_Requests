using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            _repository = departmentRepository;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _repository.AddAsync(department);
        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            await _repository.DeleteAsync(department);
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(Expression<Func<Department, bool>>? filter = null,
                                                    bool tracked = false,
                                                    params string[]? includeProperties)
		{
            return await _repository.GetAllAsync(filter, tracked,includeProperties);
        }

        public async Task<bool> IsExistingDepartmentNameWithDifferentId(int id, string departmentName)
        {
            return await _repository.AnyAsync(d => EF.Functions.Like(d.DepartmentName,departmentName) 
            && d.DepartmentId != id);

        }

        public async Task<bool> IsExistingDepartmentId(int id)
        {
            return await _repository.AnyAsync(d => d.DepartmentId == id);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _repository.UpdateAsync(department);
        }

		public async Task<DepartmentPaginationVM> GetPaginatedDepartmentsAsync(int page, int pageSize)
		{
			// Get the total count of departments
			int totalDepartments = await _repository.GetTotalCountAsync();

			// Calculate total pages
			int totalPages = (int)Math.Ceiling((double)totalDepartments / pageSize);

			// Fetch departments with pagination
			var departments = await _repository.GetPaginatedAsync(page, pageSize);

            // Return the ViewModel containing the departments and pagination info

            DepartmentPaginationVM departmentPaginationVM = new()
            {
				Departments = departments.ToList(),
				CurrentPage = page,
				TotalPages = totalPages,
				PageSize = pageSize,
				RecordCount = (page - 1) * pageSize + departments.Count(),
				TotalDepartments = totalDepartments
			};

            return departmentPaginationVM;

		}

		public async Task<int> GetTotalDepartmentCountAsync()
		{
			return await _repository.GetTotalCountAsync();
		}
	}
}
