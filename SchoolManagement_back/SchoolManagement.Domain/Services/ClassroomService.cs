using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly SchoolManagementDbContext _context;

        public ClassroomService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Classroom> GetAll()
        {
            return _context.Classrooms
                .Include(c => c.Groups)
                .Include(c => c.Teachers)
                .ToList();
        }

        public Classroom GetById(int id)
        {
            return _context.Classrooms
                .Include(c => c.Groups)
                .Include(c => c.Teachers)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Create(Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
        }

        public void Update(int id, Classroom classroom)
        {
            var existingClassroom = _context.Classrooms.Find(id);
            if (existingClassroom == null) return;

            existingClassroom.Name = classroom.Name;
            existingClassroom.Groups = classroom.Groups;
            existingClassroom.Teachers = classroom.Teachers;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var classroom = _context.Classrooms.Find(id);
            if (classroom == null) return;

            _context.Classrooms.Remove(classroom);
            _context.SaveChanges();
        }
    }
}
