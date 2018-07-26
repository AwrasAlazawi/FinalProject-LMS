using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject_LMS.Models
{
    public class Activity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ActivityType Type { get; set; }
        
        [Required]
        public string TypeId { get; set; }

        public Module Module { get; set; }

        [Required]
        public string ModuleId { get; set; }

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