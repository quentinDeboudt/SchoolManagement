using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;

namespace SchoolManagement.Domain.Services;
public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;

    public LessonService(ILessonRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get the total count of lessons asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    /// <summary>
    /// Get all lessons.
    /// </summary>
    public Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    /// <summary>
    /// Get lessons with pagination asynchronously.
    /// </summary>
    public async Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific lesson by ID.
    /// </summary>
    public Task<Lesson> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new lesson asynchronously.
    /// </summary>
    public void AddAsync(Lesson lesson)
    {
        _repository.AddAsync(lesson);
    }

    /// <summary>
    /// Update an existing lesson asynchronously.
    /// </summary>
    public async Task<Lesson> UpdateAsync(Lesson lesson)
    {
        return await _repository.UpdateAsync(lesson);
    }

    /// <summary>
    /// Delete a lesson by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search lessons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Lesson>> Search(string term, int pageIndex, int pageSize)
    {
        return await _repository.Search(term, pageIndex, pageSize);
    }
}
