using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.IRepository;
public interface IClassroomRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Classroom>> GetAllAsync();
    Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize);
    Task<Classroom> GetByIdAsync(int id);
    void AddAsync(Classroom classroom);
    Task<Classroom> UpdateAsync(Classroom classroom);
    void DeleteAsync(int id);
    Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize);
}
