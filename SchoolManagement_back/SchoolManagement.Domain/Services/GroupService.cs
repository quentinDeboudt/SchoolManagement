using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;
public class GroupService : IGroupService
{
    private readonly IGroupRepository _repository;

    public GroupService(IGroupRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get the total count of groups asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    /// <summary>
    /// Get all groups.
    /// </summary>
    public Task<IEnumerable<Group>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Get groups with pagination asynchronously.
    /// </summary>
    public async Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
       return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific group by ID.
    /// </summary>
    public Task<Group> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new group asynchronously.
    /// </summary>
    public void AddAsync(Group group)
    {
        _repository.AddAsync(group);
    }

    /// <summary>
    /// Update an existing group asynchronously.
    /// </summary>
    public async Task<Group> UpdateAsync(Group group)
    {
        return await _repository.UpdateAsync(group);
    }

    /// <summary>
    /// Delete a group by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
       _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search groups by term with pagination.
    /// </summary>
    public async Task<PagedResult<Group>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }
}
