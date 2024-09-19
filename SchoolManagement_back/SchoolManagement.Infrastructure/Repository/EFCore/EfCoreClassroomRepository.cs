using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;  
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCoreClassroomRepository : IClassroomRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCoreClassroomRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Classrooms asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Classrooms.CountAsync();
    }

    /// <summary>
    /// Get all Classrooms with related entities.
    /// </summary>
    public async Task<IEnumerable<Classroom>> GetAllAsync()
    {
        return await _context.Classrooms
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get Classrooms with pagination asynchronously.
    /// </summary>
    public async Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Classrooms
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Classroom by ID.
    /// </summary>
    public async Task<Classroom> GetByIdAsync(int id)
    {
        return await _context.Classrooms
            .FirstOrDefaultAsync(p => p.Id == id);
            
    }

    /// <summary>
    /// Create a new Classroom asynchronously.
    /// </summary>
    public void AddAsync(Classroom classroom)
    {
        _context.Classrooms.AddAsync(classroom);
        _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Classroom asynchronously.
    /// </summary>
    public async Task<Classroom> UpdateAsync(Classroom classroom)
    {
        var existingClassroom = await _context.Classrooms.FindAsync(classroom.Id);
        if (existingClassroom == null)
        {
            return null;
        }

        _context.Classrooms.Update(classroom);
        await _context.SaveChangesAsync();

        return existingClassroom;
    }

    /// <summary>
    /// Delete a Classroom by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {
        var classrooms = await _context.Classrooms.FindAsync(id);
    
        if (classrooms != null)
        {
           _context.Classrooms.Remove(classrooms);
           await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search Classrooms by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Classrooms
            .Where(c => c.Name.Contains(term));

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Classroom>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}