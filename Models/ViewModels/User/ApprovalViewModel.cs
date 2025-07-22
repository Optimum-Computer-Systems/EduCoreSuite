using EduCoreSuite.Models;
using System.Collections.Generic;

namespace EduCoreSuite.Models.ViewModels.User
{
    public class ApprovalViewModel
    {
        public List<Models.User> Users { get; set; }  // Fully qualify with Models.User
        public Dictionary<long, string> Roles { get; set; }
    }
}