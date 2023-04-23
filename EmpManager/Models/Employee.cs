using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmpManager.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [Range(1, 3, ErrorMessage = "Must be a whole number from 1 - 3")]
        public int? Shift { get; set; }

        public bool IsClockedIn { get; set; }
    }
}
