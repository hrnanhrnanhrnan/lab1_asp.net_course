using System;
using System.Collections.Generic;
using System.Text;
using Data.Handlers;
using Data.Utilities;

namespace lab1_asp.UI
{
    internal class UserInterface
    {
        ReportHandler _reportHandler = new ReportHandler();
        EmployeeHandler _employeeHandler = new EmployeeHandler();
        public void RunUserInterface()
        {
            bool run = true;
            Console.WriteLine("------Welcome to the application!------");
            Console.WriteLine("\tPress enter to continue");
            Console.ReadLine();
            while (run)
            {
                string[] menu = new string[] { "Admin", "Employee", "Exit"};
                int userChoice = UtilityMethods.DisplayMenuAndGetUserChoice(menu, "Menu choices: ");
                switch(userChoice)
                {
                    case 1:
                        //run admin menu
                        RunAdminMenu();
                        break;
                    case 2:
                        //run employee menu
                        RunPersonelMenu();
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
                        //run something to search for employees and see if they have any reported vacations
                        _employeeHandler.SearchAndDisplayEmployee();
                        break;
                    case 2:
                        _reportHandler.DisplayReportsByMonth();
                        break;
                    case 3:
                        //go back to main menu
                        run = false;
                        break;
                }
            }
        }


        //Menu for personel, where they can create vacation reports
        private void RunPersonelMenu()
        {
            bool run = true;
            while (run)
            {
                string[] menu = new string[] { "Create vacation report", "Return to main menu" };
                int userChoice = UtilityMethods.DisplayMenuAndGetUserChoice(menu, "Menu Choices (Employee Menu)");
                switch (userChoice)
                {
                    case 1:
                        //run create vacationreport from the reporthandler
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
