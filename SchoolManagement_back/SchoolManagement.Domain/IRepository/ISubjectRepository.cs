using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolManagement.Domain.IRepository;
public interface ISubjectRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize);
    Task<Subject> GetByIdAsync(int id);
    void AddAsync(Subject subject);
    Task<Subject> UpdateAsync(Subject subject);
    void DeleteAsync(int id);
    Task<PagedResult<Subject>> Search(string term, int pageIndex, int pageSize);
}
