using SchoolManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolManagement.Domain.IRepository;
public interface IPersonRepository
{
    Task<int> CountAsync();
    Task<IEnumerable<Person>> GetAllAsync();
    Task<List<Person>> GetWithPagination(int pageNumber, int pageSize);
    Task<Person> GetByIdAsync(int id);
    Task<int> AddAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    void DeleteAsync(int id);
    Task<PagedResult<Person>> Search(string term, int pageIndex, int pageSize);
}
