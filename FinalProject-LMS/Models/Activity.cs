using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_LMS.Models
{
    public class Activity
    {
      
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ActivityType Type { get; set; }
        
        [Required]
        public int TypeId { get; set; }

        public Module Module { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
    }
}