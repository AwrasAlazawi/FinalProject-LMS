using FinalProject_LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_LMS.ViewModels
{
    public class AllStudentsView
    {
      
        [Display(Name = "Member Name")]
        public string Name { get; set; }

      


        public int? CourseId { get; set; }

    }
}