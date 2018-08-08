using FinalProject_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_LMS.ViewModels
{
    public class CourseView
    {

        public int Id { get; set; }

        [Display(Name = "Course Name")]
        public string Name { get; set; }

      
        public string Description { get; set; }

      
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Nr Of Students")]
        public int NumberOfStudents { get; set; }

        public List<Course> Courses { get; set; }
    }
}