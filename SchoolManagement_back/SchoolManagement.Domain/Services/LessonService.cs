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
    public class LessonService : ILessonService
    {
        private readonly SchoolManagementDbContext _context;

        public LessonService(SchoolManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the total count of lessons asynchronously.
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _context.Lessons.CountAsync();
        }

        /// <summary>
        /// Get all lessons.
        /// </summary>
        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons.ToList();
        }

        /// <summary>
        /// Get lessons with pagination asynchronously.
        /// </summary>
        public async Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
        {
            return await _context.Lessons
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Include(c => c.Groups)

                .ToListAsync();
        }

        /// <summary>
        /// Get a specific lesson by ID.
        /// </summary>
        public Lesson GetById(int id)
        {
            return _context.Lessons.FirstOrDefault(l => l.Id == id);
        }

        /// <summary>
        /// Create a new lesson asynchronously.
        /// </summary>
        public  void CreateAsync(Lesson lesson)
        {
             _context.Lessons.AddAsync(lesson);
             _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an existing lesson asynchronously.
        /// </summary>
        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            var existingLesson = await _context.Lessons.FindAsync(lesson.Id);
            // if (existingLesson == null)
            // {
            //     return null;
            // }

            // existingLesson.Name = lesson.Name; // Example of property update
            // _context.Lessons.Update(existingLesson);
            // await _context.SaveChangesAsync();

            return existingLesson;
        }

        /// <summary>
        /// Delete a lesson by ID asynchronously.
        /// </summary>
        public void DeleteAsync(int id)
        {
            // _context.Lessons.Remove(id);
            // _context.SaveChangesAsync();
        }

        /// <summary>
        /// Search lessons by term with pagination.
        /// </summary>
        public async Task<PagedResult<Lesson>> SearchLessons(string term, int pageIndex, int pageSize)
        {
            // var query = _context.Lessons
            //     .Where(l => l.name.Contains(term));

            var totalCount = await _context.Lessons.CountAsync();
            var items = await _context.Lessons
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Lesson>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
