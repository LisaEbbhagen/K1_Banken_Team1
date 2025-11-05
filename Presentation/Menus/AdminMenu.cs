using K1_Banken_Team1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class AdminMenu
    {
        private readonly Bank myBank;
        public AdminMenu(Bank bank)
        {
            myBank = bank;
        }

        public void RunAdminMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                ColorHelper.ShowMenuHeader("Admin Meny");
                ColorHelper.ShowMenuChoice("1. Lista alla konton");
                ColorHelper.ShowMenuChoice("2. Visa konton med positivt saldo");
                ColorHelper.ShowMenuChoice("3. Visa de tre största transaktioner");
                ColorHelper.ShowMenuChoice("4. Visa total saldo per användare");
                ColorHelper.ShowMenuChoice("5. Visa största insättning & uttag per användare");
                ColorHelper.ShowMenuChoice("6. Visa användare med flest transaktioner");
                ColorHelper.ShowMenuChoice("7. Sök konto (kontonummer eller namn)");
                ColorHelper.ShowMenuChoice("8. Uppdatera växelkurser");
                ColorHelper.ShowMenuChoice("9. Lås upp användare");
                ColorHelper.ShowMenuChoice("10. Skapa ny användare");
                ColorHelper.ShowMenuChoice("11. Visa alla användare");
                ColorHelper.ShowMenuChoice("0. Logga ut");
                ColorHelper.ShowInputPrompt("\nVal: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        myBank.ListAllAccounts();
                        myBank.Pause();
                        break;

                    case "2":
                        myBank.PrintAccountsWithPositivBalance();
                        myBank.Pause();
                        break;

                    case "3":
                        myBank.ShowThreeBiggestTransactions();
                        myBank.Pause();
                        break;

                    case "4":
                        myBank.PrintTotalBalanceAll();
                        myBank.Pause();
                        break;

                    case "5":
                        myBank.ShowBiggestTransactionPerUser();
                        myBank.Pause();
                        break;

                    case "6":
                        myBank.ShowUserWithMostTransactions();
                        myBank.Pause();
                        break;

                    case "7":
                        myBank.SearchAccount();
                        myBank.Pause();
                        break;

                    case "8":
                        myBank.UpdateExchangeRates();
                        myBank.Pause();
                        break;

                    case "9":
                        myBank.UnLockUserMenu(); //method that unlocks user accounts
                        myBank.Pause();
                        break;

                    case "10":
                        myBank.CreateUser(); //Create new user
                        myBank.Pause();
                        break;

                    case "11":
                        myBank.ShowAllUsers(); //Show all users
                        myBank.Pause();

                        break;

                    case "0":
                        Console.WriteLine("Loggar ut från Admin...");
                        running = false;
                        break;

                    default:
                        ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
    }
}
