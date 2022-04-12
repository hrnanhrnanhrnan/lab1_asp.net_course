using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}";}
        public ICollection<Report> Reports { get; set; }

        public override string ToString()
        {
            return $"Id: {EmployeeId}, Name: {FullName}";
        }
    }
}
