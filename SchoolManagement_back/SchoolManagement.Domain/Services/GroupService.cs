using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
    public IEnumerable<Group> GetAll()
    {
        return _repository.GetAll();
    }

    /// <summary>
    /// Get groups with pagination asynchronously.
    /// </summary>
    public async Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
       return _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific group by ID.
    /// </summary>
    public Group GetById(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new group asynchronously.
    /// </summary>
    public void CreateAsync(Group group)
    {
        return _repository.AddAsync(person);
    }

    /// <summary>
    /// Update an existing group asynchronously.
    /// </summary>
    public async Task<Group> UpdateGroupAsync(Group group)
    {
        return _repository.UpdateAsync(person);
    }

    /// <summary>
    /// Delete a group by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
       return _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search groups by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
    {
        return _repository.Search(id);
    }
}
