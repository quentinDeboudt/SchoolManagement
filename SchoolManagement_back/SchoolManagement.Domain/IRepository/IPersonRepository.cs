
namespace SchoolManagement.Domain.IRepository;
public interface IPersonRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Person>> GetAllAsync();
    Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    Task<Person> GetByIdAsync(int id);
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(int id);
    Task<PagedResult<Person>> Search();
}
