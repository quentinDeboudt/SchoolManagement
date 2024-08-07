using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

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

        [HttpPost]
        public ActionResult Create([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Role object is null");
            }

            _roleService.Create(role);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Role object is null");
            }

            var existingRole = _roleService.GetById(id);
            if (existingRole == null)
            {
                return NotFound();
            }

            _roleService.Update(id, role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingRole = _roleService.GetById(id);
            if (existingRole == null)
            {
                return NotFound();
            }

            _roleService.Delete(id);
            return NoContent();
        }
    }
}
