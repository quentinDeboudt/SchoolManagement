using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace SchoolManagement.Domain.Services;

public class PersonService : IPersonService
{
    private readonly SchoolManagementDbContext _context;

    public PersonService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Persons.CountAsync();
    }

    public IEnumerable<Person> GetAll()
    {
        return _context.Persons
            .Include(p => p.Roles)
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .ToList();
    }

    public Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        return _context.Persons
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Person GetById(int id)
    {
        return _context.Persons
            .Include(p => p.Roles)
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .FirstOrDefault(p => p.Id == id);
    }

    public void Create(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var existingPerson = await _context.Persons.FindAsync(person.Id);
        if (existingPerson == null)
        {
            return null;
        }

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Roles = person.Roles;
        existingPerson.StudentGroups = person.StudentGroups;
        existingPerson.TeacherClassrooms = person.TeacherClassrooms;
        existingPerson.TeacherLessons = person.TeacherLessons;

        _context.Persons.Update(existingPerson);
        await _context.SaveChangesAsync();

        return existingPerson;
    }

    public void Delete(int id)
    {
        var person = _context.Persons.Find(id);
        if (person == null) return;

        _context.Persons.Remove(person);
        _context.SaveChanges();
    }

    public PagedResult<Person> SearchPersons(string term, int pageIndex, int pageSize)
    {
        var query = _context.Persons
            .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term));

        var totalCount = query.Count();

        var items = query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<Person>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
