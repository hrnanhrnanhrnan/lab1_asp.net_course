using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        [Required]
        public string TypeOfLeave { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReportDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public override string ToString()
        {
            return $"Employee: {this.Employee.FirstName} {this.Employee.LastName}, TypeOfLeave: {this.TypeOfLeave}, Reported date: {this.ReportDate}, Date: {this.StartDate} - {this.EndDate}";
        }
    }
}
