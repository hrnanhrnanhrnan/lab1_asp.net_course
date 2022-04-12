using Data.Context;
using System;
using System.Collections.Generic;
using Data.Utilities;
using System.Linq;
using Data.Models;

namespace Data.Handlers
{
    public class ReportHandler
    {
        private readonly EmployeeHandler _employeeHandler = new EmployeeHandler();
        public void CreateVacationReport()
        {
            //Get the employees from the database
            var employees = _employeeHandler.GetEmployees();

            //New list of strings and pass all employees names to the list
            var employeesNames = new List<string>();
            employees.ForEach(employee => employeesNames.Add($"{employee.FirstName} {employee.LastName}"));

            //Display employees names and get the choosen id from the user
            int choosenId = UtilityMethods.DisplayMenuAndGetUserChoice(employeesNames.ToArray(), "Please enter Id to the person that you want to create report for: ");

            //Display the types of vacations and get the choosen type of vacation from the user
            string[] vacations = new string[] { "Semester", "Vård av barn", "Tjänsledigt"};
            int choosenVacation = UtilityMethods.DisplayMenuAndGetUserChoice(vacations, "Please choose the type of vacation: ");

            //Get the start date from the user
            DateTime startDate = UtilityMethods.GetUserDateInputAndFormatToDateTime("Please enter the START date of your vacation (YYYY-MM-DD): ");

            //Get the end date from the user
            DateTime endDate = UtilityMethods.GetUserDateInputAndFormatToDateTime("Please enter the END date of your vacation (YYYY-MM-DD): ", startDate);

            //create report and pass the input from the user and save it to database
            using (var context = new EmployeeReportsContext())
            {
                var choosenperson = context.Employees.FirstOrDefault(x => x.EmployeeId == choosenId);
                context.Reports.Add(new Report() { Employee = choosenperson, TypeOfLeave = vacations[choosenVacation - 1], StartDate = startDate, EndDate = endDate, ReportDate = DateTime.Now });
                context.SaveChanges();
            }
        }

        //Get all reports from the database by the month that they have been reported
        public List<Report> GetReportsByMonth (int monthInNum)
        {
            var list = new List<Report>();
            using (var context = new EmployeeReportsContext())
            {
                list = context.Reports.Where(report => report.ReportDate.Month == monthInNum).ToList();
            }
            return list;
        }

        //Method to summarize all total days of reported days of vacation for each employee, the dictionary holds the employeeid as key and number of reported vacation days as value
        //the method takes reports as a parameter and then uses the employee id to set key and add together the number of days from the timespan of the vacation report
        internal Dictionary<int, int> GetNumberOfDaysReportedForEachEmployeeId(List<Report> reports)
        {
            var dictToReturn = new Dictionary<int, int>();
            for(var idx = 0; idx < reports.Count; idx++)
            {
                //if the employeeid doesnt exist in when iterating through the reports,
                //then add id to the dictionary to return along with the number of days from the timespan of startdate to enddate
                if(!dictToReturn.ContainsKey(reports[idx].EmployeeId))
                {
                    dictToReturn.Add(reports[idx].EmployeeId, GetNumberOfDaysPerTimeSpan(reports[idx].StartDate, reports[idx].EndDate));
                }
                else
                {
                    //if the employee id exists, then get the value from that key, store it in a variable, then delete the key value pair
                    //then add the employee id as a key and summarize the previous number of days of reported vacation with the number of days from the current report in the list
                    //and add that number of days as a value
                    var prevValue = dictToReturn.GetValueOrDefault(reports[idx].EmployeeId);
                    dictToReturn.Remove(reports[idx].EmployeeId);
                    dictToReturn.Add(reports[idx].EmployeeId, prevValue + GetNumberOfDaysPerTimeSpan(reports[idx].StartDate, reports[idx].EndDate));
                }
            }
            return dictToReturn;
        }

        //method that takes startdate and enddate as parameters and sets them as a result to a timespan,
        //from that timespan the days property is used to get the amount of days of the timespan and returns it
        internal int GetNumberOfDaysPerTimeSpan(DateTime startDate, DateTime endDate)
        {
            var numberOfDays = endDate - startDate;
            return numberOfDays.Days;
        }

        //method to get the choosen number of month from the user and display all reports from that month,
        //along with a summarize of how many days each person has reported vacation from that month 
        public void DisplayReportsByMonth()
        {
            Console.Clear();
            string [] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int choosenMonthNum = UtilityMethods.DisplayMenuAndGetUserChoice(months, "Please enter number for the month that you want to see reports for:");
            var listOfReports = GetReportsByMonth(choosenMonthNum);

            Console.Clear();
            // as long as the reports for the choosen month is not equal to 0, then each report is printed out for that month
            if (listOfReports.Count != 0)
            {
                Console.WriteLine($"All reports for {months[choosenMonthNum - 1]}:");
                var employees = _employeeHandler.GetEmployees();
                for (var idx = 0; idx < listOfReports.Count; idx++)
                {
                    //as it iterates through all reports it also gets the employee for the current report by matching the employee id of the report to the employee id in the employees table
                    Console.WriteLine($"Report date: {listOfReports[idx].ReportDate}, number of days of report: {GetNumberOfDaysPerTimeSpan(listOfReports[idx].StartDate, listOfReports[idx].EndDate)}, Reported by: {employees.FirstOrDefault(employee => employee.EmployeeId == listOfReports[idx].EmployeeId).FullName}");
                }

                Console.WriteLine("-------------Summary--------------");
                
                //this then fetches a dictionary with employeeids and the number of days reported through the list of reports for the choosen month
                var empIdWithNumOfReports = GetNumberOfDaysReportedForEachEmployeeId(listOfReports);

                //by then iterating through the dicitonary it writes out the fullname of the employee (by matching the key in the dicitonary to the employee id in the employee table),
                //as well as the total number of reported days
                foreach (var keyValue in empIdWithNumOfReports)
                {
                    Console.WriteLine($"Name: {employees.FirstOrDefault(employee => employee.EmployeeId == keyValue.Key).FullName}, number of days reported: {keyValue.Value}");
                }
                Console.WriteLine("-----------------------------------\nPlease press enter to return to menu");
                Console.ReadLine(); 
            }
            else
            {
                Console.WriteLine("No reports for the choosen month..");
                Console.WriteLine("-----------------------------------\nPlease press enter to return to menu");
                Console.ReadLine();
            }
        }
    }
}
