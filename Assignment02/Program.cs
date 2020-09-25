using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment02
{
    public class Program
    {
        public static void Main()
        {
            // We need this to make sure we can always use periods for decimal points.
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var expense = new ExpenseManager();
            Console.Clear();
            var running = true;
            while (running)
            {
                int selectedOption = ShowMenu("\nMenu:", new[]
                {
                    "Add Expense",
                    "Show All Expenses",
                    "Show Sum by Category",
                    "Remove Expense",   //TODO add methods
                    "Remove All Expenses",
                    "Exit"
                });

                switch (selectedOption) // Menu selection 
                {
                    case 0:
                        expense.AddExpense();
                        break;

                    case 1:
                        expense.ShowAllExpenses();
                        break;

                    case 2:
                        expense.ShowCategorySum();
                        break;

                    case 3:
                        expense.RemoveExpense();
                        break;

                    case 4:
                        expense.RemoveAllExpenses();
                        break;

                    case 5: //Exit program
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                }
            }
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ExampleTest()
        {
            // Code needed here.
        }
    }
}