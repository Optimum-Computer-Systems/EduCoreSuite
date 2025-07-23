using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduCoreSuite.Services
{
    public class InstitutionService
    {
        private readonly ApplicationDbContext _db;

        public InstitutionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Institution>> GetAllInstitutionsAsync()
        {
            return await _db.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .ToListAsync();
        }

        public async Task<Institution> GetInstitutionByIdAsync(int id)
        {
            return await _db.Institutions
                .Include(i => i.County)
                .Include(i => i.SubCounty)
                .FirstOrDefaultAsync(i => i.InstitutionID == id);
        }

        public async Task AddInstitutionAsync(Institution institution)
        {
            _db.Institutions.Add(institution);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateInstitutionAsync(Institution institution)
        {
            _db.Institutions.Update(institution);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteInstitutionAsync(int id)
        {
            var institution = await _db.Institutions.FindAsync(id);
            if (institution != null)
            {
                _db.Institutions.Remove(institution);
                await _db.SaveChangesAsync();
            }
        }
    }
}
