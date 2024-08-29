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
    private readonly SchoolManagementDbContext _context;

    public ClassroomService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of classrooms asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Classrooms.CountAsync();
    }

    /// <summary>
    /// Get all classrooms.
    /// </summary>
    public IEnumerable<Classroom> GetAll()
    {
        return _context.Classrooms.ToList();
    }

    /// <summary>
    /// Get classrooms with pagination asynchronously.
    /// </summary>
    public async Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Classrooms
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(c => c.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific classroom by ID.
    /// </summary>
    public Classroom GetById(int id)
    {
        return _context.Classrooms.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Create a new classroom asynchronously.
    /// </summary>
    public void CreateAsync(Classroom classroom)
    {
        _context.Classrooms.AddAsync(classroom);
        _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing classroom asynchronously.
    /// </summary>
    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        var existingClassroom = await _context.Classrooms.FindAsync(classroom.Id);
        if (existingClassroom == null)
        {
            return null;
        }

        existingClassroom.Name = classroom.Name; // Example of property update
        _context.Classrooms.Update(existingClassroom);
        await _context.SaveChangesAsync();

        return existingClassroom;
    }

    /// <summary>
    /// Delete a classroom by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        // _context.Classrooms.Remove(id);
        // _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search classrooms by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> SearchClassrooms(string term, int pageIndex, int pageSize)
    {
        var query = _context.Classrooms
            .Where(c => c.Name.Contains(term));

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Classroom>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
