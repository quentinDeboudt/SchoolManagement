using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
        return _repository.CountAsync();
    }

    /// <summary>
    /// Get all classrooms.
    /// </summary>
    public IEnumerable<Classroom> GetAll()
    {
        return _repository.GetAll();
    }

    /// <summary>
    /// Get classrooms with pagination asynchronously.
    /// </summary>
    public async Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        return _repository.GetWithPagination(pageNumber, pageSize);
    }

    /// <summary>
    /// Get a specific classroom by ID.
    /// </summary>
    public Classroom GetById(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new classroom asynchronously.
    /// </summary>
    public void CreateAsync(Classroom classroom)
    {
       return _repository.AddAsync(person);
    }

    /// <summary>
    /// Update an existing classroom asynchronously.
    /// </summary>
    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        return _repository.UpdateAsync(person);
    }

    /// <summary>
    /// Delete a classroom by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Search classrooms by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
    {
        return _repository.Search(id);
    }
}
