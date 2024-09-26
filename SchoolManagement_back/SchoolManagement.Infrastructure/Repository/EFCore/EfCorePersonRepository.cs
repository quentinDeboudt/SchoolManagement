using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace SchoolManagement.Infrastructure.Repository.EFCore;
public class EfCorePersonRepository : IPersonRepository
{
    private readonly SchoolManagementDbContext _context;

    public EfCorePersonRepository(SchoolManagementDbContext context)
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
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons
            .Include(p => p.Roles)
            .ToListAsync();
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
    public async Task<Person> GetByIdAsync(int id)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Create a new person asynchronously.
    /// </summary>
    public async Task<int> AddAsync(Person person)
    {
        // await _context.Persons.AddAsync(person);
        // return await _context.SaveChangesAsync();
        // Vérifiez si les rôles existent déjà dans la base de données
        var rolesToAdd = new List<Role>();

        foreach (var role in person.Roles)
        {
            // Récupérer le rôle existant par ID
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole != null)
            {
                rolesToAdd.Add(existingRole); // Ajoutez le rôle existant à la liste
            }
        }

        // Assignez la liste des rôles à la personne
        person.Roles = rolesToAdd;

        // Ajoutez la nouvelle personne à la base de données
        await _context.Persons.AddAsync(person);
        
        // Sauvegardez les changements et retournez le nombre de lignes affectées
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing person asynchronously.
    /// </summary>
    public async Task<Person> UpdateAsync(Person person)
    {
         // Récupère l'entité sans suivi pour éviter les conflits de suivi
        var existingPerson = await _context.Persons
            .Include(p => p.Roles)  // Inclure les rôles existants
            .Include(p => p.StudentGroups)
            .Include(p => p.TeacherClassrooms)
            .Include(p => p.TeacherLessons)
            .SingleOrDefaultAsync(p => p.Id == person.Id);

        if (existingPerson == null)
        {
            return null; // Personne non trouvée
        }

        // Mettre à jour les propriétés de la personne
        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.StudentGroups = person.StudentGroups;
        existingPerson.TeacherClassrooms = person.TeacherClassrooms;
        existingPerson.TeacherLessons = person.TeacherLessons;

        // Mettre à jour les rôles
        UpdateRoles(existingPerson, person.Roles);

        // Marquer l'entité modifiée
        _context.Persons.Update(existingPerson);

        // Sauvegarder les changements
        await _context.SaveChangesAsync();

        return existingPerson;
    }

    private void UpdateRoles(Person existingPerson, ICollection<Role> newRoles)
    {
        // Récupérer les rôles existants
        var existingRoles = existingPerson.Roles.ToList();

        // Supprimer les rôles qui ne sont plus dans la nouvelle liste
        foreach (var existingRole in existingRoles)
        {
            if (!newRoles.Any(r => r.Id == existingRole.Id))
            {
                existingPerson.Roles.Remove(existingRole); // Supprimer l'association
            }
        }

        // Ajouter les nouveaux rôles
        foreach (var role in newRoles)
        {
            if (!existingRoles.Any(r => r.Id == role.Id))
            {
                existingPerson.Roles.Add(role); // Ajouter le nouveau rôle
            }
        }
    }

    /// <summary>
    /// Delete a person by ID asynchronously.
    /// </summary>
    public async void DeleteAsync(int id)
    {

        // Récupérer l'entité sans suivi pour éviter les conflits de suivi
        // var existingPerson = await _context.Persons
        //     .Include(p => p.Roles)  // Inclure les rôles associés, si nécessaire
        //     .Include(p => p.StudentGroups) // Inclure d'autres collections, si nécessaire
        //     .SingleOrDefaultAsync(p => p.Id == id);
var person = await _context.Persons.FindAsync(id);
    
   

    _context.Persons.Remove(person);
    await _context.SaveChangesAsync();
    
    }

    /// <summary>
    /// Search persons by term with pagination.
    /// </summary>
    public async Task<PagedResult<Person>> Search(string term, int pageIndex, int pageSize)
    {
        var query = _context.Persons
            .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term));

        if (query == null)
        {
            return null;
        }

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