using System;
using Data.Handlers;
using Data.Utilities;

namespace lab1_asp.UI
{
    internal class UserInterface
    {
        ReportHandler _reportHandler = new ReportHandler();
        EmployeeHandler _employeeHandler = new EmployeeHandler();

        //starting point of the application, runs the start menu
        internal void RunUserInterface()
        {
            bool run = true;
            Console.WriteLine("------Welcome to the application!------");
            Console.WriteLine("\tPress enter to continue");
            Console.ReadLine();
            while (run)
            {
                string[] menu = new string[] { "Admin menu", "Employee menu", "Exit"};
                int userChoice = UtilityMethods.DisplayMenuAndGetUserChoice(menu, "Menu choices: ");
                switch(userChoice)
                {
                    case 1:
                        //run admin menu
                        RunAdminMenu();
                        break;
                    case 2:
                        //run employee menu
                        RunEmployeeMenu();
                        break;
                    case 3:
                        //Exit application
                        Console.WriteLine("------------------------\nNow closing application, please press enter to close the application!");
                        Console.ReadLine();
                        run = false;
                        break;
                }
            }
        }


        //Menu for admins
        private void RunAdminMenu()
        {
            bool run = true;
            while (run)
            {
                string[] menu = new string[] { "Search Employee", "Get reports by month", "Return to main menu"};
                int userChoice = UtilityMethods.DisplayMenuAndGetUserChoice(menu, "Menu Choices (Admin Menu)");
                switch (userChoice)
                {
                    case 1:
                        //runs the search and display employee method from the employee handler class
                        _employeeHandler.SearchAndDisplayEmployee();
                        break;
                    case 2:
                        //runs the display reports by month method from the report handler class
                        _reportHandler.DisplayReportsByMonth();
                        break;
                    case 3:
                        //go back to main menu
                        run = false;
                        break;
                }
            }
        }


        //Menu for employees
        private void RunEmployeeMenu()
        {
            bool run = true;
            while (run)
            {
                string[] menu = new string[] { "Create vacation report", "Return to main menu" };
                int userChoice = UtilityMethods.DisplayMenuAndGetUserChoice(menu, "Menu Choices (Employee Menu)");
                switch (userChoice)
                {
                    case 1:
                        //run create vacationreport method from the reporthandler
                        _reportHandler.CreateVacationReport();
                        break;
                    case 2:
                        //go back to main menu
                        run = false;
                        break;
                }
            }

        }

    }
}
