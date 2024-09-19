using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCorePersonRepository : IPersonRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCorePersonRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of persons asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Persons.CountAsync();
    }

    /// <summary>
    /// Get all persons with related entities.
    /// </summary>
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons
            .Include(p => p.Roles)
            .ToListAsync();
    }

    /// <summary>
    /// Get persons with pagination asynchronously.
    /// </summary>
    public async Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Persons
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Roles)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific person by ID.
    /// </summary>
    public async Task<Person> GetByIdAsync(int id)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Create a new person asynchronously.
    /// </summary>
    public void AddAsync(Person person)
    {
         _context.Persons.AddAsync(person);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing person asynchronously.
    /// </summary>
    public async Task<Person> UpdateAsync(Person person)
    {
        var existingPerson = await _context.Persons.FindAsync(person.Id);
        if (existingPerson == null)
        {
            return null;
        }

        _context.Persons.Update(person);
        await _context.SaveChangesAsync();

        return existingPerson;
    }

    /// <summary>
    /// Delete a person by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {

        var person = await _context.Persons.FindAsync(id);

        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Search persons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Person>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Persons
            .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Person>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}