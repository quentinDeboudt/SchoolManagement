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
        public IEnumerable<Subject> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get subjects with pagination asynchronously.
        /// </summary>
        public async Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
        {
            return _repository.GetWithPagination(pageNumber, pageSize);
        }

        /// <summary>
        /// Get a specific subject by ID.
        /// </summary>
        public Subject GetById(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Create a new subject asynchronously.
        /// </summary>
        public async Task CreateAsync(Subject subject)
        {
           return _repository.AddAsync(person);
        }

        /// <summary>
        /// Update an existing subject asynchronously.
        /// </summary>
        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            return _repository.UpdateAsync(person);
        }

        /// <summary>
        /// Delete a subject by ID asynchronously.
        /// </summary>
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Search subjects by term with pagination.
        /// </summary>
        public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
        {
            return _repository.Search(id);
        }   
    }
}
