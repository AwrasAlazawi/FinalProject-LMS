using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject_LMS.ViewModels
{
    public class AllCoursesViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Count of Students")]
        public int count { get; set; }
    }
}