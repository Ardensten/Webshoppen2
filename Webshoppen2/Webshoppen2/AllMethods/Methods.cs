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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  ");
                Console.ResetColor();
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

        public static void SignUp()
        {
            Console.WriteLine($"Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Social security number: ");
            long socialSecurityNumber = 0; socialSecurityNumber = TryNumberLong();
            Console.WriteLine("Phone number: ");
            int phoneNumber = 0; phoneNumber = TryNumberInt();
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("CityID: ");
            int cityId = 0; cityId = TryNumberInt();
            Console.WriteLine("Adress: ");
            string adress = Console.ReadLine();

            using (var db = new webshoppenContext())
            {
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

        public static void UserLogIn()  //Måste kolla så användaren finns!!
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
                }
            }
            StartPage(socialSecurityNumber);
        }

        public static void StartPage(long socialSecurityNumber)
        {
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ____            _                       _                  _    _ _                 _           _                  _   \r\n / ___| _   _ ___| |_ ___ _ __ ___  _   _| |___   _____  ___| | _| (_)_ __   __ _ ___| |__   ___ | | __ _  __ _  ___| |_ \r\n \\___ \\| | | / __| __/ _ \\ '_ ` _ \\| | | | __\\ \\ / / _ \\/ __| |/ / | | '_ \\ / _` / __| '_ \\ / _ \\| |/ _` |/ _` |/ _ \\ __|\r\n  ___) | |_| \\__ \\ ||  __/ | | | | | |_| | |_ \\ V /  __/ (__|   <| | | | | | (_| \\__ \\ |_) | (_) | | (_| | (_| |  __/ |_ \r\n |____/ \\__, |___/\\__\\___|_| |_| |_|\\__,_|\\__| \\_/ \\___|\\___|_|\\_\\_|_|_| |_|\\__, |___/_.__/ \\___/|_|\\__,_|\\__, |\\___|\\__|\r\n        |___/                                                               |___/                         |___/  ");
                Console.ResetColor();
                using (var db = new webshoppenContext())
                {
                    var customerName = db.Customers.Where(x => x.SocialSecurityNumber == socialSecurityNumber).Select(x => x.Name).FirstOrDefault();
                    Console.WriteLine($"\n\t\t  Welcome {customerName} to our beautiful webshop! Here can you buy spirits and get wasted!\n\n");
                    Console.WriteLine("\t\t\t\t\t\tRecommended products: ");


                    int i = 0;
                    foreach (var p in db.Products.Where(p => p.ChosenProduct == true))
                    {
                        Console.Write("\t\t\t\t \t\t\t  _\r\n \t\t\t\t\t\t\t {_}\r\n \t\t\t\t\t\t\t |(|\r\n\t\t\t\t\t\t\t |=|\r\n\t\t\t\t\t\t\t/   \\\t\t\t\t\t  [-] \r\n\t\t.~~~~.\t\t\t\t\t|.--| \t\t\t\t\t.-'-'-. \r\n\t\ti====i_\t\t\t\t\t||  |\t\t\t\t\t:-...-: \r\n\t\t|cccc|_)\t\t\t\t||  |\t\t\t\t\t|;:   | \r\n\t\t|cccc|   \t\t\t\t|'--|\t\t\t\t\t|;:.._|\r\n\t\t`-==-'\t\t\t\t\t'-=-'\t\t\t\t\t`-...-'");
                        Console.Write($"\n{p.Name}\tPrice: {p.Price}SEK \t\n");
                        i++;
                        if (i >= 3)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("\n\n\t\t1. Categories \t\t\t2. Search products \t\t\t3. Cart");

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
                    default:
                        InputInstructions();
                        break;
                }
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private static void Search()  //Dapper
        {
            string connstring = "Server=tcp:grupp3skola.database.windows.net,1433;Initial Catalog=webshoppen;Persist Security Info=False;User ID=grupp3admin;Password=NUskavikoda1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Console.WriteLine("\nType your search.");
            var search = Console.ReadLine();
            var sql = $"SELECT * FROM Product WHERE Name like '%{search}%'";
            var beers = new List<Models.Product>();

            using (var db = new SqlConnection(connstring))
            {
                beers = db.Query<Models.Product>(sql).ToList();
            }

            Console.WriteLine();
            foreach(var b in beers)
            {
                Console.WriteLine($"{b.Price}Sek, \t\t{b.Name}");
            }
            Console.ReadLine();
        }

        private static void ShowCart() //Lägg till en loop för för processen
        {
            using (var db = new webshoppenContext())
            {
                var cart = (from c in db.Carts
                            join p in db.Products on c.ProductId equals p.Id
                            join cu in db.Customers on c.CustomerId equals cu.Id
                            where cu.Id == loggedInId
                            select new { Name = p.Name, Price = p.Price, AmountofUnits = c.AmountofUnits, CartId = c.Id,  CartProductId = c.ProductId, CartCustomerId = c.CustomerId }).ToList();
               
                double? totalCostOfCart = 0;

                Console.WriteLine();
                foreach (var item in cart)
                {
                    Console.WriteLine($"{item.CartId} {item.Name} {item.Price} {item.AmountofUnits}");
                    double? totalPerCartId = Convert.ToDouble(item.AmountofUnits) * item.Price;
                    totalCostOfCart += totalPerCartId;                  
                }
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"\n\nTotal cost of cart: {totalCostOfCart}\n\n");
                Console.ResetColor();

                Console.WriteLine("Where do you want to go?\n[E] : Edit your cart \n[C] : Checkout\n[B] : Back to categories");
                var choice = Console.ReadLine();
                if (choice == "e")
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("[1] : Change amount of a product \n[2] : Remove a product from your cart");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("Which product do you want to change the amount of?");
                            int cartProductId = TryNumberInt();
                            Console.WriteLine("Enter the number you want in cart.");
                            int productAmount = TryNumberInt();

                            var product = db.Carts.Where(p => p.Id == cartProductId);
                            var changeCart = db.Carts.Where(p => p.CustomerId == loggedInId);

                            foreach (var c in changeCart)
                            {
                                if (cartProductId == c.Id && loggedInId == c.CustomerId)
                                {
                                    c.AmountofUnits = productAmount;
                                }

                            }
                            db.SaveChanges();

                            break;
                    }

                }

                else if (choice == "c")
                {
                    Checkout();
                }
                else if (choice == "b")
                {
                    Categories();
                }
                else
                {
                    InputInstructions();
                }

            }
        }

        public static void Checkout()
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
                            select new {Price = p.Price, AmountofUnits = c.AmountofUnits, CartId = c.Id, CartProductId = c.ProductId, CartCustomerId = c.CustomerId }).ToList();

                //var parcelChoise = (from s in db.ShippingInfos
                //                    join cu in db.Customers on s.CustomerId equals cu.Id
                //                    where cu.Id == loggedInId
                //                    select new { Customer = cu.Id, ParcelServiceName = s.ParcelServiceName });

                var checkoutCart = db.Carts.Where(p => p.CustomerId == loggedInId);

                Console.WriteLine("Choose shipping method: ");
                Console.WriteLine("[P]ostnord || [D]HL");
                var choice = Console.ReadLine();
                if (choice == "P" || choice == "p")
                {





                    var newParcel = new OrderHistory
                    {
                        ShippingInfoId = 1,
                        
                    };
                    var orderHistoryList = db.OrderHistories;
                    orderHistoryList.Add(newParcel);
                    db.SaveChanges();
                }
                else if (choice == "D" || choice == "d")
                {
                    var newParcel = new OrderHistory
                    {
                        ShippingInfoId = 2
                    };
                    var orderHistoryList = db.OrderHistories;
                    orderHistoryList.Add(newParcel);
                    db.SaveChanges();
                }

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
                    Console.WriteLine("[Q] : Go back to start");
                }
                Console.WriteLine("Input the category-id you wish to view: ");
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        ShowAllBeer();
                        Console.WriteLine("Choose a beer: ");
                        ShowInfoOnProduct();
                        break;
                    case '2':
                        ShowAllWine();
                        Console.WriteLine("Choose a wine: ");
                        ShowInfoOnProduct();
                        break;
                    case '3':
                        ShowAllSpirits();
                        Console.WriteLine("Choose spirits: ");
                        ShowInfoOnProduct();
                        break;
                    case '4':
                        ShowAllChampagne();
                        Console.WriteLine("Choose a champagne: ");
                        ShowInfoOnProduct();
                        break;
                    case '5':
                        ShowAllCider();
                        Console.WriteLine("Choose a cider: ");
                        ShowInfoOnProduct();
                        break;
                    case 'q':
                    case 'Q':
                        runCategories = false;
                        break;
                    default:
                        InputInstructions();
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ShowInfoOnProduct()
        {
            Console.WriteLine("Choose product-Id to see more.");
            int choosenNumber = 0; choosenNumber = Methods.TryNumberInt();
            using (var db = new webshoppenContext())
            {
                foreach (var p in db.Products.Where(y => y.Id == choosenNumber).Include(x => x.Supplier).Include(c => c.Supplier.City))
                {
                    Console.WriteLine($"{p.Name} - {p.Price} - {p.InfoText} - {p.Supplier.Name} - {p.Supplier.City.Name}");
                }
            }
            CartChoice(choosenNumber);
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

        internal static void CartChoice(int chosenNumber)
        {

            Console.WriteLine("Add to cart?");
            var choice = Console.ReadLine();
            if (choice == "y")
            {
                AddProductToCart(chosenNumber);
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

        private static void AddProductToCart(int chosenNumber) //Vi måste felsäkra så man inte kan lägga till mer än vad det finns tillgängligt i lagret
        {
            using (var db = new webshoppenContext())
            {
                Console.WriteLine("How many do you want to add to your cart?");
                int amountOfUnits = TryNumberInt();

                var product = db.Products.Where(p => p.Id == chosenNumber).ToList();
                foreach (var p in product)
                {
                    var cart = new Cart
                    {
                        ProductId = chosenNumber,
                        AmountofUnits = amountOfUnits,
                        CustomerId = loggedInId
                    };
                    var cartList = db.Carts;
                    cartList.Add(cart);
                    db.SaveChanges();
                }
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

        internal static void InputInstructions()
        {
            Console.WriteLine("Wrong input. Try something else!");
        }
    }
}
