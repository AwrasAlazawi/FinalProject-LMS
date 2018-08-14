using FinalProject_LMS.Models;
using System.Collections.Generic;

namespace FinalProject_LMS.ViewModels
{
    public class AllTeachersView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<ApplicationUser> teachers = new List<ApplicationUser>();
      
    }
}