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
            Account userAccount = currentUser.Accounts.First(); // // simple version: get the first account of the user.

            
            _ = Task.Run(async () =>  // starts backgrounds activity that runs pending transactions every 15 minutes.
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    _myBank.ProcessPendingTransactions();
                }
            });

            while (loggedIn)
            {
                Console.Clear();
                ColorHelper.ShowMenuHeader("Meny");
                ColorHelper.ShowInfoMessage("Välj ett av följande alternativ:\n");

                ColorHelper.ShowMenuChoice("1. Sätta in pengar");
                ColorHelper.ShowMenuChoice("2. Ta ut pengar");
                ColorHelper.ShowMenuChoice("3. Överför pengar");
                ColorHelper.ShowMenuChoice("4. Visa transaktioner");
                ColorHelper.ShowMenuChoice("5. Visa alla mina konton och saldo");
                ColorHelper.ShowMenuChoice("6. Skapa nytt spar/checkkonto");
                ColorHelper.ShowMenuChoice("7. Ta ett banklån");
                ColorHelper.ShowMenuChoice("8. Logga ut");
                ColorHelper.ShowInputPrompt("\nVal: ");
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
                        _myBank.Transactions(currentUser);
                        break;

                    case "5": 
                        _myBank.ShowAllMyAccountsAndMoney(currentUser);
                        _myBank.Pause();
                        break;

                    case "6":
                        _myBank.AddNewAccount(currentUser);
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
                        ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
    }
}
