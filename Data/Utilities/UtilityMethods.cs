using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;

namespace Data.Utilities
{
    public class UtilityMethods
    {
        public static void CreateDummyEmployees()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee() { FirstName = "Peter", LastName="Andersson" });
            employees.Add(new Employee() { FirstName = "Maja", LastName = "Håkansson" });
            employees.Add(new Employee() { FirstName = "Sandra", LastName = "Pettersson" });
            employees.Add(new Employee() { FirstName = "Robin", LastName = "Mjäki" });
            employees.Add(new Employee() { FirstName = "Tommy", LastName = "Andersson" });
            employees.Add(new Employee() { FirstName = "Petra", LastName = "Rydberg" });

            using (var context = new EmployeeReportsContext())
            {
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }

        public static void CreateDummyReports()
        {
            var reports = new List<Report>();
            using (var context = new EmployeeReportsContext())
            {
                var employees = context.Employees;
                foreach (var employee in employees)
                {
                    if(employee.FirstName != "Robin" && employee.FirstName != "Maja")
                    {
                        reports.Add(new Report() { Employee = employee, TypeOfLeave = "Semester", StartDate = DateTime.Today, EndDate = DateTime.Today, ReportDate = DateTime.Now});
                    }
                }
                context.Reports.AddRange(reports);
                context.SaveChanges();
            } 
        }

        public static int DisplayMenuAndGetUserChoice(string[] menuChoices, string menuHeader)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(menuHeader);
                for (int idx = 0; idx < menuChoices.Length; idx++)
                {
                    Console.WriteLine($"{idx + 1}. {menuChoices[idx]}");
                }

                Console.Write("Please write your option and press enter: ");

                string userInput = Console.ReadLine();
                bool isInteger = int.TryParse(userInput, out int choice);
                if (isInteger && choice < menuChoices.Length + 1 && choice > 0)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("--------------------------------\nSomething went wrong, please press enter to try again!");
                    Console.ReadLine();
                }
            }

        }

        public static DateTime GetUserDateInputAndFormatToDateTime (string menuChoices)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(menuChoices);
                bool isCorrectDate = DateTime.TryParse(Console.ReadLine(), out DateTime inputDateTime);
                if(isCorrectDate)
                {
                    return inputDateTime;
                }
                else
                {
                    Console.WriteLine("Something went wrong, please press enter to try again!");
                    Console.ReadLine();
                }
            }
        }



    }
}
