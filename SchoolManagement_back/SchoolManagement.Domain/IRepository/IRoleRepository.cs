using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolManagement.Domain.IRepository;
public interface IRoleRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Role>> GetAllAsync();
    Task<List<Role>> GetWithPagination(int pageNumber, int pageSize);
    Task<Role> GetByIdAsync(int id);
    void AddAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    void DeleteAsync(int id);
    Task<PagedResult<Role>> Search(string term, int pageIndex, int pageSize);
}
