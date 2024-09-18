using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get the total count of roles asynchronously.
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        public IEnumerable<Role> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get roles with pagination asynchronously.
        /// </summary>
        public async Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
        {
           return _repository.GetWithPagination(pageNumber, pageSize);
        }

        /// <summary>
        /// Get a specific role by ID.
        /// </summary>
        public Role GetById(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a new role asynchronously.
        /// </summary>
        public void CreateAsync(Role role)
        {
            return _repository.AddAsync(person);
        }

        /// <summary>
        /// Update an existing role asynchronously.
        /// </summary>
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            return _repository.UpdateAsync(person);
        }

        /// <summary>
        /// Delete a role by ID asynchronously.
        /// </summary>
        public void DeleteAsync(int id)
        {
           return _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Search roles by term with pagination.
        /// </summary>
        public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
        {
            return _repository.Search(id);
        }
    }
}
