using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Classroom>> GetAll()
        {
            var classrooms = _classroomService.GetAll();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public ActionResult<Classroom> GetById(int id)
        {
            var classroom = _classroomService.GetById(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return Ok(classroom);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Classroom classroom)
        {
            if (classroom == null)
            {
                return BadRequest("Classroom object is null");
            }

            _classroomService.Create(classroom);
            return CreatedAtAction(nameof(GetById), new { id = classroom.Id }, classroom);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Classroom classroom)
        {
            if (classroom == null)
            {
                return BadRequest("Classroom object is null");
            }

            var existingClassroom = _classroomService.GetById(id);
            if (existingClassroom == null)
            {
                return NotFound();
            }

            _classroomService.Update(id, classroom);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingClassroom = _classroomService.GetById(id);
            if (existingClassroom == null)
            {
                return NotFound();
            }

            _classroomService.Delete(id);
            return NoContent();
        }
    }
}
