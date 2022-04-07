using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public string FullName { get => $"{this.FirstName} {this.LastName}";}
        public ICollection<Report> Reports { get; set; }

        public override string ToString()
        {
            return $"Id: {this.EmployeeId}, Name: {this.FirstName} {this.LastName}";
        }
    }
}
