using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Subject>> GetAll()
        {
            var subjects = _subjectService.GetAll();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public ActionResult<Subject> GetById(int id)
        {
            var subject = _subjectService.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Subject subject)
        {
            if (subject == null)
            {
                return BadRequest("Subject object is null");
            }

            _subjectService.Create(subject);
            return CreatedAtAction(nameof(GetById), new { id = subject.Id }, subject);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Subject subject)
        {
            if (subject == null)
            {
                return BadRequest("Subject object is null");
            }

            var existingSubject = _subjectService.GetById(id);
            if (existingSubject == null)
            {
                return NotFound();
            }

            _subjectService.Update(id, subject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingSubject = _subjectService.GetById(id);
            if (existingSubject == null)
            {
                return NotFound();
            }

            _subjectService.Delete(id);
            return NoContent();
        }
    }
}
