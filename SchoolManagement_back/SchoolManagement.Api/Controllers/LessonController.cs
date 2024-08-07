using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> GetAll()
        {
            var lessons = _lessonService.GetAll();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public ActionResult<Lesson> GetById(int id)
        {
            var lesson = _lessonService.GetById(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Lesson lesson)
        {
            if (lesson == null)
            {
                return BadRequest("Lesson object is null");
            }

            _lessonService.Create(lesson);
            return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Lesson lesson)
        {
            if (lesson == null)
            {
                return BadRequest("Lesson object is null");
            }

            var existingLesson = _lessonService.GetById(id);
            if (existingLesson == null)
            {
                return NotFound();
            }

            _lessonService.Update(id, lesson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingLesson = _lessonService.GetById(id);
            if (existingLesson == null)
            {
                return NotFound();
            }

            _lessonService.Delete(id);
            return NoContent();
        }
    }
}
