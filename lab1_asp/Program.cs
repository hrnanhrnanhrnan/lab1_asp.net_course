using System;
using Data.Models;
using System.Collections.Generic;
using Data.Utilities;
using lab1_asp.UI;
using System.Configuration;
using Data.Context;
using System.Linq;
using Data.Handlers;

namespace lab1_asp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInterface = new UserInterface();
            userInterface.RunUserInterface();
        }
    }
}
