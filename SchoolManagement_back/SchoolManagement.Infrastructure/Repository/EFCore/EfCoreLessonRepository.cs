using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCoreLessonRepository : ILessonRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCoreLessonRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Lessons asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Lessons.CountAsync();
    }

    /// <summary>
    /// Get all Lessons with related entities.
    /// </summary>
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _context.Lessons
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get Lessons with pagination asynchronously.
    /// </summary>
    public async Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Lessons
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Subject)
            .Include(p => p.Teachers)
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Lesson by ID.
    /// </summary>
    public async Task<Lesson> GetByIdAsync(int id)
    {
        return await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
    }

    /// <summary>
    /// Create a new Lesson asynchronously.
    /// </summary>
    public void AddAsync(Lesson Lesson)
    {
         _context.Lessons.AddAsync(Lesson);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Lesson asynchronously.
    /// </summary>
    public async Task<Lesson> UpdateAsync(Lesson lesson)
    {
        var existingLesson = await _context.Lessons.FindAsync(lesson.Id);
            if (existingLesson == null)
            {
                return null;
            }

            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();

            return existingLesson;
    }

    /// <summary>
    /// Delete a Lesson by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
    
        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search Lessons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Lesson>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Lessons
            .Include(l => l.Subject)
            .Where(p => p.Subject.Name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Lesson>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}