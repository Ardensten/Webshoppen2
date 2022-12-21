using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Webshoppen2.AllMethods;

namespace Webshoppen2.Models
{
    internal class Methods
    {
        internal static void Running()
        {
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  ");
                Console.ResetColor();
                Console.WriteLine($"\n\t\t  Welcome to our beautiful webshop! Here can you buy spirits and get wasted!\n\n"
                    + "\n1. Free text search."
                    + "\n2. Categories."
                    + "\n3. Admin.");

                using(var db = new webshoppenContext())
                {
                    foreach (var p in db.Products.Where(p => p.ChosenProduct == true))
                    {
                        Console.Write($"{p.Name}\tPrice: {p.Price}SEK");
                    }
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        //MethodSearch()
                        break;
                    case '2':
                        Categories();
                        break;
                    case '3':
                        Admin.Menu();
                        break;
                    default:
                        InputInstructions();
                        break;
                }
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        internal static void Categories()
        {
            Console.Clear();
            bool runCategories = true;

            while (runCategories)
            {
                using (var db = new webshoppenContext())
                {
                    foreach (var p in db.Categories)
                    {
                        Console.WriteLine($"[{p.Id}] : {p.Name}");
                    }
                }
                Console.WriteLine("Input the category-id you wish to view: ");
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        ShowAllBeer();
                        Console.WriteLine("Choose a beer: ");
                        CartChoice();
                        break;
                    case '2':
                        ShowAllWine();
                        Console.WriteLine("Choose a wine: ");
                        CartChoice();
                        break;
                    case '3':
                        ShowAllSpirits();
                        Console.WriteLine("Choose spirits: ");
                        CartChoice();
                        break;
                    case '4':
                        ShowAllChampagne();
                        Console.WriteLine("Choose a champagne: ");
                        CartChoice();
                        break;
                    case '5':
                        ShowAllCider();
                        Console.WriteLine("Choose a cider: ");
                        CartChoice();
                        break;
                    default:
                        InputInstructions();
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ShowAllChampagne()  //slå ihop alla metoder och ta categori id som en inparameter!!!!!!!!!!!!!!!!!!
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nList of all champagne. Enjoy!");
                Console.ResetColor();
                foreach (var p in db.Products.Where(x => x.CategoryId == 4).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\tPrice: {p.Price}SEK \t\t{inStock}\t{p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowAllCider()
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nList of all ciders. Enjoy!");
                Console.ResetColor();
                foreach (var p in db.Products.Where(x => x.CategoryId == 5).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\tPrice: {p.Price}SEK \t\t{inStock}\t{p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowAllSpirits()
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nList of all spirits. Enjoy!");
                Console.ResetColor();
                foreach (var p in db.Products.Where(x => x.CategoryId == 3).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\tPrice: {p.Price}SEK \t\t{inStock}\t{p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowAllBeer()
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nList of beers. Enjoy!");
                Console.ResetColor();
                foreach (var p in db.Products.Where(x => x.CategoryId == 1).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\tPrice: {p.Price}SEK \t\t{inStock}\t{p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }
        private static void ShowAllWine()
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nList of all wine. Enjoy!");
                Console.ResetColor();
                foreach (var p in db.Products.Where(x => x.CategoryId == 2).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\tPrice: {p.Price}SEK \t\t{inStock}\t{p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }

        internal static void CartChoice()
        {

            Console.WriteLine("Add to cart?");
            var choice = Console.ReadLine();
            if (choice == "y")
            {
                //AddProductToCart();
            }
            else if (choice == "n")
            {
                Categories();
            }
            else
            {
                InputInstructions();
            }
        }

        internal static void InputInstructions()
        {
            Console.WriteLine("Wrong input. Try something else!");
        }
    }
}
