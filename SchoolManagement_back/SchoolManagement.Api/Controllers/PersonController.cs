using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Interfaces;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    // Constructor for injecting the Person service.
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    // Get the total count of persons.
    // Returns: An ActionResult containing the total count of persons as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetPersonsCount()
    {
        var count = await _personService.CountAsync();
        return Ok(count);
    }

    // Get all persons without pagination.
    // Returns: An ActionResult containing a list of all persons.
    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        var persons = _personService.GetAll();
        return Ok(persons);
    }

    // Get persons with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of persons for the specified page.
    [HttpGet("pagination")]
    public Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        var persons = _personService.GetWithPagination(pageNumber, pageSize);
        return persons;
    }

    // Get a specific person by ID.
    // Parameters:
    //   - id: The ID of the person to retrieve (int).
    // Returns: An ActionResult containing the person with the specified ID.
    //          Returns 404 Not Found if the person is not found.
    [HttpGet("{id}")]
    public ActionResult<Person> GetById(int id)
    {
        var person = _personService.GetById(id);
        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    // Create a new person.
    // Parameters:
    //   - person: The person entity to create (Person).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new person.
    [HttpPost]
    public ActionResult Create([FromBody] Person person)
    {
        _personService.CreateAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    // Update an existing person.
    // Parameters:
    //   - person: The updated person entity (Person).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated person, or 404 Not Found if the person does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] Person person)
    {
        if (person == null || person.Id <= 0)
        {
            return BadRequest("Invalid person data");
        }

        var updatedPerson = await _personService.UpdatePersonAsync(person);
        if (updatedPerson == null)
        {
            return NotFound("Person not found");
        }

        return Ok(updatedPerson);
    }

    // Delete a person by ID.
    // Parameters:
    //   - id: The ID of the person to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {
        _personService.DeleteAsync(id);
    }

    // Search persons by term with pagination.
    // Parameters:
    //   - term: The search term to filter persons (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public IActionResult SearchPersons(string term, int pageIndex, int pageSize)
    {
        var result = _personService.SearchPersons(term, pageIndex, pageSize);
        return Ok(result);
    }
}
