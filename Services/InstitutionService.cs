using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Services
{
    public class InstitutionService
    {
        private readonly ApplicationDbContext _context;

        // Constructor for Dependency Injection: Entity Framework will inject an instance
        // of ApplicationDbContext here when the service is used.
        public InstitutionService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all institutions, including their associated sub-county and county information.
        /// </summary>
        /// <returns>A list of Institution objects.</returns>
        public async Task<List<Institution>> GetInstitutionsAsync()
        {
            // Use .Include() and .ThenInclude() to eager load related data (SubCounty and County)
            // This prevents N+1 query problems when displaying the list.
            return await _context.Institutions
                .Include(i => i.County)       // Include the SubCounty object
                    .ThenInclude(sc => sc.SubCounties) // Then include the County object within the SubCounty
                .ToListAsync(); // Execute the query and return as a list
        }

        /// <summary>
        /// Retrieves a single institution by its ID, including related sub-county and county.
        /// </summary>
        /// <param name="id">The ID of the institution to retrieve.</param>
        /// <returns>The Institution object or null if not found.</returns>
        public async Task<Institution> GetInstitutionByIdAsync(int id)
        {
            return await _context.Institutions
                .Include(navigationPropertyPath: i => i.County)
                    .ThenInclude(sc => sc.SubCounties)
                .FirstOrDefaultAsync(i => i.InstitutionID == id); // Find by ID
        }

       
        /// Adds a new institution to the database.
        /// </summary>
        /// <param name="institution">The Institution object to add.</param>
        public async Task AddInstitutionAsync(Institution institution)
        {
            _context.Institutions.Add(institution); // Add to DbContext
            await _context.SaveChangesAsync();     // Save changes to database
        }

        /// <summary>
        /// Updates an existing institution in the database.
        /// </summary>
        /// <param name="institution">The Institution object with updated data.</param>
        public async Task UpdateInstitutionAsync(Institution institution)
        {
            // Inform EF Core that the entity has been modified.
            _context.Entry(institution).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an institution from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the institution to delete.</param>
        public async Task DeleteInstitutionAsync(int id)
        {
            var institution = await _context.Institutions.FindAsync(id); // Find the entity by ID
            if (institution != null)
            {
                _context.Institutions.Remove(institution); // Mark for removal
                await _context.SaveChangesAsync();         // Save changes
            }
        }

        /// <summary>
        /// Retrieves all counties.
        /// </summary>
        /// <returns>A list of County objects.</returns>
        public async Task<List<County>> GetCountiesAsync()
        {
            return await _context.Counties.ToListAsync();
        }

        /// <summary>
        /// Retrieves sub-counties associated with a specific county ID.
        /// </summary>
        /// <param name="countyId">The ID of the county.</param>
        /// <returns>A list of SubCounty objects.</returns>
        public async Task<List<Subcounty>> GetSubCountiesByCountyAsync(int countyId)
        {
            return await _context.SubCounties
                .Where(sc => sc.CountyID == countyId) // Filter by CountyID
                .ToListAsync();
        }

    }
}
