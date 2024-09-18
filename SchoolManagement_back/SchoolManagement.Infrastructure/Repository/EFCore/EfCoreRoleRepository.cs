public class EfCoreRoleRepository : IRoleRepository
{
    private readonly SchoolManagementDbContext _context;

    public RoleService(SchoolManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the total count of Roles asynchronously.
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Roles.CountAsync();
    }

    /// <summary>
    /// Get all Roles with related entities.
    /// </summary>
    public IEnumerable<Role> GetAll()
    {
        return _context.Roles.ToList();
    }

    /// <summary>
    /// Get Roles with pagination asynchronously.
    /// </summary>
    public async Task<List<Role>> GetWithPagination(int pageNumber, int pageSize)
    {
        return await _context.Roles
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific Role by ID.
    /// </summary>
    public Role GetById(int id)
    {
        return _repository.Roles.FirstOrDefault(r => r.Id == id);
    }

    /// <summary>
    /// Create a new Role asynchronously.
    /// </summary>
    public  void CreateAsync(Role Role)
    {
         _context.Roles.AddAsync(Role);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing Role asynchronously.
    /// </summary>
    public async Task<Role> UpdateRoleAsync(Role Role)
    {
        var existingRole = await _repository.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                return null;
            }

            existingRole.Name = role.Name; // Example of property update
            _repository.Roles.Update(existingRole);
            await _repository.SaveChangesAsync();

            return existingRole;
    }

    /// <summary>
    /// Delete a Role by ID asynchronously.
    /// </summary>
    public void DeleteAsync(int id)
    {
        _context.Roles.Remove(id);
         _context.SaveChangesAsync();
    }

    /// <summary>
    /// Search Roles by term with pagination.
    /// </summary>
    public async Task<PagedResult<Role>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Roles
            .Where(p => p.name.Contains(term));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Role>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}