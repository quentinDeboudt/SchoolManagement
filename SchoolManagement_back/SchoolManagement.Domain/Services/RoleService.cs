using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;
public class RoleService: IRoleService
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
    /// 
    public Task<IEnumerable<Role>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Get roles with pagination asynchronously.
    /// </summary>
    public async Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific role by ID.
    /// </summary>
    public Task<Role> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new role asynchronously.
    /// </summary>
    public void AddAsync(Role role)
    {
        _repository.AddAsync(role);
    }

    /// <summary>
    /// Update an existing role asynchronously.
    /// </summary>
    public async Task<Role> UpdateAsync(Role role)
    {
        return await _repository.UpdateAsync(role);
    }

    /// <summary>
    /// Delete a role by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search roles by term with pagination.
    /// </summary>
    public async Task<PagedResult<Role>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }
}
