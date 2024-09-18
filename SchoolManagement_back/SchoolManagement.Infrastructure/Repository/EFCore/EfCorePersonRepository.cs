public class EfCorePersonRepository : IPersonRepository
{
    private readonly SchoolManagementDbContext _context;

    public PersonService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of persons asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Persons.CountAsync();
    }

    /// <summary>
    /// Get all persons with related entities.
    /// </summary>
    public IEnumerable<Person> GetAll()
    {
        return _context.Persons
            .Include(p => p.Roles)
            .ToList();
    }

    /// <summary>
    /// Get persons with pagination asynchronously.
    /// </summary>
    public async Task<List<Person>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Persons
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Roles)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific person by ID.
    /// </summary>
    public Person GetById(int id)
    {
        return _context.Persons
            .Include(p => p.Roles)
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Create a new person asynchronously.
    /// </summary>
    public  void CreateAsync(Person person)
    {
         _context.Persons.AddAsync(person);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing person asynchronously.
    /// </summary>
    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var existingPerson = await _context.Persons.FindAsync(person.Id);
        if (existingPerson == null)
        {
            return null;
        }

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Roles = person.Roles;
        existingPerson.StudentGroups = person.StudentGroups;
        existingPerson.TeacherClassrooms = person.TeacherClassrooms;
        existingPerson.TeacherLessons = person.TeacherLessons;

        _context.Persons.Update(existingPerson);
        await _context.SaveChangesAsync();

        return existingPerson;
    }

    /// <summary>
    /// Delete a person by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        // _context.Persons.Remove(id);
        //  _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search persons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Person>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Persons
            .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Person>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}