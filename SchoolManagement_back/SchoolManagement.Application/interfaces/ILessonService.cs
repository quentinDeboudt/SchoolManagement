using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Interfaces;
public interface ILessonService
{
    // Get the total number of lessons.
    Task<int> CountAsync();

    // Get all lessons without pagination.
    IEnumerable<Lesson> GetAll();

    // Get lessons with pagination.
    Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize);

    // Get a specific lesson by ID.
    Lesson GetById(int id);

    // Create a new lesson.
    void CreateAsync(Lesson lesson);

    // Update an existing lesson asynchronously.
    Task<Lesson> UpdateLessonAsync(Lesson lesson);

    // Delete a lesson by ID.
    void DeleteAsync(int id);

    // Search lessons by term with pagination.
    Task<PagedResult<Lesson>> SearchLessons(string term, int pageIndex, int pageSize);
}
