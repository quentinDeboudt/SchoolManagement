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
        public IEnumerable<Lesson> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get lessons with pagination asynchronously.
        /// </summary>
        public async Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
        {
           return _repository.GetWithPagination(pageNumber, pageSize);
        }

        /// <summary>
        /// Get a specific lesson by ID.
        /// </summary>
        public Lesson GetById(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a new lesson asynchronously.
        /// </summary>
        public  void CreateAsync(Lesson lesson)
        {
            return _repository.AddAsync(person);
        }

        /// <summary>
        /// Update an existing lesson asynchronously.
        /// </summary>
        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            return _repository.UpdateAsync(person);
        }

        /// <summary>
        /// Delete a lesson by ID asynchronously.
        /// </summary>
        public void DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Search lessons by term with pagination.
        /// </summary>
        public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
        {
            return _repository.Search(id);
        }
    }
}
