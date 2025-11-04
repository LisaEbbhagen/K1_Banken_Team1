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

                Console.WriteLine("\n=== Admin Meny ===");
                Console.WriteLine("1. Lista alla konton");
                Console.WriteLine("2. Visa konton med positivt saldo");
                Console.WriteLine("3. Visa de tre största transaktionerna");
                Console.WriteLine("4. Visa totalt saldo per användare");
                Console.WriteLine("5. Visa största insättning & uttag per användare");
                Console.WriteLine("6. Visa användare med flest transaktioner");
                Console.WriteLine("7. Sök konto (kontonummer eller namn)");
                Console.WriteLine("8. Uppdatera växelkurser");
                Console.WriteLine("9. Lås upp användare");
                Console.WriteLine("10. Skapa ny användare");
                Console.WriteLine("11. Visa alla användare");
                Console.WriteLine("0. Logga ut");
                Console.Write("Val: ");
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
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
    }
}
