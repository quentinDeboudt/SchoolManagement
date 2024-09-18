
namespace SchoolManagement.Domain.IRepository;
public interface ILessonRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
    Task<Lesson> GetByIdAsync(int id);
    Task AddAsync(Lesson lesson);
    Task UpdateAsync(Lesson lesson);
    Task DeleteAsync(int id);
    Task<PagedResult<Lesson>> Search();
}
