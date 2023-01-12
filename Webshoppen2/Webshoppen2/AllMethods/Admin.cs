﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Webshoppen2.Models;

namespace Webshoppen2.AllMethods
{
    internal class Admin
    {
        internal static void Menu()
        {
            bool runMenu = false;
            while (!runMenu)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\t\t     _    ____  __  __ ___ _   _ \r\n\t\t\t\t    / \\  |  _ \\|  \\/  |_ _| \\ | |\r\n\t\t\t\t   / _ \\ | | | | |\\/| || ||  \\| |\r\n\t\t\t\t  / ___ \\| |_| | |  | || || |\\  |\r\n\t\t\t\t /_/   \\_\\____/|_|  |_|___|_| \\_|\r\n\n");
                Console.ResetColor();
                Console.WriteLine($"[1] Add a product."
                    + "\n[2] Change a product."
                    + "\n[3] Delete a product."
                    + "\n[4] Add category."
                    + "\n[5] Change customer-info."
                    + "\n[6] View purchase histories."
                    + "\n[7] Log out.");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        AddProduct();
                        break;
                    case '2':
                        EditProduct();
                        break;
                    case '3':
                        RemoveProduct();
                        break;
                    case '4':
                        AddCategory();
                        break;
                    case '5':
                        ChangeCustomerInfo();
                        break;
                    case '6':
                        ViewPurchaseHistory();
                        break;
                    case '7':
                        runMenu = true;
                        break;
                    default:
                        Methods.InputInstructions();
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ViewPurchaseHistory()
        {
            using (var db = new webshoppenContext())
            {
                Console.WriteLine("Input Security number that you want to check: ");
                long securityNumber = Methods.TryNumberLong();
                var purchaseHistory = (from c in db.Customers
                                       join o in db.OrderHistories on c.Id equals o.CustomerId
                                       where c.SocialSecurityNumber == securityNumber
                                       select new { SocialSecurityNumber = c.SocialSecurityNumber, OrderNumber = o.CheckoutCartOrderId, CustomerId = o.CustomerId }).ToList();

                foreach (var purchase in purchaseHistory)
                {
                    Console.WriteLine($"{purchase.OrderNumber}");
                }
            }
        }

        private static void ChangeCustomerInfo()
        {
            using (var db = new webshoppenContext())
            {
                foreach (var c in db.Customers)
                {
                    Console.Write($"{c.Name}\t{c.SocialSecurityNumber}");
                }
            }
        }

        private static void AddCategory()
        {
            Console.WriteLine("Enter catgory name: ");
            string name = Console.ReadLine();
            using (var db = new webshoppenContext())
            {
                var newCategory = new Category
                {
                    Name = name
                };
                var categoryList = db.Categories;
                categoryList.Add(newCategory);
                db.SaveChanges();
            }
        }

        private static void AddProduct()
        {

            Console.WriteLine("\nEnter product name: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nEnter product price: ");
            double price = 0; price = Methods.TryNumberDouble();
            Console.WriteLine("\nEnter product supplier-id: ");
            ShowListSupplier();
            Console.SetCursorPosition(0, 21);
            int supplierId = 0; supplierId = Methods.TryNumberInt();

            Console.WriteLine("\nEnter product category-id: ");
            ShowListCategory();
            Console.SetCursorPosition(0, 24);

            int categoryId = 0; categoryId = Methods.TryNumberInt();
            Console.WriteLine("\nEnter product information text: ");
            var infotext = Console.ReadLine();
            Console.WriteLine("\nEnter number of products in stock: ");
            int unitsInStock = 0; unitsInStock = Methods.TryNumberInt();

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

        private static void ShowListproducts(int categoryId)
        {
            using (var db = new webshoppenContext())
            {
                var products = db.Products;
                int i = 10;
                Console.SetCursorPosition(70, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product list.");
                Console.ResetColor();
                foreach (var p in products.Where(p => p.CategoryId == categoryId))
                {
                    i++;
                    Console.SetCursorPosition(70, i);
                    Console.WriteLine($"{p.Id} {p.Name} ");
                }

            }
        }

        private static void ShowListSupplier()
        {
            using (var db = new webshoppenContext())
            {
                var suppliers = db.Suppliers;
                int i = 10;
                Console.SetCursorPosition(70, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Suppliers list.");
                Console.ResetColor();
                foreach (var s in suppliers)
                {
                    i++;
                    Console.SetCursorPosition(70, i);
                    Console.WriteLine($"{s.Id} {s.Name} ");
                }

            }
        }

        private static void ShowListCategory()
        {
            using (var db = new webshoppenContext())
            {
                var categories = db.Categories;
                int i = 10;
                Console.SetCursorPosition(100, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Categories list.");
                Console.ResetColor();
                foreach (var c in categories)
                {
                    i++;
                    Console.SetCursorPosition(100, i);
                    Console.WriteLine($"{c.Id} {c.Name} ");
                }

            }
        }

        public static void RemoveProduct()
        {
            Console.WriteLine("Input id of product you want to delete");
            var productId = 0; productId = Methods.TryNumberInt();

            using (var db = new webshoppenContext())
            {
                var product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                if (product != null)
                {
                    db.Products.Remove((Product)product);
                    db.SaveChanges();
                }
            }
        }

        public static void EditProduct()
        {
            Console.WriteLine("\nInput id of the category you want to edit");
            ShowListCategory();
            Console.SetCursorPosition(0, 16);
            var categoryId = Methods.TryNumberInt();
            Console.WriteLine("Input id of product you want to edit");
            ShowListproducts(categoryId);
            Console.SetCursorPosition(0, 18);
            var productId = Methods.TryNumberInt();

            Console.WriteLine("What do you want to edit?\n" +
                "[1] : Name\n" +
                "[2] : Price\n" +
                "[3] : Infotext\n" +
                "[4] : Units in Stock\n" +
                "[5] : Add product to front page\n" +
                "[6] : Remove product from front page\n" +
                "[7] : Return to main menu");

            bool runMenu = false;
            while (!runMenu)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                using (var db = new webshoppenContext())
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("What is the new name");
                            var newName = Console.ReadLine();
                            var product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.Name = newName;
                                db.SaveChanges();
                            }
                            break;

                        case '2':
                            Console.WriteLine("Enter the updated price: ");
                            var newPrice = 0; Methods.TryNumberDouble();
                            product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.Price = newPrice;
                                db.SaveChanges();
                            }
                            break;

                        case '3':
                            Console.WriteLine("Enter the updated info-text: ");
                            var newText = Console.ReadLine();
                            product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.InfoText = newText;
                                db.SaveChanges();
                            }
                            break;

                        case '4':
                            Console.WriteLine("Enter the updated amount of units in stock: ");
                            var newUnitsInStock = 0; newUnitsInStock = Methods.TryNumberInt();
                            product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.UnitsInStock = newUnitsInStock;
                                db.SaveChanges();
                            }
                            break;

                        case '5':
                            Console.WriteLine("Added product to front page. ");
                            product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.ChosenProduct = true;
                                db.SaveChanges();
                            }
                            break;
                        case '6':
                            Console.WriteLine("Removed product to front page. ");
                            product = db.Products.Where(x => x.Id == productId).SingleOrDefault();

                            if (product != null)
                            {
                                product.ChosenProduct = false;
                                db.SaveChanges();
                            }
                            break;
                        case '7':
                            runMenu = true;
                            break;

                        default:
                            Methods.InputInstructions();
                            break;
                    }
                }
            }
        }
    }
}
