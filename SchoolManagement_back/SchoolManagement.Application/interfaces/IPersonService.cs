using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces;

public interface IPersonService
{
    IEnumerable<Person> GetAll();
    Person GetById(int id);
    void Create(Person person);
    void Update(int id, Person person);
    void Delete(int id);
}