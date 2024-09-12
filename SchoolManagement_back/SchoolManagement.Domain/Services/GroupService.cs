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
    private readonly SchoolManagementDbContext _context;

    public GroupService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of groups asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Groups.CountAsync();
    }

    /// <summary>
    /// Get all groups.
    /// </summary>
    public IEnumerable<Group> GetAll()
    {
        return _context.Groups
            .Include(c => c.Classroom)
            .ToList();
    }

    /// <summary>
    /// Get groups with pagination asynchronously.
    /// </summary>
    public async Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Groups
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(c => c.Classroom)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific group by ID.
    /// </summary>
    public Group GetById(int id)
    {
        return _context.Groups.FirstOrDefault(g => g.Id == id);
    }

    /// <summary>
    /// Create a new group asynchronously.
    /// </summary>
    public void CreateAsync(Group group)
    {
         _context.Groups.AddAsync(group);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing group asynchronously.
    /// </summary>
    public async Task<Group> UpdateGroupAsync(Group group)
    {
        var existingGroup = await _context.Groups.FindAsync(group.Id);
        if (existingGroup == null)
        {
            return null;
        }

        existingGroup.Name = group.Name; // Example of property update
        _context.Groups.Update(existingGroup);
        await _context.SaveChangesAsync();

        return existingGroup;
    }

    /// <summary>
    /// Delete a group by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        // _context.Groups.Remove(id);
        // _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search groups by term with pagination.
    /// </summary>
    public async Task<PagedResult<Group>> SearchGroups(string term, int pageIndex, int pageSize)
    {
        var query = _context.Groups
            .Where(g => g.Name.Contains(term));

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Group>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
