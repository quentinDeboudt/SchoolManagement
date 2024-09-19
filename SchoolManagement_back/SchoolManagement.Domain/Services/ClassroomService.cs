using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _repository;

    public ClassroomService(IClassroomRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get the total count of classrooms asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    /// <summary>
    /// Get all classrooms.
    /// </summary>
    public Task<IEnumerable<Classroom>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Get classrooms with pagination asynchronously.
    /// </summary>
    public async Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific classroom by ID.
    /// </summary>
    public Task<Classroom> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new classroom asynchronously.
    /// </summary>
    public void AddAsync(Classroom classroom)
    {
       _repository.AddAsync(classroom);
    }

    /// <summary>
    /// Update an existing classroom asynchronously.
    /// </summary>
    public async Task<Classroom> UpdateAsync(Classroom classroom)
    {
        return await _repository.UpdateAsync(classroom);
    }

    /// <summary>
    /// Delete a classroom by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search classrooms by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }
}
