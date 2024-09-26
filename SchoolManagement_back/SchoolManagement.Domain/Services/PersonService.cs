using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;
public class PersonService: IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    // Get the total count of persons.
    // Returns: An ActionResult containing the total count of persons as an integer.
    public async Task<int> CountAsync(){
        return await _repository.CountAsync();
    }

    // Get all persons without pagination.
    // Returns: An ActionResult containing a list of all persons.
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Get persons with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of persons for the specified page.
    public async Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    // Get a specific person by ID.
    // Parameters:
    //   - id: The ID of the person to retrieve (int).
    // Returns: An ActionResult containing the person with the specified ID.
    public Task<Person> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    // Create a new person.
    // Parameters:
    //   - person: The person entity to create (Person).
    // Returns: An ActionResult indicating the result of the creation.
    public async Task<int> AddAsync(Person person)
    {
        return await _repository.AddAsync(person);
    }

    // Update an existing person.
    // Parameters:
    //   - person: The updated person entity (Person).
    // Returns: An IActionResult indicating the result of the update.
    public async Task<Person> UpdateAsync(Person person)
    {
        return await _repository.UpdateAsync(person);
    }

    // Delete a person by ID.
    // Parameters:
    //   - id: The ID of the person to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    public void DeleteAsync(int id)
    {
        _repository.DeleteAsync(id);
    }

    public async Task<PagedResult<Person>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }

}
