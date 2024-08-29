using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace SchoolManagement.Application.Interfaces;
public interface IClassroomService
{
    // Get the total number of classrooms.
    Task<int> CountAsync();

    // Get all classrooms without pagination.
    IEnumerable<Classroom> GetAll();

    // Get classrooms with pagination.
    Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize);

    // Get a specific classroom by ID.
    Classroom GetById(int id);

    // Create a new classroom.
    void CreateAsync(Classroom classroom);

    // Update an existing classroom asynchronously.
    Task<Classroom> UpdateClassroomAsync(Classroom classroom);

    // Delete a classroom by ID.
    void DeleteAsync(int id);

    // Search classrooms by term with pagination.
    Task<PagedResult<Classroom>> SearchClassrooms(string term, int pageIndex, int pageSize);
}
