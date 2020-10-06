using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment02
{
    public class Expense
    {
        public string Name;
        public decimal Price;
        public string Category;
        public static string[] ArrayCategory = {"Food", "Entertainment", "Other"};
        public static List<Expense> ExpenseList = new List<Expense>();

        public void
            NewExpense() // Reads purchase information from the console and adds this purchase to the ExpenseList
        {
            Console.Clear();
            Console.WriteLine("Add expense: ");
            string userInput;
            string name;
            decimal price;

            while (true)
            {
                Console.Write("Name: ");
                userInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    name = userInput;
                    break;
                }

                Console.Clear();
                Console.WriteLine("Error! Please, enter a name.");
            }

            while (true)
            {
                Console.Write("Price: ");
                userInput = Console.ReadLine();
                try
                {
                    price = decimal.Parse(userInput);
                    break;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Error! Please, enter a number.");
                }
            }

            int indexCategory = Program.ShowMenu("Category:", Expense.ArrayCategory);
            var category = Expense.ArrayCategory[indexCategory];

            var purchase = new Expense
            {
                Name = name,
                Price = price,
                Category = category
            };

            ExpenseList.Add(purchase);
            Console.Clear();
            Console.WriteLine("Expense added!");
        }

        public static decimal
            SumExpenses(List<Expense> expenses,
                string category =
                    null) //Return the sum of all expenses with the specified category in the specified list, or the sum of all expenses if the category is null.
        {
            decimal sum = 0;
            foreach (var element in expenses)
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
            foreach (var e in ExpenseList)
            {
                Console.WriteLine("- " + e.Name + ": " + e.Price + " kr (" + e.Category + ")");
            }

            Console.WriteLine("\nSum: " + Expense.SumExpenses(ExpenseList) + " kr"); //Show the total sum
        }

        public void ShowSum() //Displays the sum divided by the categories.
        {
            Console.Clear();
            Console.WriteLine("Sum by category:\n");
            foreach (string str in ArrayCategory)
            {
                Console.WriteLine(str + ": " + Expense.SumExpenses(ExpenseList, str) + " kr");
            }
        }

        public void RemoveExpense() //Removes the selected expense
        {
            Console.Clear();
            var list = ExpenseList.Select(e => e.Name).ToArray();
            Console.WriteLine("Which expense do you want to remove?");
            try
            {
                int select = Program.ShowMenu("Expenses:", list);
                ExpenseList.RemoveAt(select);
                Console.Clear();
                Console.WriteLine("Expense removed!");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("You cannot remove nothing, add an expense first.");
            }

        }

        public void RemoveAllExpenses()
        {
            Console.Clear();
            if (ExpenseList.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You cannot remove nothing, add an expense first.");
            }
            else
            {
                int select = Program.ShowMenu("Remove all expenses?:", new[]
                {
                    "Yes",
                    "No"
                });

                if (@select != 0) return;
                ExpenseList.Clear();
                Console.Clear();
                Console.WriteLine("All expenses removed.");
            }

        }
    }

    public static class Program
    {
        public static void Main()
        {
            var p = new Expense();
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


                switch (selectedOption) // Menu selection
                {
                    case 0:
                        p.NewExpense();
                        break;
                    case 1:
                        p.ShowAllExpenses();
                        break;

                    case 2:
                        p.ShowSum();
                        break;

                    case 3:
                        p.RemoveExpense();
                        break;

                    case 4:
                        p.RemoveAllExpenses();
                        break;

                    case 5: //Exit program
                        Console.Clear();
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
                selected = key switch
                {
                    ConsoleKey.DownArrow => Math.Min(selected + 1, options.Length - 1),
                    ConsoleKey.UpArrow => Math.Max(selected - 1, 0),
                    _ => selected
                };
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
        public void SumTest() //Work in progress
        {
            var Test = new Expense{ Name ="Water", Price = 100};
            var Test2 = new Expense{ Name ="Bills", Price = 300};
            Expense.ExpenseList.Add(Test);
            Expense.ExpenseList.Add(Test2);
            var sum = Expense.SumExpenses(Expense.ExpenseList, null);
            Assert.AreEqual(400, sum);


        }
    }
}
