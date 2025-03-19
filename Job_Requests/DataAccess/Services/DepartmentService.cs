﻿using Job_Requests.DataAccess.Repositories;
using Job_Requests.Models;
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

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _repository.GetAllAsync();
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
    }
}
