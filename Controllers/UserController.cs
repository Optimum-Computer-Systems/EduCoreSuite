using EduCoreSuite.Data;
using EduCoreSuite.Models.ViewModels.UserApproval;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace EduCoreSuite.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize(Roles = "3")]

        [HttpGet]
        public async Task<IActionResult> Approval()
        {
            var viewModel = new ApprovalViewModel
            {
                Users = await _db.Users.ToListAsync(),
                //Roles = await _db.Roles.ToDictionaryAsync(r => r.ID.ToString(), r => r.Name)
            };
            return View(viewModel);
        }
    }
}