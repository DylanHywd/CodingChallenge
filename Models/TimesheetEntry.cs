using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    public class TimesheetEntry
    {
        [Required(ErrorMessage = "ID is required.")]
        [StringLength(10, ErrorMessage = "ID should not exceed 10 characters.")]
        public int ID { get; set; } //Primary Key
        
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(50, ErrorMessage = "Project name should not exceed 50 characters.")]
        public string Project { get; set; }

        [StringLength(200, ErrorMessage = "Task description should not exceed 200 characters.")]
        public string TaskDescription { get; set; }

        [Range(1, 24, ErrorMessage = "Hours worked must be between 1 and 24.")]
        public int HoursWorked { get; set; }

        public int TotalHoursWorked { get; set; }
    }
}
