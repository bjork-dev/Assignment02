using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpenseCalculator
{
    public class Expense
    {
        public string Name;
        public decimal Price;
        public string Category;
        public string[] arrayCategory = { "Food", "Entertainment", "Other" };
        public List<Expense> expenseList = new List<Expense>();

        public void NewExpense(string[] arrayCategory) // Reads the entered variables + selected category from the ShowMenu method.
        {
            Console.Clear();
            Console.WriteLine("Add expense: ");
            Console.Write("Name: ");
            Name = Console.ReadLine();
            Console.Write("Price: ");
            Price = decimal.Parse(Console.ReadLine());
            int index_category = Program.ShowMenu("Category:", arrayCategory);
            Category = arrayCategory[index_category];
        }

        //Return the sum of all expenses with the specified category in the specified list, or the sum of all expenses if the category is null.
        public static decimal SumExpenses(List<Expense> expenses, string category = null) // Returns the sum of all expenses combined.
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

        public void ShowAllExpenses() //Displays all elements in the expenseList.
        {
            Console.Clear();
            Console.WriteLine("All expenses:\n");
            foreach (Expense e in expenseList)
            {
                Console.WriteLine("- " + e.Name + ": " + e.Price + " kr (" + e.Category + ")");
            }
            Console.WriteLine("\nSum: " + Expense.SumExpenses(expenseList) + " kr"); //Show the total sum
        }

        public void ShowSum() //Displays the sum divided by the categories.
        {
            Console.Clear();
            Console.WriteLine("Sum by category:\n");
            foreach (string str in arrayCategory)
            {
                Console.WriteLine(str + ": " + Expense.SumExpenses(expenseList, str) + " kr");
            }
        }

        public void RemoveExpense() //Removes the selected index in expenseList by comparing "i" to "select".
        {
            List<string> names = new List<string>();
            Console.Clear();
            foreach (Expense e in expenseList)
            {
                names.Add(e.Name);
            }
            var list = names.ToArray();
            Console.WriteLine("Which expense do you want to remove?");
            int select = Program.ShowMenu("Expenses:", list);
            int i = 0;
            foreach (Expense e in expenseList) // Tydligen fungerar detta
            {
                if (i == select)
                {
                    expenseList.Remove(e);
                    Console.WriteLine(e.Name + "removed.");
                    break;
                }
                i++;
            }
        }

        public void RemoveAllExpenses()
        {
            int select = Program.ShowMenu("Remove all expenses?:", new[]
                {
                "Yes",
                "No"
                });
            if (select == 0)
            {
                expenseList.Clear();
                Console.Clear();
                Console.WriteLine("All expenses removed.");
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Expense p = new Expense();
            var running = true;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Console.WriteLine("Welcome!");
            while (running)
            {
                int selectedOption = ShowMenu("\nMenu:", new[]
                {
                    "Add Expense",
                    "Show All Expenses",
                    "Show Sum by Category",
                    "Remove Expense",
                    "Remove All Expenses",
                    "Exit"
                });

                Console.Clear();
                switch (selectedOption) // Menu selection
                {
                    case 0:                                     // Add Expense
                        Expense purchase = new Expense();       // Create a temporary object of the Expense class
                        purchase.NewExpense(p.arrayCategory);   // Fill in the object
                        p.expenseList.Add(purchase);            // Add the object to the List of expenses
                        Console.Clear();
                        Console.WriteLine("Expense added!");       //Kan deta förenklas så att enbart "p.NewExpense()" ligger här?
                        break;

                    case 1:                                     // Show All Expenses
                        p.ShowAllExpenses();
                        break;

                    case 2:                                     //Show Sum by Category
                        p.ShowSum();
                        break;

                    case 3:
                        p.RemoveExpense();
                        break;

                    case 4:
                        p.RemoveAllExpenses();
                        break;

                    case 5: //Exit program
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                }
            }
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