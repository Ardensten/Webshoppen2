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

                        break;

                }

               Console.ReadKey(true);
            }
        }



        internal static void Categories()
        {
            Console.Clear();
            bool runCategories = true;

            while(runCategories) 
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
                        //
                        break;
                }

                Console.ReadKey();
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
                Console.WriteLine("Felaktig inmatning.");
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
                    //Add product
                    break;
                case '2':
                    //Edit product
                    break;
                case '3':
                    //Remove product
                    break;
                default:
                    //
                    break;

            }

            Console.ReadKey();
        }
    }
}
