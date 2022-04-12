using Data.Models;
using System;
using System.Collections.Generic;
using Data.Context;

namespace Data.Utilities
{
    public class UtilityMethods
    {
        //Method to create employees and populate the database
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

        //Method to create reports and populate the database
        public static void CreateDummyReports()
        {
            var reports = new List<Report>();
            using (var context = new EmployeeReportsContext())
            {
                //iterates through the employees of the database and populates the database with reports
                var employees = context.Employees;
                foreach (var employee in employees)
                {
                    //this if statement is to ensure that some of the employees doesnt have any vacationreports
                    if(employee.FirstName != "Robin" && employee.FirstName != "Maja")
                    {
                        reports.Add(new Report() { Employee = employee, TypeOfLeave = "Semester", StartDate = DateTime.Parse("2022-07-01"), EndDate = DateTime.Parse("2022-08-01"), ReportDate = DateTime.Now});
                    }
                }
                context.Reports.AddRange(reports);
                context.SaveChanges();
            } 
        }

        //method to print out the menu choices that is being sent in as a parameter,
        //and then it takes the users choice as a integer, and it ensures that it is an integer 
        //and a valid choice from the menu and only then it returns that integer to be used in switch statements etc
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

        //method that takes the input from the user and tries to parse it into a Datetime data type, and only if it can do that it will return the DateTime
        public static DateTime GetUserDateInputAndFormatToDateTime (string menuHeader)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(menuHeader);
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

        //Overloaded GetUserDateInputAndFormatToDateTime with a date to compare to, to make sure that the end date is being set after the start date
        public static DateTime GetUserDateInputAndFormatToDateTime(string menuHeader, DateTime dateToCompareTo)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(menuHeader);
                bool isCorrectDate = DateTime.TryParse(Console.ReadLine(), out DateTime inputDateTime);
                if (isCorrectDate && inputDateTime > dateToCompareTo)
                {
                    return inputDateTime;
                }
                else
                {
                    Console.WriteLine("Something went wrong, please press enter to try again! And remember that the end date has to be set after the start date");
                    Console.ReadLine();
                }
            }
        }


    }
}
