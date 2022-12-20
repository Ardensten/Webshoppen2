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
                Console.WriteLine("SYSTEMUTVECKLINGSBOLAGET");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Välkommen till vår vackra webshop! Här kan du köpa sprit.");
                Console.WriteLine();
                Console.WriteLine("Top tre produkter: ");
                Console.WriteLine();

                Console.WriteLine("1. Fritextsök");
                Console.WriteLine("2. Kategorier");
                Console.WriteLine("3. Admin");

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
                Console.WriteLine("1. Öl");
                Console.WriteLine("2. Vin");
                Console.WriteLine("3. Sprit");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        //ShowAllBeer();
                        Console.WriteLine("Välj en öl: ");
                        CartChoice();
                        break;
                    case '2':
                        //ShowAllWine();
                        Console.WriteLine("Välj ett vin: ");
                        CartChoice();
                        break;
                    case '3':
                        //ShowAllSpirits();
                        Console.WriteLine("Välj sprit: ");
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

        internal static void CartChoice()
        {

            Console.WriteLine("Lägg till i varukorg?");
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
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Ändra produkt");
            Console.WriteLine("3. Ta bort produkt");

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
            Console.WriteLine("Ange produktens namn: ");
            string name = Console.ReadLine();
            Console.WriteLine("Ange produktens pris: ");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ange produktens leverantörs-nummer: ");
            int supplierId = Convert.ToInt32(Console.ReadLine());
            //ShowListSupplier();

            Console.WriteLine("Ange produktens kategori-id: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            //ShowListCategory();

            Console.WriteLine("Skriv en info-text om produkten: ");
            var infotext = Console.ReadLine();
            Console.WriteLine("Ange antal produkter som finns på lager: ");
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
            Console.WriteLine("Felaktig inmatning.");
        }
    }
}
