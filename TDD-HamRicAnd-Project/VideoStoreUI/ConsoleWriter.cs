using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreUI
{
   public static class ConsoleWrite
    {
        public static void Success(string input)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(string input)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Heading(string input)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void MainMenu()
        {
            var menuString =
          $@"
1. Register New Customer
2. Get Customer
3. Get Customers
";
            var menuString2 =
$@"   
4. Rent Movie
5. Return Movie
6. Add Movie
7. Get Movies

8. EXIT
";
            ConsoleWrite.Heading("START MENU");
            Console.WriteLine(Environment.NewLine);
            ConsoleWrite.Heading("Customer Section");
            Console.WriteLine(menuString);
            ConsoleWrite.Heading("Movie Section");
            Console.WriteLine(menuString2);
        }

        internal static void ErrorInput()
        {          
                ConsoleWrite.Error("ERROR. wrong input.");         
        }
    }
}
