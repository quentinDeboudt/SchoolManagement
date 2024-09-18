
namespace SchoolManagement.Domain.IRepository;
public interface IClassroomRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Classroom>> GetAllAsync();
    Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    Task<Classroom> GetByIdAsync(int id);
    Task AddAsync(Classroom classroom);
    Task UpdateAsync(Classroom classroom);
    Task DeleteAsync(int id);
    Task<PagedResult<Classroom>> Search();
}
