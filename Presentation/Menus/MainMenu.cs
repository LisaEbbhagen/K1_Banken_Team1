using K1_Banken_Team1.Domain;
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
                ColorHelper.ShowMenuHeader("Välkommen till K1 Banken");
                ColorHelper.ShowMenuChoice("1. Logga in");
                ColorHelper.ShowMenuChoice("2. Admin");
                ColorHelper.ShowMenuChoice("3. Avsluta");

                ColorHelper.ShowInputPrompt("\nVal: ");
                string startChoice = Console.ReadLine();

                switch (startChoice)
                {
                    case "1":
                        return "UserMenu";    // Return the value, do not run the UserMenu here, and return the value.
                    case "2":
                        return "AdminMenu";  // Return the value.
                    case "3":
                        return "Exit";
                    default:
                        ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                        myBank.Pause();
                        return "MainMenu";
                }
            }
        }
    }
}


