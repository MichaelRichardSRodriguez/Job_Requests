﻿using Job_Requests.Models;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task<bool> IsExistingDepartmentNameWithDifferentId(int id,string departmentName);
        Task<bool> IsExistingDepartmentId(int id);

    }
}
