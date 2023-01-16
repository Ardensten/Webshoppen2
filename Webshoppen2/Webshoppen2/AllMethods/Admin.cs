using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Webshoppen2.Migrations;
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
                    + "\n[5] Add Supplier."
                    + "\n[6] Change customer-info."
                    + "\n[7] View purchase histories."
                    + "\n[8] Log out.");

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
                        AddSupplier();
                        break;
                    case '6':
                        ChangeCustomerInfo();
                        break;
                    case '7':
                        ViewPurchaseHistory();
                        break;
                    case '8':
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

        private static void AddSupplier()
        {
            Console.WriteLine("Enter supplier name: ");
            string name = Console.ReadLine();

            ShowListCities();
            Console.WriteLine("\nEnter city-id: ");
            int cityId = Methods.TryNumberInt();

            using (var db = new webshoppenContext())
            {
                var newSupplier = new Supplier
                {
                    Name = name,
                    CityId = cityId
                };
                var supplierList = db.Suppliers;
                supplierList.Add(newSupplier);
                db.SaveChanges();
            }
        }

        private static void ShowListCities()
        {
            using (var db = new webshoppenContext())
            {
                var cities = db.Cities;
                int i = 10;
                Console.SetCursorPosition(100, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("City list.");
                Console.ResetColor();
                foreach (var c in cities)
                {
                    i++;
                    Console.SetCursorPosition(100, i);
                    Console.WriteLine($"{c.Id} {c.Name} ");
                }

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

                Console.WriteLine("Input orderId you want to see more of:");
                var orderId = Methods.TryNumberFloat();
                var carts = from c in db.Carts
                            join p in db.Products on c.ProductId equals p.Id
                            where c.OrderId == orderId
                            select new { ProductName = p.Name, ProductPrice = p.Price, ProductAmount = c.AmountofUnits };

                double? totalCost = 0;
                foreach (var cart in carts)
                {
                    totalCost += cart.ProductPrice * cart.ProductAmount;
                    Console.WriteLine($"Product: {cart.ProductName}\n" +
                        $"Amount of products: {cart.ProductAmount}\n" +
                        $"Total Cost of {cart.ProductName}: {cart.ProductPrice * cart.ProductAmount}");
                }
                totalCost = (double)System.Math.Round((double)totalCost, 2);
                Console.WriteLine("Total cost of order: " + totalCost); 
            }
        }

        private static void ChangeCustomerInfo()
        {
            ShowListCustomers();
            using (var db = new webshoppenContext())
            {
                Console.WriteLine("Input ID number of the customer you want to edit.");
                var customerId = Methods.TryNumberInt();


                bool runMenu = false;
                while (!runMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t     _    ____  __  __ ___ _   _ \r\n\t\t\t\t    / \\  |  _ \\|  \\/  |_ _| \\ | |\r\n\t\t\t\t   / _ \\ | | | | |\\/| || ||  \\| |\r\n\t\t\t\t  / ___ \\| |_| | |  | || || |\\  |\r\n\t\t\t\t /_/   \\_\\____/|_|  |_|___|_| \\_|\r\n\n");
                    Console.ResetColor();

                    foreach (var customer in db.Customers.Where(x => x.Id == customerId))
                    {
                        Console.WriteLine($"Name: {customer.Name} \n" +
                            $"Social Security number: {customer.SocialSecurityNumber} \n" +
                            $"Phone number: {customer.PhoneNumber} \n" +
                            $"Email: {customer.Email} \n" +
                            $"City: {customer.City} \n" +
                            $"Adress: {customer.Adress} \n");
                    }

                    Console.WriteLine("What do you want to edit?\n" +
                                  "[1] : Name\n" +
                                  "[2] : Social security number\n" +
                                  "[3] : Phone number\n" +
                                  "[4] : Email\n" +
                                  "[5] : City ID\n" +
                                  "[6] : Adress\n" +
                                  "[7] : Return to main menu");

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("What is the new name");
                            var newName = Console.ReadLine();
                            var customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.Name = newName;
                                db.SaveChanges();
                            }
                            break;

                        case '2':
                            Console.WriteLine("Enter the updated social security number: ");
                            var newSocialSecurityNumber = Methods.TryNumberLong();
                            customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.SocialSecurityNumber = newSocialSecurityNumber;
                                db.SaveChanges();
                            }
                            break;

                        case '3':
                            Console.WriteLine("Enter the updated phone number: ");
                            var newPhoneNumber = Methods.TryNumberInt();
                            customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.PhoneNumber = newPhoneNumber;
                                db.SaveChanges();
                            }
                            break;

                        case '4':
                            Console.WriteLine("Enter the updated email: ");
                            var newEmail = Console.ReadLine();
                            customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.Email = newEmail;
                                db.SaveChanges();
                            }
                            break;

                        case '5':
                            Console.WriteLine("Enter the new city ID: ");
                            ShowListCities();
                            var newCityID = Methods.TryNumberInt();
                            customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.CityId = newCityID;
                                db.SaveChanges();
                            }
                            break;
                        case '6':
                            Console.WriteLine("Enter the new address: ");
                            var newAddress = Console.ReadLine();
                            customer = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

                            if (customer != null)
                            {
                                customer.Adress = newAddress;
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


        private static void ShowListCustomers()
        {
            using (var db = new webshoppenContext())
            {
                var customers = db.Customers;
                int i = 10;
                Console.SetCursorPosition(100, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Customer list.");
                Console.ResetColor();
                foreach (var c in customers)
                {
                    i++;
                    Console.SetCursorPosition(70, i);
                    Console.WriteLine($"{c.Id} {c.Name} {c.SocialSecurityNumber}");
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
            double price = Methods.TryNumberDouble();
            Console.WriteLine("\nEnter product supplier-id: ");
            ShowListSupplier();
            Console.SetCursorPosition(0, 22);
            int supplierId = Methods.TryNumberInt();

            Console.WriteLine("\nEnter product category-id: ");
            ShowListCategory();
            Console.SetCursorPosition(0, 24);

            int categoryId = Methods.TryNumberInt();
            Console.WriteLine("\nEnter product information text: ");
            var infotext = Console.ReadLine();
            Console.WriteLine("\nEnter number of products in stock: ");
            int unitsInStock = Methods.TryNumberInt();

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
            Console.SetCursorPosition(0, 17);
            var categoryId = Methods.TryNumberInt();
            Console.WriteLine("Input id of product you want to edit");
            ShowListproducts(categoryId);
            Console.SetCursorPosition(0, 18);
            var productId = Methods.TryNumberInt();


            bool runMenu = false;
            while (!runMenu)
            {
                using (var db = new webshoppenContext())
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t     _    ____  __  __ ___ _   _ \r\n\t\t\t\t    / \\  |  _ \\|  \\/  |_ _| \\ | |\r\n\t\t\t\t   / _ \\ | | | | |\\/| || ||  \\| |\r\n\t\t\t\t  / ___ \\| |_| | |  | || || |\\  |\r\n\t\t\t\t /_/   \\_\\____/|_|  |_|___|_| \\_|\r\n\n");
                    Console.ResetColor();

                    foreach (var product in db.Products.Where(x => x.Id == productId))
                    {
                        Console.WriteLine($"Name: {product.Name} \n" +
                            $"Price: {product.Price} \n" +
                            $"Infotext: {product.InfoText} \n" +
                            $"Units in stock: {product.UnitsInStock} \n" +
                            $"Chosen product status: {product.ChosenProduct} \n");

                    }

                    Console.WriteLine("What do you want to edit?\n" +
                        "[1] : Name\n" +
                        "[2] : Price\n" +
                        "[3] : Infotext\n" +
                        "[4] : Units in Stock\n" +
                        "[5] : Add product to front page\n" +
                        "[6] : Remove product from front page\n" +
                        "[7] : Return to main menu");

                    ConsoleKeyInfo key = Console.ReadKey(true);
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
                            var newPrice = Methods.TryNumberDouble();
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
                            var newUnitsInStock = Methods.TryNumberInt();
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


