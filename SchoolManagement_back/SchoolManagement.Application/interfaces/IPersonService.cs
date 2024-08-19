using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Interfaces;

public interface IPersonService
{
    IEnumerable<Person> GetAll();
    Task<List<Person>> GetWithPagination(int pageNumber, int pageSize);
    Person GetById(int id);
    void Create(Person person);
    void Update(int id, Person person);
    void Delete(int id);
    Task<int> CountAsync();
}