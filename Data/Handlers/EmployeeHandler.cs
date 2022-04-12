using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Handlers
{
    //Get all employees from the database
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

        //Get all employees from the database that has firstname and lastname that contains the parameter that is being passed in
        private List<Employee> GetEmployeesByName(string name)
        {
            using (var context = new EmployeeReportsContext())
            {
                var queryEmployee = context.Employees.Where(emp => (emp.FirstName.ToLower() + " " + emp.LastName.ToLower()).Contains(name.ToLower())).ToList();
                //var queryEmployee = context.Employees.Where(emp => emp.FirstName.ToLower().Contains(name.ToLower()) || emp.LastName.ToLower().Contains(name.ToLower())).ToList();
                return queryEmployee;
            }
        }

        //method to take in input from the user and gets the employees that matches the search input from the user and then writes out the users
        public void SearchAndDisplayEmployee()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Please enter the name of the employee you want to see report-history for (or leave it empty and press enter to see all employees): ");
                string nameInput = Console.ReadLine();
                var employees = GetEmployeesByName(nameInput);
                try
                {

                    if (employees.Count != 0)
                    {
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"Name: {employee.FullName}, {HasReportedVacation(employee)}");
                        } 
                    }
                    else if(employees.Count == 0)
                    {
                        Console.WriteLine("No results for the search...");
                    }
                    Console.WriteLine("-----------------------\nPlease press enter to return to menu!");
                    Console.ReadLine();
                    run = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("-----------------------\nNo match for the name you entered, please press enter to return to menu!");
                    Console.ReadLine();
                    run = false;
                } 
            }
            
        }

        //method that returns different strings depending on if the employee has reported any vacations or not
        private string HasReportedVacation(Employee employee)
        {
            using (var context = new EmployeeReportsContext())
            {
                var reportsOfEmployee = context.Reports.Where(report => report.Employee == employee).ToList();
                if (reportsOfEmployee.Count == 0)
                {
                    return "has not made any vacation reports";
                }
                else if (reportsOfEmployee.Count == 1)
                {
                    return "has made one vacation report";
                }
                else
                {
                    return $"has made {reportsOfEmployee.Count} vacation reports";
                }
            }
        }
    }
}
