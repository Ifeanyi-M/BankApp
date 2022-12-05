using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.UI
{
    public static class Service
    {
        private static long tranId;
        private static CultureInfo culture = new CultureInfo("en-GB");
         public static long GetTransactionId()
        {
            return ++tranId;  
        }
        public static string SecretInput(string prompt)
        {
            bool isPrompt = true;
            string asterisks = "";

            StringBuilder input = new StringBuilder();

            while (true)
            {
                if(isPrompt)
                Console.WriteLine(prompt);
                isPrompt = false;

                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if(inputKey.Key == ConsoleKey.Enter)
                {
                    if(input.Length == 6)
                    {
                        break;
                    }
                    else 
                    { 
                        PrintMessage("\nPlease enter 6 digits", false);
                        isPrompt = true;
                        input.Clear();
                        continue;

                    }
                }
                if(inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterisks + "*");
                      
                }

               
            }
            return input.ToString();
        }
        public static void PrintMessage(string message, bool success = true)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else 
            { 
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            HitEnterKey();
        }
        public static string GetUserDeets(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }

        public static void PrintDotAnime(int timer = 10)
        {
         
            for (int i = 0; i < timer; i++)
            {
                Console.Write("->");
                Thread.Sleep(200);
            }
            Console.Clear();
        }
        public static void HitEnterKey()
        {
            Console.WriteLine("\n\n Hit Enter to proceed...\n");
            Console.ReadLine();
        }
        public static string CurrencyForm(decimal amt)
        {
            return String.Format(culture, "{0:C2}", amt);
        }
    }
}
