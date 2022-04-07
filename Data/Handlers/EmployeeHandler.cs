using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Handlers
{
    public class EmployeeHandler
    {
        internal List<Employee> GetEmployees()
        {
            var list = new List<Employee>();
            using(var context = new EmployeeReportsContext())
            {
                list = context.Employees.ToList();
            }
            return list;
        }

        internal Employee GetEmployeeByName(string name)
        {
            using (var context = new EmployeeReportsContext())
            {
                var queryEmployee = context.Employees.FirstOrDefault(emp => emp.FirstName.ToLower() == name.ToLower() || emp.LastName.ToLower() == name.ToLower());
                return queryEmployee;
            }
        }

        internal Employee GetEmployeeById(int id)
        {
            {
                using (var context = new EmployeeReportsContext())
                {
                    var queryEmployee = context.Employees.FirstOrDefault(emp => emp.EmployeeId == id);
                    return queryEmployee;
                }
            }
        }

        public void SearchAndDisplayEmployee()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Please enter the name of the employee you want to see report-history for: ");
                string nameInput = Console.ReadLine();
                var employee = GetEmployeeByName(nameInput);
                try
                {
                    Console.WriteLine($"Name: {employee.FullName}, {HasReportedVacation(employee)}");
                    Console.WriteLine("-----------------------\nPlease press enter to return to menu");
                    Console.ReadLine();
                    run = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("-----------------------\nNo match for the name you entered, please press enter to try again");
                    Console.ReadLine();
                } 
            }
            
        }

        internal string HasReportedVacation(Employee employee)
        {
            using (var context = new EmployeeReportsContext())
            {
                var reportsOfEmployee = context.Reports.Where(report => report.Employee == employee).ToList();
                if (reportsOfEmployee.Count == 0)
                {
                    return "has not made any vacation reports";
                }
                else
                {
                    return $"has made {reportsOfEmployee.Count} vacation reports";
                }
            }
        }
    }
}
