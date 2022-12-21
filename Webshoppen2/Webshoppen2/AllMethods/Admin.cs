using System;
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
            Console.Clear();
            Console.WriteLine($"[1] Add a product."
                + "\n[2] Change a product."
                + "\n[3] Delete a product."
                + "\n[4] Add category."
                + "\n[5] Change customer-info"
                + "\n[6] View purchase histories");

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
                default:
                    Methods.InputInstructions();
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void ViewPurchaseHistory()
        {

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

        public static void RemoveProduct()
        {
            Console.WriteLine("Input id of product you want to delete");
            var productId = Convert.ToInt32(Console.ReadLine());

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
            Console.WriteLine("Input id of product you want to edit");
            var productId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("What do you want to edit?\n" +
                "[1] : Name\n" +
                "[2] : Price\n" +
                "[3] : Infotext\n" +
                "[4] : Units in Stock\n" +
                "[5] : Add product to front page\n" +
                "[6] : Remove product from front page\n" +
                "[7] : Return to main menu");

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
                        var newPrice = Convert.ToDouble(Console.ReadLine());
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
                        var newUnitsInStock = Convert.ToInt32(Console.ReadLine());
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
                        Methods.Running();
                        break;

                    default:
                        Methods.InputInstructions();
                        break;
                }
            }
        }
    }
}
