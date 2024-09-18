
namespace SchoolManagement.Domain.IRepository;
public interface IGroupRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Group>> GetAllAsync();
    Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    Task<Group> GetByIdAsync(int id);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
    Task DeleteAsync(int id);
    Task<PagedResult<Group>> Search();
}
