
namespace SchoolManagement.Domain.IRepository;
public interface IRoleRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Person>> GetAllAsync();
    Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
    Task<Role> GetByIdAsync(int id);
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(int id);
    Task<PagedResult<Person>> Search();
}
