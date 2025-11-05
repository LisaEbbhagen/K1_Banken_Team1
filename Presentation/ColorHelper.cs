using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation
{
    public class ColorHelper
    {
        public static void ShowTitle(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowMenuChoice(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowInputPrompt(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            Console.ResetColor();
        }

        public static void ShowColoredLogo(string logo)
        {
            foreach (char c in logo)
            {
                if (c == '$')
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.Write(c);
            }
            Console.ResetColor();
        }
    }
}
