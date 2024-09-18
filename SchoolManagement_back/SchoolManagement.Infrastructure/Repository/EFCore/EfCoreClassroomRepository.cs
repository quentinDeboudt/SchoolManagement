public class EfCoreClassroomRepository : IClassroomRepository
{
    private readonly SchoolManagementDbContext _context;

    public ClassroomService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Classrooms asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Classrooms.CountAsync();
    }

    /// <summary>
    /// Get all Classrooms with related entities.
    /// </summary>
    public IEnumerable<Classroom> GetAll()
    {
        return _context.Classrooms
            .Include(p => p.Roles)
            .ToList();
    }

    /// <summary>
    /// Get Classrooms with pagination asynchronously.
    /// </summary>
    public async Task<List<Classroom>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Classrooms
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Groups)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Classroom by ID.
    /// </summary>
    public Classroom GetById(int id)
    {
        return _context.Classrooms
            .Include(p => p.Roles)
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Create a new Classroom asynchronously.
    /// </summary>
    public  void CreateAsync(Classroom classroom)
    {
         _context.Classrooms.AddAsync(classroom);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Classroom asynchronously.
    /// </summary>
    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        var existingClassroom = await _repository.Classrooms.FindAsync(classroom.Id);
        if (existingClassroom == null)
        {
            return null;
        }

        existingClassroom.Name = classroom.Name; // Example of property update
        _repository.Classrooms.Update(existingClassroom);
        await _repository.SaveChangesAsync();

        return existingClassroom;
    }

    /// <summary>
    /// Delete a Classroom by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _context.Classrooms.Remove(id);
        _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search Classrooms by term with pagination.
    /// </summary>
    public async Task<PagedResult<Classroom>> Search(string term, int pageIndex, int pageSize)
    {
       var query = _repository.Classrooms
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