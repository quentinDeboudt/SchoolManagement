using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;
public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _repository;

    public SubjectService(ISubjectRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get the total count of subjects asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    /// <summary>
    /// Get all subjects.
    /// </summary>
    public Task<IEnumerable<Subject>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Get subjects with pagination asynchronously.
    /// </summary>
    public async Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific subject by ID.
    /// </summary>
    public Task<Subject> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new subject asynchronously.
    /// </summary>
    public void AddAsync(Subject subject)
    {
        _repository.AddAsync(subject);
    }

    /// <summary>
    /// Update an existing subject asynchronously.
    /// </summary>
    public async Task<Subject> UpdateAsync(Subject subject)
    {
        return await _repository.UpdateAsync(subject);
    }

    /// <summary>
    /// Delete a subject by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search subjects by term with pagination.
    /// </summary>
    public async Task<PagedResult<Subject>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }   
}
