using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class AdminMenu
    {
        public void RunAdminMenu()
        {
            bool running = true;

            while (running)
            {

                Console.WriteLine("\n=== Admin Meny ===");
                Console.WriteLine("1. Lista alla konton");
                Console.WriteLine("2. Visa konton med positivt saldo");
                Console.WriteLine("3. Visa de tre största transaktioner");
                Console.WriteLine("4. Visa total saldo per användare");
                Console.WriteLine("5. Visa största insättning & uttag per användare");
                Console.WriteLine("6. Visa användare med flest transaktioner");
                Console.WriteLine("7. Sök konto (kontonummer eller namn)");
                Console.WriteLine("8. Uppdatera växelkurser");
                Console.WriteLine("8. Lås upp användare");
                Console.WriteLine("9. Logga ut");
                Console.Write("Val: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Alla konton:");
                        foreach (var acc in accounts.Values)
                        {
                            Console.WriteLine($"Konto: {acc.AccountNumber}, Ägare: {acc.Owner.Name}, Saldo: {acc.Balance} SEK");
                        }
                        break;

                    case "2":
                        PrintAccountsWithPositivBalance();
                        break;

                    case "3":
                        var topThree = threeBiggestAmount();
                        Console.WriteLine("De tre största transaktionerna:");
                        foreach (var t in topThree)
                        {
                            Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount} kr – Konto: {t.AccountNumber}"); //*fixa till utskriften, svenska o engelska blandas
                        }
                        break;

                    case "4":
                        PrintTotalBalanceAll();
                        break;

                    case "5":
                        ShowBiggestTransactionPerUser();
                        break;

                    case "6":
                        ShowUserWithMostTransactions();
                        break;

                    case "7":
                        SearchAccount();
                        break;

                    case "8":
                        UpdateExchangeRates();
                        UnLockUserMenu(); //metod som låser upp användare
                        Pause();
                        break;

                    case "9":
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
