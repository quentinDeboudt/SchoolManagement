using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.IRepository;
public interface IGroupRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Group>> GetAllAsync();
    Task<List<Group>> GetWithPagination(int pageNumber, int pageSize);
    Task<Group> GetByIdAsync(int id);
    void AddAsync(Group group);
    Task<Group> UpdateAsync(Group group);
    void DeleteAsync(int id);
    Task<PagedResult<Group>> Search(string term, int pageIndex, int pageSize);
}
