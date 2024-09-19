using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCoreRoleRepository : IRoleRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCoreRoleRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Roles asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Roles.CountAsync();
    }

    /// <summary>
    /// Get all Roles with related entities.
    /// </summary>
    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    /// <summary>
    /// Get Roles with pagination asynchronously.
    /// </summary>
    public async Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Roles
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Role by ID.
    /// </summary>
    public async Task<Role> GetByIdAsync(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Create a new Role asynchronously.
    /// </summary>
    public void AddAsync(Role Role)
    {
         _context.Roles.AddAsync(Role);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Role asynchronously.
    /// </summary>
    public async Task<Role> UpdateAsync(Role role)
    {
        var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                return null;
            }

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return existingRole;
    }

    /// <summary>
    /// Delete a Role by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
    
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search Roles by term with pagination.
    /// </summary>
    public async Task<PagedResult<Role>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Roles
            .Where(p => p.Name.Contains(term));

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