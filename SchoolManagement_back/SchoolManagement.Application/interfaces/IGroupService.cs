using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Interfaces;
public interface IGroupService
{
    // Get the total number of groups.
    Task<int> CountAsync();

    // Get all groups without pagination.
    IEnumerable<Group> GetAll();

    // Get groups with pagination.
    Task<List<Group>> GetWithPagination(int pageNumber, int pageSize);

    // Get a specific group by ID.
    Group GetById(int id);

    // Create a new group.
    void CreateAsync(Group group);

    // Update an existing group asynchronously.
    Task<Group> UpdateGroupAsync(Group group);

    // Delete a group by ID.
    void DeleteAsync(int id);

    // Search groups by term with pagination.
    Task<PagedResult<Group>> SearchGroups(string term, int pageIndex, int pageSize);
}
