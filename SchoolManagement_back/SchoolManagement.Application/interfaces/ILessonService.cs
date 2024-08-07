using SchoolManagement.Domain.Entities;
using System.Collections.Generic;

namespace SchoolManagement.Application.Interfaces
{
    public interface ILessonService
    {
        IEnumerable<Lesson> GetAll();
        Lesson GetById(int id);
        void Create(Lesson lesson);
        void Update(int id, Lesson lesson);
        void Delete(int id);
    }
}
