using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolManagement.Application.Interfaces;
public interface ILessonService
{
    Task<int> CountAsync();
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize);
    Task<Lesson> GetByIdAsync(int id);
    void AddAsync(Lesson lesson);
    Task<Lesson> UpdateAsync(Lesson lesson);
    void DeleteAsync(int id);
    Task<PagedResult<Lesson>> Search(string term, int pageIndex, int pageSize);
}
