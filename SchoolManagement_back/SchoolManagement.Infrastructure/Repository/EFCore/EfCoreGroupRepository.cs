using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCoreGroupRepository : IGroupRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCoreGroupRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Groups asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Groups.CountAsync();
    }

    /// <summary>
    /// Get all Groups with related entities.
    /// </summary>
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        return await _context.Groups
            .Include(p => p.Classroom)
            .ToListAsync();
    }

    /// <summary>
    /// Get Groups with pagination asynchronously.
    /// </summary>
    public async Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Groups
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Classroom)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Group by ID.
    /// </summary>
    public async Task<Group> GetByIdAsync(int id)
    {
        return await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
    }

    /// <summary>
    /// Create a new Group asynchronously.
    /// </summary>
    public void AddAsync(Group Group)
    {
         _context.Groups.AddAsync(Group);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Group asynchronously.
    /// </summary>
    public async Task<Group> UpdateAsync(Group group)
    {
      var existingGroup = await _context.Groups.FindAsync(group.Id);
        if (existingGroup == null)
        {
            return null;
        }

        _context.Groups.Update(group);
        await _context.SaveChangesAsync();

        return existingGroup;
    }

    /// <summary>
    /// Delete a Group by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {
        var group = await _context.Groups.FindAsync(id);
    
        if (group != null)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search Groups by term with pagination.
    /// </summary>
    public async Task<PagedResult<Group>> Search(string term, int pageIndex, int pageSize)
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