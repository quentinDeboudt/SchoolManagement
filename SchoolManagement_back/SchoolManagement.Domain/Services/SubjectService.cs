using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace SchoolManagement.Domain.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly SchoolManagementDbContext _context;

        public SubjectService(SchoolManagementDbContext context)
        {
            _context = context;
        }

    //     /// <summary>
    //     /// Get the total count of subjects asynchronously.
    //     /// </summary>
    //     public async Task<int> CountAsync()
    //     {
    //         return await _context.Subjects.CountAsync();
    //     }

    //     /// <summary>
    //     /// Get all subjects.
    //     /// </summary>
    //     public IEnumerable<Subject> GetAll()
    //     {
    //         return _context.Subjects.ToList();
    //     }

    //     /// <summary>
    //     /// Get subjects with pagination asynchronously.
    //     /// </summary>
    //     public async Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    //     {
    //         return await _context.Subjects
    //             .Skip(pageNumber * pageSize)
    //             .Take(pageSize)
    //             .ToListAsync();
    //     }

    //     /// <summary>
    //     /// Get a specific subject by ID.
    //     /// </summary>
    //     public Subject GetById(int id)
    //     {
    //         return _context.Subjects.FirstOrDefault(s => s.Id == id);
    //     }

    //     /// <summary>
    //     /// Create a new subject asynchronously.
    //     /// </summary>
    //     public async Task CreateAsync(Subject subject)
    //     {
    //         await _context.Subjects.AddAsync(subject);
    //         await _context.SaveChangesAsync();
    //     }

    //     /// <summary>
    //     /// Update an existing subject asynchronously.
    //     /// </summary>
    //     public async Task<Subject> UpdateSubjectAsync(Subject subject)
    //     {
    //         var existingSubject = await _context.Subjects.FindAsync(subject.Id);
    //         if (existingSubject == null)
    //         {
    //             return null;
    //         }

    //         existingSubject.Name = subject.Name; // Example of property update
    //         _context.Subjects.Update(existingSubject);
    //         await _context.SaveChangesAsync();

    //         return existingSubject;
    //     }

    //     /// <summary>
    //     /// Delete a subject by ID asynchronously.
    //     /// </summary>
    //     public async Task<IActionResult> DeleteAsync(int id)
    //     {
    //         var subject = await _context.Subjects.FindAsync(id);
    //         if (subject == null) return;

    //         _context.Subjects.Remove(subject);
    //        return await _context.SaveChangesAsync();
    //     }

    //     /// <summary>
    //     /// Search subjects by term with pagination.
    //     /// </summary>
    //     public async Task<PagedResult<Subject>> SearchSubjects(string term, int pageIndex, int pageSize)
    //     {
    //         var query = _context.Subjects
    //             .Where(s => s.Name.Contains(term));

    //         var totalCount = await query.CountAsync();
    //         var items = await query
    //             .Skip(pageIndex * pageSize)
    //             .Take(pageSize)
    //             .ToListAsync();

    //         return new PagedResult<Subject>
    //         {
    //             Items = items,
    //             TotalCount = totalCount
    //         };
    //     }
    }
}
