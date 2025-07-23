using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Pages.Admin.Counties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<County> Counties { get; set; } = default!;
        public string SearchString { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchString, int pageIndex = 1)
        {
            SearchString = searchString ?? "";
            PageIndex = pageIndex;

            var query = _db.Counties
                .Include(c => c.SubCounties)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(c => c.CountyName.Contains(SearchString));
            }

            int pageSize = 10;
            TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)pageSize);

            Counties = await query
                .OrderBy(c => c.CountyName)
                .Skip((PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var county = await _db.Counties.FindAsync(id);
            if (county != null)
            {
                _db.Counties.Remove(county);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
