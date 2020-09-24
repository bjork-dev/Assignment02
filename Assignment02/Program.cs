using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

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
                Console.Clear();
                var selectedOption = Menu.ShowMenu("Menu", new[]
                {
                    "Add Expense", //TODO add expense class
                    "Show All Expenses", //TODO add methods
                    "Show Sum by Category",
                    "Remove Expense",
                    "Remove All Expenses",
                    "Exit"
                });

                switch (selectedOption)
                {
                    case 0: // Menu selection TODO add method
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
            using FakeConsole console = new FakeConsole("First input", "Second input");
            Program.Main();
            Assert.AreEqual("Hello!", console.Output);
        }
    }
}