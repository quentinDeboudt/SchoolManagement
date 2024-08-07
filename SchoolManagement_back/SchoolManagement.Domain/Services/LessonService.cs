using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Services
{
    public class LessonService : ILessonService
    {
        private readonly SchoolManagementDbContext _context;

        public LessonService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teachers)
                .Include(l => l.Groups)
                .ToList();
        }

        public Lesson GetById(int id)
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teachers)
                .Include(l => l.Groups)
                .FirstOrDefault(l => l.Id == id);
        }

        public void Create(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
        }

        public void Update(int id, Lesson lesson)
        {
            var existingLesson = _context.Lessons.Find(id);
            if (existingLesson == null) return;

            existingLesson.SubjectId = lesson.SubjectId;
            existingLesson.Subject = lesson.Subject;
            existingLesson.Teachers = lesson.Teachers;
            existingLesson.Groups = lesson.Groups;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null) return;

            _context.Lessons.Remove(lesson);
            _context.SaveChanges();
        }
    }
}
