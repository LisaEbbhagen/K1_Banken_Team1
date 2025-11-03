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
                    await Task.Delay(TimeSpan.FromMinutes(1));
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
                            _myBank.ExecuteTransaction("Deposit", null, 0, null);
                            _myBank.Pause();
                            break;

                    case "2":
                        _myBank.ExecuteTransaction("Withdraw", null, 0, null); 
                        _myBank.Pause();
                            break;                          

                    case "3":
                        _myBank.ExecuteTransaction("Transfer", null, 0, null);
                        _myBank.Pause();
                            break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("== Transaktioner ==\n");

                        _myBank.ProcessPendingTransactions(verbose: true); //Run pending transactions silently only runs if something runs

                        var accNo = userAccount.AccountNumber; // get current user account numer
                        var allTx = _myBank.GetAllTransactions()
                            .Where(t => t.AccountNumber == accNo)
                            .OrderBy(t => t.Timestamp)
                            .ToList();

                        Console.WriteLine(            //table headers
                            "Typ".PadRight(12) +
                            "Belopp".PadRight(12) +
                            "Från".PadRight(10) +
                            "Till".PadRight(10) +
                            "Saldo".PadRight(12) +
                            "Status".PadRight(12)
                        );
                        Console.WriteLine(new string('-', 70));

                        foreach (var tx in allTx)      //Print each transaction in one line
                        {
                            string toText = tx.Type == "Transfer" ? (tx.ToAccountNumber ?? "-") : "-";
                            string statusTxt = tx.Status == "Completed" ? "✅ Completed" : "⏳ Pending";

                            Console.WriteLine(
                                tx.Type.PadRight(12) +
                                ($"{tx.Amount} kr").PadRight(12) +
                                tx.AccountNumber.PadRight(10) +
                                toText.PadRight(10) +
                                tx.BalanceAfter.ToString().PadRight(12) +
                                statusTxt.PadRight(12)
                            );
                        }

                        Console.WriteLine();
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
