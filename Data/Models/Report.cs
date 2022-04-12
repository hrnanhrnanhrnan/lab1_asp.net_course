using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
