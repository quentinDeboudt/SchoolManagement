using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.Application.Interfaces
{
    public interface IGroupService
    {
        IEnumerable<Group> GetAll();
        Group GetById(int id);
        void Create(Group group);
        void Update(int id, Group group);
        void Delete(int id);
    }
}
