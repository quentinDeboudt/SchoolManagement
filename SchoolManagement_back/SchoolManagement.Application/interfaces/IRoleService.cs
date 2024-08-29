using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Interfaces;
public interface IRoleService
{
    // Get the total number of roles.
    Task<int> CountAsync();

    // Get all roles without pagination.
    IEnumerable<Role> GetAll();

    // Get roles with pagination.
    Task<List<Role>> GetWithPagination(int pageNumber, int pageSize);

    // Get a specific role by ID.
    Role GetById(int id);

    // Create a new role.
    void CreateAsync(Role role);

    // Update an existing role asynchronously.
    Task<Role> UpdateRoleAsync(Role role);

    // Delete a role by ID.
    void DeleteAsync(int id);

    // Search roles by term with pagination.
    Task<PagedResult<Role>> SearchRoles(string term, int pageIndex, int pageSize);
}
