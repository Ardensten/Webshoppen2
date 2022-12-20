﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshoppen2.Models
{
    internal class Methods
    {
        internal static void Running()
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  ");
                Console.ResetColor();
                Console.WriteLine($"\n\t\t  Welcome to our beautiful webshop! Here can you buy spirits and get wasted!\n\n"
                    + "Top three products."
                    + "\n1. Free text search."
                    + "\n2. Categories."
                    + "\n3. Admin.");

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
                        Admin();
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
                Console.WriteLine($"1. Beer"
                    + "\n2. Wine"
                    + "\n3. Spirits");               

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        ShowAllBeer();
                        Console.WriteLine("Choose a beer: ");
                        CartChoice();
                        break;
                    case '2':
                        //ShowAllWine();
                        Console.WriteLine("Choose a wine: ");
                        CartChoice();
                        break;
                    case '3':
                        //ShowAllSpirits();
                        Console.WriteLine("Choose spirits: ");
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

        private static void ShowAllBeer()
        {
            using (var db = new webshoppenContext())
            {
                foreach(var p in db.Products.Include(s => s.Supplier)) 
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine($"\nList of beers. Enjoy!");
                    Console.ResetColor();
                    Console.Write($" - [ID: {p.Id}] {p.Name}, Info: {p.InfoText} Price: {p.Price}SEK, from the company");
                    foreach (var s in db.Suppliers)
                    {
                        Console.WriteLine($" {s.Name}.");
                    }

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
                //MethodCart();
            }
            else if (choice == "n")
            {
                Categories();
            }
            else
            {
                InputInstructions();
                Categories();
            }
        }

        private static void Admin()
        {
            Console.WriteLine($"1. Add a product."
                +"\n2. Change a product."
                +"\n3. Delete a product.");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case '1':
                    AddProduct();
                    break;
                case '2':
                    //Edit product
                    break;
                case '3':
                    //Remove product
                    break;
                default:
                    InputInstructions();
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void AddProduct()
        {
            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter product price: ");
            var price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter product supplier-id: ");
            int supplierId = Convert.ToInt32(Console.ReadLine());
            //ShowListSupplier();

            Console.WriteLine("Enter product category-id: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            //ShowListCategory();

            Console.WriteLine("Enter product information text: ");
            var infotext = Console.ReadLine();
            Console.WriteLine("Enter number of products in stock: ");
            int unitsInStock = Convert.ToInt32(Console.ReadLine());

            using (var db = new webshoppenContext())
            {
                var newProduct = new Product
                {
                    Name = name,
                    Price = price,
                    SupplierId = supplierId,
                    CategoryId = categoryId,
                    InfoText = infotext,
                    UnitsInStock = unitsInStock
                };
                var productList = db.Products;
                productList.Add(newProduct);
                db.SaveChanges();
            }
        }

        internal static void InputInstructions()
        {
            Console.WriteLine("Wrong input. Try something else!");
        }
    }
}
