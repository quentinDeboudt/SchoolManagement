using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Group>> GetAll()
        {
            var groups = _groupService.GetAll();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public ActionResult<Group> GetById(int id)
        {
            var group = _groupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Group group)
        {
            if (group == null)
            {
                return BadRequest("Group object is null");
            }

            _groupService.Create(group);
            return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Group group)
        {
            if (group == null)
            {
                return BadRequest("Group object is null");
            }

            var existingGroup = _groupService.GetById(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            _groupService.Update(id, group);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingGroup = _groupService.GetById(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            _groupService.Delete(id);
            return NoContent();
        }
    }
}
