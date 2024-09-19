using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCoreSubjectRepository : ISubjectRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCoreSubjectRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Subjects asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Subjects.CountAsync();
    }

    /// <summary>
    /// Get all Subjects with related entities.
    /// </summary>
    public async Task<IEnumerable<Subject>> GetAllAsync()
    {
        return await _context.Subjects.ToListAsync();
    }

    /// <summary>
    /// Get Subjects with pagination asynchronously.
    /// </summary>
    public async Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Subjects
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Subject by ID.
    /// </summary>
    public async Task<Subject> GetByIdAsync(int id)
    {
        return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Create a new Subject asynchronously.
    /// </summary>
    public void AddAsync(Subject Subject)
    {
         _context.Subjects.AddAsync(Subject);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Subject asynchronously.
    /// </summary>
    public async Task<Subject> UpdateAsync(Subject Subject)
    {
        var existingSubject = await _context.Subjects.FindAsync(Subject.Id);
        if (existingSubject == null)
        {
            return null;
        }

        _context.Subjects.Update(Subject);
        await _context.SaveChangesAsync();

        return existingSubject;
    }

    /// <summary>
    /// Delete a Subject by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
    
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search Subjects by term with pagination.
    /// </summary>
    public async Task<PagedResult<Subject>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Subjects
            .Where(p => p.Name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Subject>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}