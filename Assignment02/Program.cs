using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseCalculator
{
    public class Expense
    {
        public string Name;
        public decimal Price;
        public string Category;

        public void NewExpense(string[] array_category) // Fill in an object of the Expense class
        {
            Console.WriteLine("Add expense: ");
            Console.Write("name: ");
            Name = Console.ReadLine();
            Console.Write("price: ");
            Price = decimal.Parse(Console.ReadLine());
            int index_category = Program.ShowMenu("Category:", array_category);
            Category = array_category[index_category];
        }
    }

    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Welcome!");

            string[] array_category = { "Food", "Entertainment", "Other" };
            List<Expense> expense_list = new List<Expense>();   // List of expenses
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

                Console.Clear();
                switch (selectedOption) // Menu selection 
                {
                    case 0:                                             // Add Expense

                        Expense purchase = new Expense(); // Create a temporary object of the Expense class
                        purchase.NewExpense(array_category); // Fill in the object 
                        expense_list.Add(purchase);         // Add the object to the List of expenses

                        Console.Clear();
                        Console.WriteLine("Expense added!");
                        break;

                    case 1:                                                // Show All Expenses
                        Console.WriteLine("All expenses:\n");
                        foreach (Expense e in expense_list)
                        {
                            Console.WriteLine("- " + e.Name + ": " + e.Price + " kr (" + e.Category + ")");
                        }
                        Console.WriteLine("\nSum: " + SumExpenses(expense_list) + " kr"); //Show the total sum
                        break;

                    case 2:                                                 //Show Sum by Category
                        Console.WriteLine("Sum by category:\n");
                        foreach (string str in array_category)
                        {
                            Console.WriteLine(str + ": " + SumExpenses(expense_list, str) + " kr");
                        }
                        break;

                    //case 3:
                    //    expense.RemoveExpense();
                    //    break;

                    //case 4:
                    //    expense.RemoveAllExpenses();
                    //    break;

                    case 5: //Exit program
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                }
            }
        }

        //Return the sum of all expenses with the specified category in the specified list, or the sum of all expenses if the category is null.
        public static decimal SumExpenses(List<Expense> expenses, string category = null)
        {
            decimal sum = 0;
            foreach (Expense element in expenses)
            {
                if (category == null)
                {
                    sum += element.Price;
                }
                else if (element.Category == category)
                {
                    sum += element.Price;
                }
            }
            return sum;
        }

        // Don't change this method.
        public static int ShowMenu(string prompt, string[] options)
        {
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty array of options.");
            }

            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return selected;
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