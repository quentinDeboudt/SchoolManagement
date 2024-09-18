public class EfCoreGroupRepository : IGroupRepository
{
    private readonly SchoolManagementDbContext _context;

    public GroupService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Groups asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Groups.CountAsync();
    }

    /// <summary>
    /// Get all Groups with related entities.
    /// </summary>
    public IEnumerable<Group> GetAll()
    {
        return _context.Groups
            .Include(p => p.Classroom)
            .ToList();
    }

    /// <summary>
    /// Get Groups with pagination asynchronously.
    /// </summary>
    public async Task<List<Group>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Groups
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(p => p.Classroom)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Group by ID.
    /// </summary>
    public Group GetById(int id)
    {
        return _repository.Groups.FirstOrDefault(g => g.Id == id);
    }

    /// <summary>
    /// Create a new Group asynchronously.
    /// </summary>
    public  void CreateAsync(Group Group)
    {
         _context.Groups.AddAsync(Group);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Group asynchronously.
    /// </summary>
    public async Task<Group> UpdateGroupAsync(Group Group)
    {
      var existingGroup = await _repository.Groups.FindAsync(group.Id);
        if (existingGroup == null)
        {
            return null;
        }

        existingGroup.Name = group.Name; // Example of property update
        _repository.Groups.Update(existingGroup);
        await _repository.SaveChangesAsync();

        return existingGroup;
    }

    /// <summary>
    /// Delete a Group by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _context.Groups.Remove(id);
        _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search Groups by term with pagination.
    /// </summary>
    public async Task<PagedResult<Group>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Groups
            .Where(g => g.Name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Group>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}