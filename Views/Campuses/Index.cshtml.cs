using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Views
{
    public class IndexModel : PageModel
    {
        private readonly EduCoreSuite.Data.ForgeDBContext _context;

        public IndexModel(EduCoreSuite.Data.ForgeDBContext context)
        {
            _context = context;
        }

        public IList<Campus> Campus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Campus = await _context.Campuses.ToListAsync();
        }
    }
}
