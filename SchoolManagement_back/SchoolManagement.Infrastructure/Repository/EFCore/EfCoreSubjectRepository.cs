public class EfCoreSubjectRepository : ISubjectRepository
{
    private readonly SchoolManagementDbContext _context;

    public SubjectService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Subjects asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Subjects.CountAsync();
    }

    /// <summary>
    /// Get all Subjects with related entities.
    /// </summary>
    public IEnumerable<Subject> GetAll()
    {
        return _context.Subjects.ToList();
    }

    /// <summary>
    /// Get Subjects with pagination asynchronously.
    /// </summary>
    public async Task<List<Subject>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Subjects
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Subject by ID.
    /// </summary>
    public Subject GetById(int id)
    {
        return _repository.Subject.FirstOrDefault(s => s.Id == id);
    }

    /// <summary>
    /// Create a new Subject asynchronously.
    /// </summary>
    public  void CreateAsync(Subject Subject)
    {
         _context.Subjects.AddAsync(Subject);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Subject asynchronously.
    /// </summary>
    public async Task<Subject> UpdateSubjectAsync(Subject Subject)
    {
        var existingSubject = await _context.Subjects.FindAsync(Subject.Id);
        if (existingSubject == null)
        {
            return null;
        }

        existingSubject.FirstName = Subject.FirstName;
        existingSubject.LastName = Subject.LastName;
        existingSubject.Roles = Subject.Roles;
        existingSubject.StudentGroups = Subject.StudentGroups;
        existingSubject.TeacherClassrooms = Subject.TeacherClassrooms;
        existingSubject.TeacherLessons = Subject.TeacherLessons;

        _context.Subjects.Update(existingSubject);
        await _context.SaveChangesAsync();

        return existingSubject;
    }

    /// <summary>
    /// Delete a Subject by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _context.Subjects.Remove(id);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search Subjects by term with pagination.
    /// </summary>
    public async Task<PagedResult<Subject>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Subjects
            .Where(p => p.name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Subject>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}