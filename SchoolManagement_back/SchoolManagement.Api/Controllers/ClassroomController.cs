using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Services;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomController : ControllerBase
{
    private readonly IClassroomService _classroomService;

    // Constructor for injecting the Classroom service.
    public ClassroomController(IClassroomService classroomService)
    {
        _classroomService = classroomService;
    }

    // Get the total count of classrooms.
    // Returns: An ActionResult containing the total count of classrooms as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetClassroomsCount()
    {
        var count = await _classroomService.CountAsync();
        return Ok(count);
    }

    // Get all classrooms without pagination.
    // Returns: An ActionResult containing a list of all classrooms.
    [HttpGet]
    public ActionResult<IEnumerable<Classroom>> GetAll()
    {
        var classrooms = _classroomService.GetAllAsync();
        return Ok(classrooms);
    }

    // Get classrooms with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of classrooms for the specified page.
    [HttpGet("pagination")]
    public Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        var classrooms = _classroomService.GetWithPagination(pageNumber, pageSize);
        return classrooms;
    }

    // Get a specific classroom by ID.
    // Parameters:
    //   - id: The ID of the classroom to retrieve (int).
    // Returns: An ActionResult containing the classroom with the specified ID.
    //          Returns 404 Not Found if the classroom is not found.
    [HttpGet("{id}")]
    public ActionResult<Classroom> GetById(int id)
    {
        var classroom = _classroomService.GetByIdAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }

        return Ok(classroom);
    }

    // Create a new classroom.
    // Parameters:
    //   - classroom: The classroom entity to create (Classroom).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new classroom.
    [HttpPost]
    public ActionResult Create([FromBody] Classroom classroom)
    {
        _classroomService.AddAsync(classroom);
        return CreatedAtAction(nameof(GetById), new { id = classroom.Id }, classroom);
    }

    // Update an existing classroom.
    // Parameters:
    //   - classroom: The updated classroom entity (Classroom).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated classroom, or 404 Not Found if the classroom does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdateClassroom([FromBody] Classroom classroom)
    {
        if (classroom == null || classroom.Id <= 0)
        {
            return BadRequest("Invalid classroom data");
        }

        var updatedClassroom = await _classroomService.UpdateAsync(classroom);
        if (updatedClassroom == null)
        {
            return NotFound("Classroom not found");
        }

        return Ok(updatedClassroom);
    }

    // Delete a classroom by ID.
    // Parameters:
    //   - id: The ID of the classroom to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void DeleteAsync(int id)
    {
        _classroomService.DeleteAsync(id);
    }

    // Search classrooms by term with pagination.
    // Parameters:
    //   - term: The search term to filter classrooms (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public Task<PagedResult<Classroom>> SearchClassrooms(string term, int pageIndex, int pageSize)
    {
        var result = _classroomService.Search(term, pageIndex, pageSize);
        return result;
    }
}
