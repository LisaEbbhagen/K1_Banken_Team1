using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation
{
    public class ColorHelper
    {
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

        

        public static void ShowInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowMenuChoice(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowHighlightedChoice(string message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"> {message}");
            Console.ResetColor();
        }

        public static void ShowInputPrompt(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(message);
            Console.ResetColor();
        }

        public static void ShowMenuHeader(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"\n=== {message.ToUpper()} ===\n");
            Console.ResetColor();
        }

        //For MAINMENU
        public static void ShowTitle(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            int windowWidth = Console.WindowWidth;
            string title = $"=== {message.ToUpper()} ===";
            int padding = Math.Max((windowWidth - title.Length) / 2, 0);

            Console.WriteLine(new string(' ', windowWidth));
            Console.WriteLine(new string(' ', padding) + title);
            Console.ResetColor();
        }

        public static void ShowErrorMessageFirstMenu(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(new string(' ', 55) + message);
            Console.ResetColor();
        }

        public static void ShowInputPromptFirstMenu(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(new string(' ', 55) + message);
            Console.ResetColor();
        }

        public static void ShowWarningMessageFirstMenu(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string(' ', 55) + message);
            Console.ResetColor();
        }

        public static void ShowSuccessMessageFirstMenu(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string(' ', 55) + message);
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
