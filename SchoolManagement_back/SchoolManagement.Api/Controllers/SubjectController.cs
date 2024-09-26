using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Services;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    // Constructor for injecting the Subject service.
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    // Get the total count of subjects.
    // Returns: An ActionResult containing the total count of subjects as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetSubjectsCount()
    {
        var count = await _subjectService.CountAsync();
        return Ok(count);
    }

    // Get all subjects without pagination.
    // Returns: An ActionResult containing a list of all subjects.
    [HttpGet]
    public async Task<IEnumerable<Subject>> GetAll()
    {
        return await _subjectService.GetAllAsync();
    }

    // Get subjects with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of subjects for the specified page.
    [HttpGet("pagination")]
    public Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    {
        var subjects = _subjectService.GetWithPagination(pageNumber, pageSize);
        return subjects;
    }

    // Get a specific subject by ID.
    // Parameters:
    //   - id: The ID of the subject to retrieve (int).
    // Returns: An ActionResult containing the subject with the specified ID.
    //          Returns 404 Not Found if the subject is not found.
    [HttpGet("{id}")]
    public ActionResult<Subject> GetById(int id)
    {
        var subject = _subjectService.GetByIdAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    // Create a new subject.
    // Parameters:
    //   - subject: The subject entity to create (Subject).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new subject.
    [HttpPost]
    public ActionResult Create([FromBody] Subject subject)
    {
        _subjectService.AddAsync(subject);
        return CreatedAtAction(nameof(GetById), new { id = subject.Id }, subject);
    }

    // Update an existing subject.
    // Parameters:
    //   - subject: The updated subject entity (Subject).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated subject, or 404 Not Found if the subject does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdateSubject([FromBody] Subject subject)
    {
        if (subject == null || subject.Id <= 0)
        {
            return BadRequest("Invalid subject data");
        }

        var updatedSubject = await _subjectService.UpdateAsync(subject);
        if (updatedSubject == null)
        {
            return NotFound("Subject not found");
        }

        return Ok(updatedSubject);
    }

    // Delete a subject by ID.
    // Parameters:
    //   - id: The ID of the subject to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {
        _subjectService.DeleteAsync(id);
    }

    // Search subjects by term with pagination.
    // Parameters:
    //   - term: The search term to filter subjects (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public Task<PagedResult<Subject>> SearchSubjects(string term, int pageIndex, int pageSize)
    {
        var result = _subjectService.Search(term, pageIndex, pageSize);
        return result;
    }
}
