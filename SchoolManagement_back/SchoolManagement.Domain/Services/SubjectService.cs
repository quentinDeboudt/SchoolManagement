using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly SchoolManagementDbContext _context;

        public SubjectService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Subject> GetAll()
        {
            return _context.Subjects
                .Include(s => s.Lessons)
                .ToList();
        }

        public Subject GetById(int id)
        {
            return _context.Subjects
                .Include(s => s.Lessons)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Create(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        public void Update(int id, Subject subject)
        {
            var existingSubject = _context.Subjects.Find(id);
            if (existingSubject == null) return;

            existingSubject.Name = subject.Name;
            existingSubject.Lessons = subject.Lessons;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return;

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }
    }
}
