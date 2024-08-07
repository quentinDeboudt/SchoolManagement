using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.Application.Interfaces
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetAll();
        Subject GetById(int id);
        void Create(Subject subject);
        void Update(int id, Subject subject);
        void Delete(int id);
    }
}
