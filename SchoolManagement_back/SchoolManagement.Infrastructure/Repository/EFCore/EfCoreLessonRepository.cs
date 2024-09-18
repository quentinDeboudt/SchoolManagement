public class EfCoreLessonRepository : ILessonRepository
{
    private readonly SchoolManagementDbContext _context;

    public LessonService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Lessons asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Lessons.CountAsync();
    }

    /// <summary>
    /// Get all Lessons with related entities.
    /// </summary>
    public IEnumerable<Lesson> GetAll()
    {
        return _context.Lessons
            .Include(p => p.Groups)
            .ToList();
    }

    /// <summary>
    /// Get Lessons with pagination asynchronously.
    /// </summary>
    public async Task<List<Lesson>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Lessons
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Lesson by ID.
    /// </summary>
    public Lesson GetById(int id)
    {
        return _repository.Lessons.FirstOrDefault(l => l.Id == id);
    }

    /// <summary>
    /// Create a new Lesson asynchronously.
    /// </summary>
    public  void CreateAsync(Lesson Lesson)
    {
         _context.Lessons.AddAsync(Lesson);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Lesson asynchronously.
    /// </summary>
    public async Task<Lesson> UpdateLessonAsync(Lesson Lesson)
    {
        var existingLesson = await _context.Lessons.FindAsync(Lesson.Id);
        if (existingLesson == null)
        {
            return null;
        }

        existingLesson.FirstName = Lesson.FirstName;
        existingLesson.LastName = Lesson.LastName;
        existingLesson.Roles = Lesson.Roles;
        existingLesson.StudentGroups = Lesson.StudentGroups;
        existingLesson.TeacherClassrooms = Lesson.TeacherClassrooms;
        existingLesson.TeacherLessons = Lesson.TeacherLessons;

        _context.Lessons.Update(existingLesson);
        await _context.SaveChangesAsync();

        return existingLesson;
    }

    /// <summary>
    /// Delete a Lesson by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _context.Lessons.Remove(id);
        _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search Lessons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Lesson>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Lessons
            .Where(p => p.name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
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