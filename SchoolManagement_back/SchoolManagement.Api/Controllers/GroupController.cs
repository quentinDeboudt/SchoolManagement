using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Services;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly GroupService _groupService;

    // Constructor for injecting the Group service.
    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }

    // Get the total count of groups.
    // Returns: An ActionResult containing the total count of groups as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetGroupsCount()
    {
        var count = await _groupService.CountAsync();
        return Ok(count);
    }

    // Get all groups without pagination.
    // Returns: An ActionResult containing a list of all groups.
    [HttpGet]
    public ActionResult<IEnumerable<Group>> GetAll()
    {
        var groups = _groupService.GetAllAsync();
        return Ok(groups);
    }

    // Get groups with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of groups for the specified page.
    [HttpGet("pagination")]
    public Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
        var groups = _groupService.GetWithPagination(pageNumber, pageSize);
        return groups;
    }

    // Get a specific group by ID.
    // Parameters:
    //   - id: The ID of the group to retrieve (int).
    // Returns: An ActionResult containing the group with the specified ID.
    //          Returns 404 Not Found if the group is not found.
    [HttpGet("{id}")]
    public ActionResult<Group> GetById(int id)
    {
        var group = _groupService.GetByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }

    // Create a new group.
    // Parameters:
    //   - group: The group entity to create (Group).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new group.
    [HttpPost]
    public ActionResult Create([FromBody] Group group)
    {
        _groupService.AddAsync(group);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    // Update an existing group.
    // Parameters:
    //   - group: The updated group entity (Group).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated group, or 404 Not Found if the group does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdateGroup([FromBody] Group group)
    {
        if (group == null || group.Id <= 0)
        {
            return BadRequest("Invalid group data");
        }

        var updatedGroup = await _groupService.UpdateAsync(group);
        if (updatedGroup == null)
        {
            return NotFound("Group not found");
        }

        return Ok(updatedGroup);
    }

    // Delete a group by ID.
    // Parameters:
    //   - id: The ID of the group to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {
        _groupService.DeleteAsync(id);
    }

    // Search groups by term with pagination.
    // Parameters:
    //   - term: The search term to filter groups (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public Task<PagedResult<Group>> SearchGroups(string term, int pageIndex, int pageSize)
    {
        var result = _groupService.Search(term, pageIndex, pageSize);
        return result;
    }
}
