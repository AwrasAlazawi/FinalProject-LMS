using FinalProject_LMS.Models;
using System.Collections.Generic;

namespace FinalProject_LMS.ViewModels
{
    public class AllStudentsView 
    {
        public string  Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //public List<ApplicationUser> students = new List<ApplicationUser>();
        public string CourseName { get; set; }


    }
}