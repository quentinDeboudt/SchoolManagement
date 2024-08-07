using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.Application.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Create(Role role);
        void Update(int id, Role role);
        void Delete(int id);
    }
}
