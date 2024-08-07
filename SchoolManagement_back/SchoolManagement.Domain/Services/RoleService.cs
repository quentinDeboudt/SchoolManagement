using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly SchoolManagementDbContext _context;

        public RoleService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public void Create(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Update(int id, Role role)
        {
            var existingRole = _context.Roles.Find(id);
            if (existingRole == null) return;

            existingRole.Name = role.Name;
            existingRole.Persons = role.Persons;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return;

            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
