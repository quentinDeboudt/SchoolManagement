using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.Application.Interfaces
{
    public interface IClassroomService
    {
        IEnumerable<Classroom> GetAll();
        Classroom GetById(int id);
        void Create(Classroom classroom);
        void Update(int id, Classroom classroom);
        void Delete(int id);
    }
}
