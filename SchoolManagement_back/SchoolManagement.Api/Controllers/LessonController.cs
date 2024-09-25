using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Services;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    // Constructor for injecting the Lesson service.
    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    // Get the total count of lessons.
    // Returns: An ActionResult containing the total count of lessons as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetLessonsCount()
    {
        var count = await _lessonService.CountAsync();
        return Ok(count);
    }

    // Get all lessons without pagination.
    // Returns: An ActionResult containing a list of all lessons.
    [HttpGet]
    public ActionResult<IEnumerable<Lesson>> GetAll()
    {
        var lessons = _lessonService.GetAllAsync();
        return Ok(lessons);
    }

    // Get lessons with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of lessons for the specified page.
    [HttpGet("pagination")]
    public Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
    {
        var lessons = _lessonService.GetWithPagination(pageNumber, pageSize);
        return lessons;
    }

    // Get a specific lesson by ID.
    // Parameters:
    //   - id: The ID of the lesson to retrieve (int).
    // Returns: An ActionResult containing the lesson with the specified ID.
    //          Returns 404 Not Found if the lesson is not found.
    [HttpGet("{id}")]
    public ActionResult<Lesson> GetById(int id)
    {
        var lesson = _lessonService.GetByIdAsync(id);
        if (lesson == null)
        {
            return NotFound();
        }

        return Ok(lesson);
    }

    // Create a new lesson.
    // Parameters:
    //   - lesson: The lesson entity to create (Lesson).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new lesson.
    [HttpPost]
    public ActionResult Create([FromBody] Lesson lesson)
    {
        _lessonService.AddAsync(lesson);
        return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, lesson);
    }

    // Update an existing lesson.
    // Parameters:
    //   - lesson: The updated lesson entity (Lesson).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated lesson, or 404 Not Found if the lesson does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdateLesson([FromBody] Lesson lesson)
    {
        if (lesson == null || lesson.Id <= 0)
        {
            return BadRequest("Invalid lesson data");
        }

        var updatedLesson = await _lessonService.UpdateAsync(lesson);
        if (updatedLesson == null)
        {
            return NotFound("Lesson not found");
        }

        return Ok(updatedLesson);
    }

    // Delete a lesson by ID.
    // Parameters:
    //   - id: The ID of the lesson to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {
        _lessonService.DeleteAsync(id);
    }

    // Search lessons by term with pagination.
    // Parameters:
    //   - term: The search term to filter lessons (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public Task<PagedResult<Lesson>> SearchLessons(string term, int pageIndex, int pageSize)
    {
        var result = _lessonService.Search(term, pageIndex, pageSize);
        return result;
    }
}
