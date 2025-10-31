using K1_Banken_Team1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace K1_Banken_Team1.Presentation.Menus
{
    public class MainMenu
    {
        private readonly Bank myBank;

        public MainMenu(Bank bank)
        {
            myBank = bank;
        }

        public string RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Välkommen till K1 Banken ===");
                Console.WriteLine("1. Logga in");
                Console.WriteLine("2. Skapa konto (Ej implementerat)");
                Console.WriteLine("3. Admin");
                Console.WriteLine("4. Avsluta");

                Console.Write("Val: ");
                string startChoice = Console.ReadLine();

                switch (startChoice)
                {
                    case "1":
                        return "UserMenu";    // returnera värdet, kör inte UserMenu här
                    case "2":
                        return "CreateAccount";
                    case "3":
                        return "AdminMenu";  // returnera värdet
                    case "4":
                        return "Exit";
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        myBank.Pause();
                        return "MainMenu";
                }
            }
        }
    }
}


