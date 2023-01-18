using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Webshoppen2.AllMethods;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Webshoppen2.Models
{
    internal class Methods
    {
        static int loggedInId;
        internal static void Running()
        {
            Console.Clear();
            while (true)
            {
                PrintLogo();
                Console.WriteLine($"\n\t\t  Welcome to our beautiful webshop! Here can you buy spirits and get wasted!\n\n"
                    + "\n1. Log in."
                    + "\n2. Sign up."
                    + "\n3. Admin log in.");



                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        UserLogIn();
                        break;
                    case '2':
                        SignUp();
                        break;
                    case '3':
                        Admin.LoginAdmin();
                        break;
                    default:
                        InputInstructions();
                        break;
                }
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        public static void SignUp()
        {
            Console.Write($"Name: ");
            string name = TryStringIn();
            Console.Write("Social security number: ");
            long socialSecurityNumber = TryNumberLong(); // detta är en unique nu, det krashar om man skriver samma, säkra detta
            Console.Write("Phone number: ");
            int phoneNumber = TryNumberInt();
            Console.Write("Email (Optional): ");
            string email = Console.ReadLine();

            using (var db = new webshoppenContext())
            {
                Console.Write("Choose a city by writing the corresponding id: ");
                Admin.ShowListCities();
                Console.SetCursorPosition(0, 18);
                int cityId = TryNumberInt();
                Console.Write("Adress: ");
                string adress = TryStringIn();

                var newCustomer = new Customer
                {
                    Name = name,
                    SocialSecurityNumber = socialSecurityNumber,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    CityId = cityId,
                    Adress = adress
                };
                var customerList = db.Customers;
                customerList.Add(newCustomer);
                db.SaveChanges();
            }
        }

        public static void UserLogIn()
        {
            Console.WriteLine("Please enter your social security number (YYYYMMDDXXXX): ");
            long socialSecurityNumber = TryNumberLong();
            //Console.WriteLine("Log in with your BankID");
            //Thread.Sleep(1000);
            //Console.Write(".");
            //Thread.Sleep(1000);
            //Console.Write(".");
            //Thread.Sleep(1000);
            //Console.WriteLine(".");
            //Console.WriteLine("Login successful!");
            //Console.ReadKey();

            using (var db = new webshoppenContext())
            {
                var id = db.Customers.Where(i => i.SocialSecurityNumber == socialSecurityNumber);
                foreach (var i in id)
                {
                    loggedInId = i.Id;
                    StartPage(loggedInId);
                }
                if (loggedInId == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That user does not exist!");
                    Console.ResetColor();
                }
            }
        }

        public static void StartPage(int id)
        {
            Console.Clear();
            bool runMenu = false;
            int chosenProduct1 = 0;
            int chosenProduct2 = 0;
            int chosenProduct3 = 0;
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  ");
                Console.ResetColor();
                using (var db = new webshoppenContext())
                {
                    var customerName = db.Customers.Where(x => x.Id == loggedInId).Select(x => x.Name).FirstOrDefault();
                    Console.WriteLine($"\n\t\t  Welcome {customerName} to our beautiful webshop! Here can you buy spirits and get wasted!\n\n");
                    Console.WriteLine("\t\t\t\t\t\tRecommended products: ");


                    var chosenProducts = (from p in db.Products
                                          join c in db.Categories on p.CategoryId equals c.Id
                                          select new { ProductName = p.Name, ProductPrice = p.Price, ProductInStock = p.UnitsInStock, Productid = p.Id, ChosenProduct = p.ChosenProduct, CategoryId = c.Id, CategoryName = c.Name }).ToList();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\t\t\t\t \t\t\t  _\r\n \t\t\t\t\t\t\t {_}\r\n \t\t\t\t\t\t\t |(|\r\n\t\t\t\t\t\t\t |=|\r\n\t\t\t\t\t\t\t/   \\\t\t\t\t\t  [-] \r\n\t\t.~~~~.\t\t\t\t\t|.--| \t\t\t\t\t.-'-'-. \r\n\t\ti====i_\t\t\t\t\t||  |\t\t\t\t\t:-...-: \r\n\t\t|cccc|_)\t\t\t\t||  |\t\t\t\t\t|;:   | \r\n\t\t|cccc|   \t\t\t\t|'--|\t\t\t\t\t|;:.._|\r\n\t\t`-==-'\t\t\t\t\t'-=-'\t\t\t\t\t`-...-'");
                    Console.ResetColor();

                    foreach (var p in chosenProducts.OrderBy(p => p.CategoryId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (p.CategoryId == 1 && p.ChosenProduct == true)
                        {
                            Console.Write($"\n\t{p.ProductName}, {p.ProductPrice}SEK, {p.ProductInStock}*  | ");
                            chosenProduct1 = p.Productid;
                        }
                        else if (p.CategoryId == 2 && p.ChosenProduct == true)
                        {
                            Console.Write($"\t{p.ProductName}, {p.ProductPrice}SEK, {p.ProductInStock}*    |  ");
                            chosenProduct2 = p.Productid;
                        }
                        else if (p.CategoryId == 3 && p.ChosenProduct == true)
                        {
                            Console.Write($"\t{p.ProductName}, {p.ProductPrice}SEK,  {p.ProductInStock}*  \t");
                            chosenProduct3 = p.Productid;
                        }
                        Console.ResetColor();
                    }

                    Console.WriteLine("\n\n\t\t\t\t *Units in stock. To quick add recommended Press []. \n\n\t\t[B]eer \t\t\t\t\t[W]ine \t\t\t\t\t [S]pirits");

                    Console.WriteLine("\n\n\t\t1. Categories \t\t2. Search products \t\t3. Cart \t\t4. Log out");

                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        Categories();
                        break;
                    case '2':
                        Search();
                        break;
                    case '3':
                        ShowCart();
                        break;
                    case '4':
                        runMenu = true;
                        break;
                    case 'B':
                    case 'b':
                        AddProductToCart(chosenProduct1);
                        break;
                    case 'W':
                    case 'w':
                        AddProductToCart(chosenProduct2);
                        break;
                    case 'S':
                    case 's':
                        AddProductToCart(chosenProduct3);
                        break;
                    default:
                        InputInstructions();
                        break;
                }
                Console.Clear();
            }
        }

        private static void Search()  //Dapper
        {
            string connstring = "Server=tcp:grupp3skola.database.windows.net,1433;Initial Catalog=webshoppen;Persist Security Info=False;User ID=grupp3admin;Password=NUskavikoda1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Console.WriteLine("\nType your search.");
            var search = Console.ReadLine();
            var sql = $"SELECT * FROM Product WHERE Name like '%{search}%'";
            var products = new List<Models.Product>();

            using (var db = new SqlConnection(connstring))
            {
                products = db.Query<Models.Product>(sql).ToList();
            }

            Console.WriteLine();
            foreach (var product in products)
            {
                Console.WriteLine($"Id [{product.Id}]\t{product.Name}\t{product.Price}Sek");
            }
            if (products.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your search returned no results. Try something else!");
                Console.ResetColor();
            }
            else
            {
                ShowInfoOnProduct();
            }
            Console.ReadLine();
        }

        private static double ShowCart()
        {
            double? totalCostOfCart = 0;

            using (var db = new webshoppenContext())
            {
                var cart = (from c in db.Carts
                            join p in db.Products on c.ProductId equals p.Id
                            join cu in db.Customers on c.CustomerId equals cu.Id
                            where cu.Id == loggedInId
                            select new { Name = p.Name, Price = p.Price, AmountofUnits = c.AmountofUnits, CartProductId = c.ProductId, CartCustomerId = c.CustomerId, OrderId = c.OrderId }).ToList();


                Console.WriteLine();
                foreach (var item in cart)
                {
                    if (item.OrderId == null)
                    {
                        Console.WriteLine($"Id [{item.CartProductId}] {item.Name} {item.Price} {item.AmountofUnits}");
                        double? totalPerCartId = Convert.ToDouble(item.AmountofUnits) * item.Price;
                        totalPerCartId = (double)System.Math.Round((double)totalPerCartId, 2);
                        totalCostOfCart += totalPerCartId;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\nTotal cost of cart: {totalCostOfCart}\n\n");
                Console.ResetColor();

                bool runMenu = false;
                while (!runMenu)
                {
                    Console.WriteLine("[E] : Edit your cart \n[C] : Checkout\n[B] : Back to start page");
                    var choice = Console.ReadLine();
                    if (choice == "e" && totalCostOfCart > 0 || choice == "E" && totalCostOfCart > 0)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Which product do you want to change the amount of? ");
                        Console.ResetColor();
                        int cartProductId = TryNumberInt();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter the number you want in cart: ");
                        Console.ResetColor();
                        int productAmount = TryNumberInt();

                        var carts = from c in db.Carts
                                    join p in db.Products on c.ProductId equals p.Id
                                    where c.CustomerId == loggedInId
                                    select new
                                    {
                                        ProductId = c.ProductId,
                                        CustomerId = c.CustomerId,
                                        OrderId = c.OrderId,
                                        AmountOfUnits = c.AmountofUnits,
                                        UnitsInStock = p.UnitsInStock
                                    };

                        var products = db.Carts.Where(p => p.ProductId == cartProductId);
                        //var carts = db.Carts.Where(p => p.CustomerId == loggedInId);

                        foreach (var c in carts)
                        {
                            if (cartProductId == c.ProductId && c.OrderId == null)
                            {
                                if (productAmount <= 0 && products != null)
                                {
                                    foreach (var p in products)
                                    {
                                        db.Carts.Remove(p);
                                    }
                                }
                                else if (productAmount < c.UnitsInStock)
                                {
                                    foreach (var p in products)
                                    {
                                        p.AmountofUnits = productAmount;
                                    }
                                }
                                else if (productAmount > c.UnitsInStock)
                                {
                                    Console.WriteLine("The product does not have that many units in stock, please input a lower amount!");
                                }
                            }
                        }
                    }

                    else if (choice == "c" && totalCostOfCart > 0 || choice == "C" && totalCostOfCart > 0)
                    {
                        Checkout((double)totalCostOfCart);
                        runMenu = true;
                    }
                    else if (choice == "b" || choice == "B")
                    {
                        runMenu = true;
                    }
                    else
                    {
                        InputInstructions();
                        Console.WriteLine("Or there is nothing in your cart!");
                    }
                    db.SaveChanges();
                }
            }
            return (double)totalCostOfCart;
        }

        public static double Checkout(double totalCostOfCart)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("   ____ _               _               _   \r\n  / ___| |__   ___  ___| | _____  _   _| |_ \r\n | |   | '_ \\ / _ \\/ __| |/ / _ \\| | | | __|\r\n | |___| | | |  __/ (__|   < (_) | |_| | |_ \r\n  \\____|_| |_|\\___|\\___|_|\\_\\___/ \\__,_|\\__|");
            Console.ResetColor();
            using (var db = new webshoppenContext())
            {
                var checkout = (from c in db.Carts
                                join p in db.Products on c.ProductId equals p.Id
                                join cu in db.Customers on c.CustomerId equals cu.Id
                                where cu.Id == loggedInId
                                select new { Price = p.Price, AmountofUnits = c.AmountofUnits, CartId = c.Id, CartProductId = c.ProductId, CartCustomerId = c.CustomerId, OrderId = c.OrderId }).ToList();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Choose shipping method: ");

                foreach (var s in db.ShippingInfos)
                {
                    Console.WriteLine($"{s.Id}. {s.ParcelServiceName} {s.Cost} SEK\t\t");
                }

                Console.ResetColor();
                bool wrongInput = false;
                bool runPostalChoise = false;
                while (!runPostalChoise)
                {
                    Console.Write("\nEnter id of choice: ");
                    var shippingChoice = TryNumberInt();
                    foreach (var s in db.ShippingInfos.Where(x => x.Id == shippingChoice))
                    {
                        if (shippingChoice == s.Id)
                        {
                            totalCostOfCart += (double)s.Cost;
                            var newOrder = new OrderHistory
                            {
                                ShippingInfoId = shippingChoice,
                            };
                            var orderHistoryList = db.OrderHistories;
                            orderHistoryList.Add(newOrder);
                            runPostalChoise = true;
                            wrongInput = true;
                        }
                    }
                    if (wrongInput == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have to choose from options above!");
                        Console.ResetColor();
                    }
                    db.SaveChanges();
                }

                GetShippingInfo(totalCostOfCart);
            }
            return totalCostOfCart;
        }
        internal static void GetShippingInfo(double totalCostOfCart)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nWould you like to use your saved address as your shipping address? [y / n]: ");
            Console.ResetColor();
            bool runShippingInfo = false;
            while (!runShippingInfo)
            {

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "y" || choice == "Y")
                {
                    using (var db = new webshoppenContext())
                    {
                        var customerInfos = (from c in db.Customers
                                             join ci in db.Cities on c.CityId equals ci.Id
                                             where c.Id == loggedInId
                                             select new { StreetName = c.Adress, City = ci.Name, Name = c.Name });
                        foreach (var c in customerInfos)
                        {
                            Console.Write($"{c.Name}\n{c.StreetName}\n{c.City}");
                            var orders = db.OrderHistories.Where(x => x.CheckoutCartOrderId == null);
                            foreach (var o in orders)
                            {

                                o.ShippingAddress = c.StreetName;
                                o.ShippingCity = c.City;
                            }
                        }
                        db.SaveChanges();
                        runShippingInfo = true;
                    }
                }
                else if (choice == "n" || choice == "N")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nEnter the city: ");
                    Console.ResetColor();
                    var newCity = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter the street-name: ");
                    Console.ResetColor();
                    var newStreetName = Console.ReadLine();

                    using (var db = new webshoppenContext())
                    {
                        var shippingAddress = db.OrderHistories.Where(x => x.ShippingInfoId == 1);
                        foreach (var s in shippingAddress)
                        {
                            s.ShippingCity = newCity;
                            s.ShippingAddress = newStreetName;
                        }
                        db.SaveChanges();

                    }
                    runShippingInfo = true;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have to choose between Y or N. See above!");
                    Console.ResetColor();
                }
            }
            PayCheckout(totalCostOfCart);
        }
        internal static void PayCheckout(double totalCostOfCart)
        {
            using (var db = new webshoppenContext())
            {
                var cart = (from c in db.Carts
                            join p in db.Products on c.ProductId equals p.Id
                            join cu in db.Customers on c.CustomerId equals cu.Id
                            where cu.Id == loggedInId
                            select new { Name = p.Name, Price = p.Price, AmountofUnits = c.AmountofUnits, CartId = c.Id, CartProductId = c.ProductId, CartCustomerId = c.CustomerId, OrderId = c.OrderId }).ToList();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nYour cart:");
                Console.ResetColor();
                foreach (var item in cart)
                {
                    if (item.OrderId == null)
                    {
                        Console.WriteLine($"{item.CartId} {item.Name} {item.Price} {item.AmountofUnits}");
                    }
                }
                var vat = totalCostOfCart * 0.25;
                vat = (double)System.Math.Round((double)vat, 2);
                Console.WriteLine($"Total cost including shipping-cost and VAT: {totalCostOfCart}\nVAT: {vat}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n\nChoose payment-method: \n[D]ebit card | [I]nvoice.");
                Console.ResetColor();
                bool runDebitChoice = false;
                while (!runDebitChoice)
                {
                    Console.Write(" Your choice: ");
                    var payChoice = Console.ReadLine();
                    var orderHistory = (from o in db.OrderHistories
                                        where o.CheckoutCartOrderId == null
                                        select o);
                    if (payChoice == "d" || payChoice == "D")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter card number:");
                        Console.ResetColor();
                        var cardNumber = Console.ReadLine();
                        foreach (var o in orderHistory)
                        {
                            o.PaymentInfoId = 2;
                        }
                        db.SaveChanges();
                        runDebitChoice = true;
                    }
                    else if (payChoice == "i" || payChoice == "I")
                    {
                        Console.WriteLine("The users personal number has been used to file an invoice");
                        foreach (var o in orderHistory)
                        {
                            o.PaymentInfoId = 1;
                        }
                        db.SaveChanges();
                        runDebitChoice = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have to choose from options above!");
                        Console.ResetColor();
                    }
                }

                Random rnd = new Random();
                var randomOrderId = rnd.Next(100000, 999999).ToString() + loggedInId.ToString();

                var setOrderId = db.Carts.Where(p => p.CustomerId == loggedInId);

                foreach (var s in setOrderId)
                {
                    if (s.CustomerId == loggedInId && s.OrderId == null)
                    {
                        s.OrderId = float.Parse(randomOrderId);
                        foreach (var d in db.OrderHistories.Where(x => x.CheckoutCartOrderId == null))
                        {
                            d.CheckoutCartOrderId = s.OrderId;
                            d.CustomerId = loggedInId;
                        }
                    }
                }
                db.SaveChanges();

                var product = (from p in db.Products
                               join c in db.Carts on p.Id equals c.ProductId
                               where c.OrderId == float.Parse(randomOrderId)
                               select new { Stock = p.UnitsInStock, CartAmount = c.AmountofUnits, ProductId = p.Id, CartProductId = c.ProductId, AmountOfUnits = c.AmountofUnits });
                foreach (var p in db.Products)
                {
                    foreach (var pr in product)
                    {
                        if (p.Id == pr.CartProductId && pr.AmountOfUnits <= p.UnitsInStock)
                        {
                            p.UnitsInStock -= pr.CartAmount;
                        }
                        //else
                        //{
                        //    Console.WriteLine("Something went wrong, please try again.");
                        //    //ShowCart();
                        //}
                    }
                }

                ConfirmOrder(randomOrderId);
                db.SaveChanges();
            }
        }
        internal static void ConfirmOrder(string orderId)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n\nYour order has been placed, thank you for your purchase!" +
                $"\nYour order id is: {orderId}\n" +
                $"\r\n  _____ _                 _                                            _ \r\n |_   _| |__   __ _ _ __ | | __  _   _  ___  _   _      __ _ _ __   __| |\r\n   | | | '_ \\ / _` | '_ \\| |/ / | | | |/ _ \\| | | |    / _` | '_ \\ / _` |\r\n   | | | | | | (_| | | | |   <  | |_| | (_) | |_| |_  | (_| | | | | (_| |\r\n   |_| |_| |_|\\__,_|_| |_|_|\\_\\  \\__, |\\___/ \\__,_( )  \\__,_|_| |_|\\__,_|\r\n               _                 |___/            |/          _       _  \r\n __      _____| | ___ ___  _ __ ___   ___    __ _  __ _  __ _(_)_ __ | | \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\  / _` |/ _` |/ _` | | '_ \\| | \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | (_| | (_| | (_| | | | | |_| \r\n   \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__,_|\\__, |\\__,_|_|_| |_(_) \r\n                                                  |___/                  \r\n\r\n                     \t  .sssssssss.\r\n                    .sssssssssssssssssss\r\n                  sssssssssssssssssssssssss\r\n                ssssssssssssssssssssssssssss\r\n                 @@sssssssssssssssssssssss@ss\r\n                 |s@@@@sssssssssssssss@@@@s|s\r\n          _______|sssss@@@@@sssss@@@@@sssss|s\r\n        /         sssssssss@sssss@sssssssss|s\r\n       /  .------+.ssssssss@sssss@ssssssss.|\r\n      /  /       |...sssssss@sss@sssssss...|\r\n     |  |        |.......sss@sss@ssss......|\r\n     |  |        |..........s@ss@sss.......|\r\n     |  |        |...........@ss@..........|\r\n      \\  \\       |............ss@..........|\r\n       \\  '------+...........ss@...........|\r\n        \\________ .........................|\r\n                 |.........................|\r\n                /...........................\\\r\n               |.............................|\r\n                  |.......................|\r\n                      |...............|");
            Console.ResetColor();
            Console.ReadKey();
        }
        internal static void Categories()
        {
            bool runCategories = true;

            while (runCategories)
            {
                Console.Clear();
                PrintLogo();
                using (var db = new webshoppenContext())
                {
                    Console.WriteLine("[0] : Go back to start");
                    foreach (var p in db.Categories)
                    {
                        Console.WriteLine($"[{p.Id}] : {p.Name}");
                    }
                }

                Console.Write("Input the category-id you wish to view or [0] to go back: ");
                int categoryId = TryNumberInt();

                switch (categoryId)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        ShowAllOfCategory(categoryId);
                        ShowInfoOnProduct();
                        break;
                    case 0:
                        runCategories = false;
                        Console.Clear();
                        break;
                    default:
                        InputInstructions();
                        break;
                }

            }
            Console.Clear();
        }


        private static void ShowAllOfCategory(int categoryId)
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var c in db.Categories.Where(x => x.Id == categoryId))
                {
                    Console.WriteLine($"\nList of all {c.Name.ToString().ToLower()}. Enjoy!");
                }
                Console.ResetColor();

                foreach (var p in db.Products.Where(x => x.CategoryId == categoryId).Include(s => s.Supplier))
                {
                    var inStock = p.UnitsInStock > 0 ? "In stock." : "Out of stock.";
                    Console.WriteLine($" - [ID: {p.Id}] {p.Name}\t\tPrice: {p.Price} SEK\t\t{inStock} {p.UnitsInStock}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowInfoOnProduct()
        {
            bool runMenu = false;
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nChoose product-Id to see more. Or press [0] to go back: ");
                Console.ResetColor();
                int choosenNumber = Methods.TryNumberInt();
                if (choosenNumber == 0)
                {
                    runMenu = true;
                }
                using (var db = new webshoppenContext())
                {
                    foreach (var p in db.Products.Where(y => y.Id == choosenNumber).Include(x => x.Supplier).Include(c => c.Supplier.City))
                    {
                        Console.WriteLine($"{p.Name} - {p.Price} SEK - Available units: {p.UnitsInStock} - {p.InfoText} - Supplier: {p.Supplier.Name} - {p.Supplier.City.Name}");
                    }
                }
                if (choosenNumber != 0)
                {
                    CartChoice(choosenNumber);
                    runMenu = true;
                }

            }
        }
        internal static void CartChoice(int chosenNumber)
        {
            bool runMenu = false;
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nAdd to cart? Press [y]es or [n]o: ");
                Console.ResetColor();
                var choice = Console.ReadLine();
                if (choice == "y" || choice == "Y")
                {
                    AddProductToCart(chosenNumber);
                    runMenu = true;
                }
                else if (choice == "n" || choice == "N")
                {
                    runMenu = true;
                }
                else
                {
                    InputInstructions();
                }

            }
        }

        private static void AddProductToCart(int chosenNumber)
        {
            using (var db = new webshoppenContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nHow many do you want to add to your cart?: ");
                Console.ResetColor();
                int amountOfUnits = TryNumberInt();
                if (amountOfUnits == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You can not add 0 units to cart.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {

                    foreach (var product in db.Products.Where(p => p.Id == chosenNumber)) // loopar 2 gånger???
                    {
                        if (amountOfUnits < product.UnitsInStock)
                        {
                            var cart = new Cart
                            {
                                ProductId = chosenNumber,
                                AmountofUnits = amountOfUnits,
                                CustomerId = loggedInId
                            };
                            var cartList = db.Carts;
                            cartList.Add(cart);
                        }
                        else
                        {
                            Console.WriteLine("The product does not have that many units in stock, please try a smaller amount!");
                            AddProductToCart(chosenNumber);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        internal static double TryNumberDouble()
        {
            double number = 0;
            bool correctInput = false;

            while (!correctInput)
            {
                if (!double.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }

        internal static float TryNumberFloat()
        {
            float number = 0;
            bool correctInput = false;
            while (!correctInput)
            {
                if (!float.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }

        internal static int TryNumberInt()
        {
            int number = 0;
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }

        internal static long TryNumberLong()
        {
            long number = 0;
            bool correctInput = false;

            while (!correctInput)
            {
                if (!long.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }

        internal static string TryStringIn()
        {
            bool correctInput = false;
            string anyWord = null;

            while (!correctInput)
            {
                anyWord = Console.ReadLine();
                if (string.IsNullOrEmpty(anyWord))
                {
                    Console.WriteLine("Wrong input! Input can not be null.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return anyWord;
        }
        internal static void InputInstructions()
        {
            Console.WriteLine("Wrong input. Try something else!");
        }

        internal static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  \n");
            Console.ResetColor();
        }
    }
}


