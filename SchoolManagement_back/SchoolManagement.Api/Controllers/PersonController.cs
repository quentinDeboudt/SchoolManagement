using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Interfaces;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetPersonsCount()
    {
        var count = await _personService.CountAsync();
        return Ok(count);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        var persons = _personService.GetAll();
        return Ok(persons);
    }

    [HttpGet("pagination")]
    public Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        var persons = _personService.GetWithPagination(pageNumber, pageSize);
        return persons;
    }

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

    [HttpPost]
    public ActionResult Create([FromBody] Person person)
    {
        _personService.Create(person);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

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

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _personService.Delete(id);
        return NoContent();
    }
}
