using K1_Banken_Team1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class UserMenu
    {
        private readonly Bank _myBank;

        public UserMenu(Bank myBank)
        {
            _myBank = myBank;
        }

        public void RunUserMenu(User currentUser)
        {
            bool loggedIn = true;
            Account userAccount = currentUser.Accounts.First(); // enkel version: varje användare har ett konto

            while (loggedIn)
            {

                Console.WriteLine("\n=== Meny ===");
                Console.WriteLine("\nVälkommen till K1 Banken!\n");
                Console.WriteLine("Välj ett av följande alternativ!");

                Console.WriteLine("1. Sätta in pengar");
                Console.WriteLine("2. Ta ut pengar");
                Console.WriteLine("3. Överför pengar");
                Console.WriteLine("4. Visa transaktioner");
                Console.WriteLine("5. Visa alla mina konton och saldo");
                Console.WriteLine("6. Skapa nytt sparkonto");
                Console.WriteLine("7. Ta ett banklån");
                Console.WriteLine("8. Logga ut");
                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "1":
                        {
                            _myBank.DepositMoney(currentUser);
                            _myBank.Pause();
                            break;
                        }

                    case "2":
                        {
                            _myBank.WithdrawMoney(currentUser);
                            _myBank.Pause();
                            break;                          
                        }

                    case "3": //Överför pengar
                        Console.Write("Vilket konto vill du överföra pengar från? ");
                        string fromAccNo = Console.ReadLine();
                        var fromAccNumber = _myBank.FindAccount(fromAccNo, currentUser);

                        Console.Write("Vilket konto vill du överföra pengar till? ");
                        string toAccNo = Console.ReadLine();
                        var toAccNumber = _myBank.FindAccount(toAccNo);

                        if (fromAccNumber == null)
                        {
                            Console.WriteLine("❌ Kontot du vill överföra pengar från hittades inte.");
                            break;
                        }

                        if (toAccNumber == null)
                        {
                            Console.WriteLine("❌ Kontot du vill överföra pengar till hittades inte.");
                            break;
                        }

                        Console.Write("Vilket belopp vill du överföra? ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount) || transferAmount <= 0)
                        {
                            Console.WriteLine("\nOgiltigt belopp!");
                            break;
                        }

                        else
                        {
                            _myBank.ExecuteTransaction("Transfer", fromAccNo, transferAmount, toAccNo);
                        }
                        _myBank.Pause();
                        break;

                    case "4": //Visa transaktioner
                        Console.Write("Kontonummer: ");
                        string accNo = Console.ReadLine();
                        var accNumber = _myBank.FindAccount(accNo, currentUser);

                        if (accNumber == null)
                        {
                            Console.WriteLine("❌ Kontot hittades inte.");
                            _myBank.Pause();
                            break;
                        }
                        else
                        {
                            _myBank.LatestTransactions(accNo); //Kontonumret skickas till metoden 
                        }
                        _myBank.Pause();
                        break;

                    case "5":
                        {
                            //visa alla konton + saldo
                            var accounts = _myBank.ListAccounts(currentUser);

                            //Om inga konto finns
                            if (accounts == null || !accounts.Any())
                            {
                                Console.WriteLine("ℹ️Du har inga konton.");
                                _myBank.Pause();
                                break;
                            }
                            Console.WriteLine("\nDina konton och saldo:");
                            Console.WriteLine("--------------------");

                            //Rubriker med justering
                            Console.WriteLine($"{"Namn",-10} | {"Konto",-10} | {"Saldo",10}");

                            foreach (var acc in accounts)
                            {
                                Console.WriteLine($"{currentUser.Name,-10} | {acc.AccountNumber,-10} | {acc.Balance,10:0} kr");
                            }
                            _myBank.Pause();
                            break;
                        }

                    case "6":
                        _myBank.AddNewSavingsAccount(currentUser);
                        _myBank.Pause();
                        break;

                    case "7":
                        _myBank.LoanMoney(currentUser);
                        _myBank.Pause();
                        break;

                    case "8":
                        Console.WriteLine($"🔒Du loggas nu ut, {currentUser.Name}...");
                        loggedIn = false;
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
    }
}
