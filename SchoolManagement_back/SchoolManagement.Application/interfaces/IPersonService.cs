using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Interfaces;
public interface IPersonService
{
    // Get the total number of persons.
    Task<int> CountAsync();

    // Get all persons without pagination.
    IEnumerable<Person> GetAll();

    // Get persons with pagination.
    Task<List<Person>> GetWithPagination(int pageNumber, int pageSize);

    // Get a specific person by ID.
    Person GetById(int id);

    // Create a new person.
    void CreateAsync(Person person);

    // Update an existing person asynchronously.
    Task<Person> UpdatePersonAsync(Person person);

    // Delete a person by ID.
    void DeleteAsync(int id);

    // Search persons by term with pagination.
    Task<PagedResult<Person>> SearchPersons(string term, int pageIndex, int pageSize);
}
