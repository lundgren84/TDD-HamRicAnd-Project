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
        public static void MainMenu()
        {
            var menuString =
          $@"
START MENU

1. Register New Customer
2. Rent Movie
3. Return Movie
4. Add Movie
5. Get Customer
6. EXIT
";
            Console.WriteLine(menuString);
        }

        internal static void ErrorInput()
        {          
                ConsoleWrite.Error("ERROR. wrong input.");         
        }
    }
}
