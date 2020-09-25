using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment02 //Todo, fixa så att om man skriver in "blankt" på name, text på Price så skrivs ett felmeddelande ut, istället för att krascha programmet.
{
    public class Program
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

                Console.Clear();
                switch (selectedOption) // Menu selection
                {
                    case 0:
                        expense.AddExpense();
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
                selected = key switch
                {
                    ConsoleKey.DownArrow => Math.Min(selected + 1, options.Length - 1),
                    ConsoleKey.UpArrow => Math.Max(selected - 1, 0),
                    _ => selected
                };
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