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

            // starta bakgrundsjobb som kör pending var 15:e minut
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(15));
                    _myBank.ProcessPendingTransactions();
                }
            });


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
                            _myBank.DepositMoney(currentUser);
                            _myBank.Pause();
                            break;

                    case "2":
                            _myBank.WithdrawMoney(currentUser);
                            _myBank.Pause();
                            break;                          

                    case "3": 
                            _myBank.TransferMoney(currentUser);
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
                            _myBank.ShowAllMyAccountsAndMoney(currentUser);
                            _myBank.Pause();
                            break;

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
