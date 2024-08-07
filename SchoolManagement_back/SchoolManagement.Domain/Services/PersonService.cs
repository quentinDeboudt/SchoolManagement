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

    public IEnumerable<Person> GetAll()
    {
        return _context.Persons
            .Include(p => p.Roles)
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .ToList();
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

    public void Update(int id, Person person)
    {
        var existingPerson = _context.Persons.Find(id);
        if (existingPerson == null) return;

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Roles = person.Roles;
        existingPerson.StudentGroups = person.StudentGroups;
        existingPerson.TeacherClassrooms = person.TeacherClassrooms;
        existingPerson.TeacherLessons = person.TeacherLessons;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var person = _context.Persons.Find(id);
        if (person == null) return;

        _context.Persons.Remove(person);
        _context.SaveChanges();
    }
}
