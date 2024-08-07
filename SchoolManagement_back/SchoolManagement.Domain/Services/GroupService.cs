using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Services
{
    public class GroupService : IGroupService
    {
        private readonly SchoolManagementDbContext _context;

        public GroupService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Group> GetAll()
        {
            return _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Lessons)
                .Include(g => g.Classroom)
                .ToList();
        }

        public Group GetById(int id)
        {
            return _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Lessons)
                .Include(g => g.Classroom)
                .FirstOrDefault(g => g.Id == id);
        }

        public void Create(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public void Update(int id, Group group)
        {
            var existingGroup = _context.Groups.Find(id);
            if (existingGroup == null) return;

            existingGroup.Name = group.Name;
            existingGroup.ClassroomId = group.ClassroomId;
            existingGroup.Classroom = group.Classroom;
            existingGroup.Students = group.Students;
            existingGroup.Lessons = group.Lessons;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null) return;

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }
    }
}
