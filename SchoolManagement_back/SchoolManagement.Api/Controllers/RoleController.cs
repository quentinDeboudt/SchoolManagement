using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Interfaces;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    // Constructor for injecting the Role service.
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // Get the total count of roles.
    // Returns: An ActionResult containing the total count of roles as an integer.
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetRolesCount()
    {
        var count = await _roleService.CountAsync();
        return Ok(count);
    }

    // Get all roles without pagination.
    // Returns: An ActionResult containing a list of all roles.
    [HttpGet]
    public ActionResult<IEnumerable<Role>> GetAll()
    {
        var roles = _roleService.GetAll();
        return Ok(roles);
    }

    // Get roles with pagination.
    // Parameters:
    //   - pageNumber: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: A Task containing a list of roles for the specified page.
    [HttpGet("pagination")]
    public Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
    {
        var roles = _roleService.GetWithPagination(pageNumber, pageSize);
        return roles;
    }

    // Get a specific role by ID.
    // Parameters:
    //   - id: The ID of the role to retrieve (int).
    // Returns: An ActionResult containing the role with the specified ID.
    //          Returns 404 Not Found if the role is not found.
    [HttpGet("{id}")]
    public ActionResult<Role> GetById(int id)
    {
        var role = _roleService.GetById(id);
        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    // Create a new role.
    // Parameters:
    //   - role: The role entity to create (Role).
    // Returns: An ActionResult indicating the result of the creation.
    //          Returns 201 Created with the URI of the new role.
    [HttpPost]
    public ActionResult Create([FromBody] Role role)
    {
        _roleService.CreateAsync(role);
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    // Update an existing role.
    // Parameters:
    //   - role: The updated role entity (Role).
    // Returns: An IActionResult indicating the result of the update.
    //          Returns 200 OK with the updated role, or 404 Not Found if the role does not exist.
    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromBody] Role role)
    {
        if (role == null || role.Id <= 0)
        {
            return BadRequest("Invalid role data");
        }

        var updatedRole = await _roleService.UpdateRoleAsync(role);
        if (updatedRole == null)
        {
            return NotFound("Role not found");
        }

        return Ok(updatedRole);
    }

    // Delete a role by ID.
    // Parameters:
    //   - id: The ID of the role to delete (int).
    // Returns: An ActionResult indicating the result of the deletion.
    //          Returns 204 No Content if successful.
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {
        _roleService.DeleteAsync(id);
    }

    // Search roles by term with pagination.
    // Parameters:
    //   - term: The search term to filter roles (string).
    //   - pageIndex: The page number to retrieve (int).
    //   - pageSize: The number of items per page (int).
    // Returns: An IActionResult containing the search results.
    [HttpGet("search")]
    public IActionResult SearchRoles(string term, int pageIndex, int pageSize)
    {
        var result = _roleService.SearchRoles(term, pageIndex, pageSize);
        return Ok(result);
    }
}
