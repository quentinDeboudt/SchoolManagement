
namespace SchoolManagement.Domain.IRepository;
public interface ISubjectRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    Task<Subject> GetByIdAsync(int id);
    Task AddAsync(Subject subject);
    Task UpdateAsync(Subject subject);
    Task DeleteAsync(int id);
    Task<PagedResult<Subject>> Search();
}
