using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
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
            DateTime endDate = UtilityMethods.GetUserDateInputAndFormatToDateTime("Please enter the END date of your vacation (YYYY-MM-DD): ");


            //create report and pass the input from the user and save it to database
            using (var context = new EmployeeReportsContext())
            {
                var choosenperson = context.Employees.FirstOrDefault(x => x.EmployeeId == choosenId);
                context.Reports.Add(new Report() { Employee = choosenperson, TypeOfLeave = vacations[choosenVacation - 1], StartDate = startDate, EndDate = endDate, ReportDate = DateTime.Now });
                context.SaveChanges();
            }
        }

        public List<Report> GetReportsByMonth (int monthInNum)
        {
            var list = new List<Report>();
            using (var context = new EmployeeReportsContext())
            {
                list = context.Reports.Where(report => report.ReportDate.Month == monthInNum).ToList();
            }
            return list;
        }

        //internal List<Report> GetReportsAndEmployees(List<Report> reports)
        //{
        //    var listToReturn = new List<Employee>();
        //    var employees = _employeeHandler.GetEmployees();
        //    for (var idx = 0; idx < reports.Count; idx++)
        //    {
        //        var numOfDays = reports[idx].EndDate - reports[idx].StartDate;
        //        Console.WriteLine($"Report date: {reports[idx].ReportDate}, number of days of report: {numOfDays.Days}, Reported by: {employees.FirstOrDefault(employee => employee.EmployeeId == reports[idx].EmployeeId).FullName}");
        //    }


        //}

        internal Dictionary<int, int> GetNumberOfDaysReportedForEachEmployeeId(List<Report> reports)
        {
            var dictToReturn = new Dictionary<int, int>();
            for(var idx = 0; idx < reports.Count; idx++)
            {
                if(!dictToReturn.ContainsKey(reports[idx].EmployeeId))
                {
                    dictToReturn.Add(reports[idx].EmployeeId, GetNumberOfDaysPerTimeSpan(reports[idx].StartDate, reports[idx].EndDate));
                }
                else
                {
                    var prevValue = dictToReturn.GetValueOrDefault(reports[idx].EmployeeId);
                    dictToReturn.Remove(reports[idx].EmployeeId);
                    dictToReturn.Add(reports[idx].EmployeeId, prevValue + GetNumberOfDaysPerTimeSpan(reports[idx].StartDate, reports[idx].EndDate));
                }
            }
            return dictToReturn;
        }

        internal int GetNumberOfDaysPerTimeSpan(DateTime startDate, DateTime endDate)
        {
            var numberOfDays = endDate - startDate;
            return numberOfDays.Days;
        }

        public void DisplayReportsByMonth()
        {
            Console.Clear();
            string [] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int choosenMonthNum = UtilityMethods.DisplayMenuAndGetUserChoice(months, "Please enter number for the month that you want to see reports for:");
            var listOfReports = GetReportsByMonth(choosenMonthNum);

            Console.Clear();
            if (listOfReports.Count != 0)
            {
                Console.WriteLine($"All reports for {months[choosenMonthNum - 1]}:");
                var employees = _employeeHandler.GetEmployees();
                for (var idx = 0; idx < listOfReports.Count; idx++)
                {
                    Console.WriteLine($"Report date: {listOfReports[idx].ReportDate}, number of days of report: {GetNumberOfDaysPerTimeSpan(listOfReports[idx].StartDate, listOfReports[idx].EndDate)}, Reported by: {employees.FirstOrDefault(employee => employee.EmployeeId == listOfReports[idx].EmployeeId).FullName}");
                }
                Console.WriteLine("-------------Summary--------------");
                var empIdWithNumOfReports = GetNumberOfDaysReportedForEachEmployeeId(listOfReports);
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
