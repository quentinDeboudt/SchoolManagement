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
        private readonly SchoolManagementDbContext _context;

        public RoleService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the total count of roles asynchronously.
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _context.Roles.CountAsync();
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        /// <summary>
        /// Get roles with pagination asynchronously.
        /// </summary>
        public async Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
        {
            return await _context.Roles
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Get a specific role by ID.
        /// </summary>
        public Role GetById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Create a new role asynchronously.
        /// </summary>
        public void CreateAsync(Role role)
        {
             _context.Roles.AddAsync(role);
             _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an existing role asynchronously.
        /// </summary>
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                return null;
            }

            existingRole.Name = role.Name; // Example of property update
            _context.Roles.Update(existingRole);
            await _context.SaveChangesAsync();

            return existingRole;
        }

        /// <summary>
        /// Delete a role by ID asynchronously.
        /// </summary>
        public void DeleteAsync(int id)
        {
            // var role =  _context.Roles.FindAsync(id);
            // if (role != null){
            //     _context.Roles.Remove(role);
            // }
        }

        /// <summary>
        /// Search roles by term with pagination.
        /// </summary>
        public async Task<PagedResult<Role>> SearchRoles(string term, int pageIndex, int pageSize)
        {
            var query = _context.Roles
                .Where(r => r.Name.Contains(term));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Role>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
