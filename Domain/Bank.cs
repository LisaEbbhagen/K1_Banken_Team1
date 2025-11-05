using K1_Banken_Team1.Presentation;
using System.Collections.Generic;
﻿using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace K1_Banken_Team1.Domain
{
    public class Bank
    {
        public Dictionary<string, decimal> ExchangeRates { get; private set; } = new Dictionary<string, decimal>
        {
            { "SEK", 1m },
            { "EUR", 10.92m },
            { "USD", 9.40m }
        };

        public List<User> users { get; private set; } = new List<User>(); //List for users.


        public void AddUser(User user)
        {
            if (!users.Any(u => u.Id == user.Id))
            {
                users.Add(user);
                ColorHelper.ShowSuccessMessage($"Användare {user.Name} med ID {user.Id} har lagts till.");
            }
            else
            {
                ColorHelper.ShowWarningMessage("Användaren finns redan.");
            }
        }

        public Dictionary<string, Account> accounts = new Dictionary<string, Account>(); //Dictionary for accounts.

        public List<Account> AccountsWithPositivBalance() //LINQ for accounts with positve balance.
        {
            return accounts.Values.Where(accounts => accounts.Balance > 0).ToList();
        }

        public void CreateUser()
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("🧑‍💻 Skapa ny användare\n");

            string name;
            while (true)
            {
                ColorHelper.ShowInputPrompt("Ange namn: ");
                name = Console.ReadLine();

                if (users.Any(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase))) //Name Validataion  (if existed)
                {
                    ColorHelper.ShowWarningMessage($"❌ Det finns redan en användare registrerad med namnet '{name}'. Välj ett annat namn.\n");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    ColorHelper.ShowWarningMessage("❌ Namn får inte vara tomt.\n");
                    continue;
                }
                break; // name is Ok, next step
            }

            string pin;
            while (true)
            {
                ColorHelper.ShowInputPrompt("Ange PIN (4 siffror): ");
                pin = Console.ReadLine();

                if (pin.Length == 4 && pin.All(char.IsDigit))
                {
                    break; // PIN ok, next step
                }
                ColorHelper.ShowWarningMessage("❌ Ogiltig PIN. Ange exakt 4 siffror.\n");

            }

            string id;
            while(true)
            {
                ColorHelper.ShowInputPrompt("Ange ID");
                id = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(id))
                {
                    ColorHelper.ShowWarningMessage("❌ ID får inte vara tomt.\n ");
                    continue;
                }

                if (users.Any(u => u.Id.Equals(id, StringComparison.OrdinalIgnoreCase))) //if Id is used
                {
                    ColorHelper.ShowWarningMessage($"❌ ID '{id}' andvänds redan. välj ett annat ID.");
                    continue;
                }
                break; //Id ok
            }

            bool isAdmin = false; // Future use for admin creation.
            while (true)
            {
               ColorHelper.ShowInputPrompt("Ska denna användare vara admin? (j/n): ");
               string adminInput = Console.ReadLine().ToLower();
                if (adminInput == "j")
                {
                   isAdmin = true;
                   break;
                }
                else if (adminInput == "n")
                {
                   isAdmin = false;
                   break;
                }
                else
                {
                        ColorHelper.ShowWarningMessage("Ogiltigt val. Ange 'j' för ja eller 'n' för nej.\n");
                }
                
            }

            var newUser = new User(name, pin, id, isAdmin); //Create user.
            AddUser(newUser);                     // Add user to list.

            ColorHelper.ShowSuccessMessage($"✅ Användaren'{name}' har skapats och lagts till i systemet!");
        }

        public void ListAllAccounts() //List all accounts, owner, balance that are registered.
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("Alla konton i banken: \n");
            Console.WriteLine(new string('-', 50)); 
            Console.WriteLine($"{"Ägare:",-15}  {"Kontonummer:",-10} {"Saldo:",16}");
            Console.WriteLine(new string('-', 50));

            foreach (var acc in accounts.Values)
            {
                Console.WriteLine($"{acc.Owner.Name,-15} | {acc.AccountNumber,-10} | {acc.Balance,15:C}");
            }
            Console.WriteLine(new string('-', 50));
        }

        public void ShowAllUsers()
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("👥 Alla användare i systemet: \n");
            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"{"Användarnamn:",-15}  {"Id:",-10}");
            Console.WriteLine(new string('-', 30));

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name,-15} | {user.Id,-10}");
            }
            Console.WriteLine(new string('-', 30));
        }

        public void PrintAccountsWithPositivBalance() //Method for printing accounts with positve balance.
        {
            Console.Clear();
            var positivAccounts = AccountsWithPositivBalance();
            if (positivAccounts.Count == 0)
            {
                ColorHelper.ShowWarningMessage("Inga konton med positivt saldo hittades.");
                return;
            }

            ColorHelper.ShowHighlightedChoice("Konton med positivt saldo:\n");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Ägare:",-15}  {"Kontonummer:",-10} {"Saldo:",16}");
            Console.WriteLine(new string('-', 50));

            foreach (var acc in positivAccounts)
            {
                Console.WriteLine($"{acc.Owner.Name,-15} | {acc.AccountNumber,-10} | {acc.Balance,15:C}");
            }
            Console.WriteLine(new string('-', 50));
        }

        public List<Transaction> transactions { get; private set; } = new List<Transaction>(); //List for transactions.

        public bool EnsureUserHasAccount(User user)
        {
            // User has no accounts
            if (!user.Accounts.Any())
            {
                ColorHelper.ShowWarningMessage("❗ Du har inget konto kopplat till ditt användarkonto.");
                ColorHelper.ShowInfoMessage("Du behöver skapa ett konto för att fortsätta.");

                Pause();
                return false;
            }
            return true;
        }

        public void OpenAccount(User user, string accountNumber, string accountType = null, string currency = null) //Method to open new account.
        {
            Console.Clear();
            if (accounts.ContainsKey(accountNumber)) //Check if account number already exists
            {
                ColorHelper.ShowWarningMessage("Kontonumret finns redan.");
                return;
            }

            while (true)
            {
                if (string.IsNullOrWhiteSpace(accountType))
                {
                    Console.WriteLine("Vilken typ av konto vill du skapa?");
                    Console.WriteLine("1. Sparkonto");
                    Console.WriteLine("2. Checkkonto");
                    ColorHelper.ShowInputPrompt("Val: ");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                        accountType = "spar";

                    else if (choice == "2")
                        accountType = "checking";

                    else
                    {
                        ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                        continue;
                    }
                }
                break;
            }

            while (true)
            {
                if (string.IsNullOrWhiteSpace(currency))
                {
                    ColorHelper.ShowInputPrompt("Vilken valuta vill du ha på kontot? (SEK, EUR, USD): ");
                    currency = Console.ReadLine()?.ToUpper();
                }

                if (currency == "SEK" || currency == "EUR" || currency == "USD")
                    break;

                ColorHelper.ShowWarningMessage("Ogiltig valuta, försök igen.");
                currency = null;
            }

            Account newAccount;

            switch (accountType.ToLower())
            {
                case "spar":
                case "sparkonto":
                    newAccount = new SavingAccount(accountNumber, user);
                    break;
                case "checking":
                case "checkkonto":
                    newAccount = new CheckingAccount(accountNumber, user);
                    break;
                default:
                    ColorHelper.ShowWarningMessage("Ogiltig kontotyp. Skapar sparkonto som standard.");
                    newAccount = new SavingAccount(accountNumber, user);
                    break;
            }

            newAccount.Currency = currency;

            accounts.Add(accountNumber, newAccount);
            user.AddAccount(newAccount);

            ColorHelper.ShowSuccessMessage($"Nytt {accountType}-konto öppnat med kontonummer: {accountNumber} ({currency}).");
        }

        public void UnLockUserMenu()
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("=== Lås upp användare === \n");

            var lockedUsers = users.Where(u => u.IsLocked).ToList(); //List all locked users

            if (!lockedUsers.Any()) //if there is no locked users
            {
                ColorHelper.ShowWarningMessage("\nℹ️ Det finns inga låsta användare just nu.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Låsta användare:");
                foreach (var u in lockedUsers)
                {
                    Console.WriteLine($"-{u.Name}");
                }

                ColorHelper.ShowInputPrompt("\nAnge namnet på användaren du vill låsa upp:");
                string name = Console.ReadLine();

                var userToUnlock = lockedUsers //find userUser
                    .FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (userToUnlock == null) //if User is not existed
                {
                    ColorHelper.ShowWarningMessage("❌ Ingen användare hittades med det namnet. Ange namn igen. \n");
                    continue;
                }

                userToUnlock.IsLocked = false; //if User is locked

                ColorHelper.ShowSuccessMessage($"🔒 Kontot för {userToUnlock.Name} har låsts upp!");
                return;
            }
        }

        // Method for finding an account. 'currentUser' was added so the code can be adjusted depending on whether it's a user or an admin.
        // Usage: if currentUser is passed in the program will only search that user's own accounts. If the parameter is not provided, any account can be chosen (e.g. admin).
        public Account FindAccount(string accountNumber, User user = null)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                ColorHelper.ShowWarningMessage("Kontonumret får inte vara tomt.");
                return null;
            }

            if (user != null)
            {
                return user.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            }

            if (accounts.TryGetValue(accountNumber, out Account account))
            {
                return account;
            }

            else
            {
                ColorHelper.ShowWarningMessage("Konto finns ej.");
                return null;
            }
        }

        //All transactiontypes in one method:
        public bool ExecuteTransaction(string type, User currentUser)
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice($"\n-- {type.ToUpper()} --");

            
            ColorHelper.ShowInputPrompt("Ange DITT kontonummer: ");
            string accountNumber = Console.ReadLine();  // Sender account - Should be inlogged user
            var fromAcc = FindAccount(accountNumber);
             
            while (fromAcc == null || fromAcc.Owner != currentUser)
            {
                ColorHelper.ShowWarningMessage($"❌ Konto {accountNumber} hittades inte. Försök igen.");
                ColorHelper.ShowInputPrompt("Ange ditt kontonummer: ");
                accountNumber = Console.ReadLine();
                fromAcc = FindAccount(accountNumber);
            }


            string toAccountNumber = null;
            Account toAcc = null;   

            if (type == "Transfer")  //when transfer - ask for reciever account validation
            {
                ColorHelper.ShowInputPrompt("Ange mottagarkonto: ");
                toAccountNumber = Console.ReadLine();
                toAcc = FindAccount(toAccountNumber);

                //not null or your own account
                while (toAcc == null || toAccountNumber == accountNumber) //not null or your own logged in account
                {
                    if (toAcc == null)
                    {
                        ColorHelper.ShowWarningMessage($"❌ Mottagarkonto {toAccountNumber} hittades inte. Försök igen.");
                    }
                    else
                    {
                        ColorHelper.ShowWarningMessage($"❌ Du kan inte överföra till detta konto. Försök igen.");
                    }
                    ColorHelper.ShowInputPrompt("Ange mottagarkonto: ");
                    toAccountNumber = Console.ReadLine();
                    toAcc = FindAccount(toAccountNumber);
                }
            }

            if ((type == "Withdraw" || type == "Transfer") && fromAcc.Balance <= 0) //block transaction if balance is 0.for withdraw/transfer
            {
                ColorHelper.ShowWarningMessage($"✖ Ditt saldo är {fromAcc.Balance :0} kr. Du kan inte göra {type.ToLower()}.");
                Pause();
                return false;
            }

            decimal amount;
            while (true)
            {
                ColorHelper.ShowInputPrompt("Ange belopp: ");
                string input = Console.ReadLine();

               
                if (!decimal.TryParse(input, out amount) || amount <= 0)  // amount must be a number and > 0
                {
                    ColorHelper.ShowWarningMessage("✖ Ogiltigt belopp. Ange en giltig siffra större än 0.");
                    continue;
                }
               
                if ((type == "Withdraw" || type == "Transfer") && amount > fromAcc.Balance)  // check balance for withdraw/transfer
                {
                    ColorHelper.ShowWarningMessage($"✖ Otillräckligt saldo (Ditt saldo = {fromAcc.Balance} kr). Ange ett lägre belopp.");
                    continue;
                }

                break; //amount confirmed
            }


            var tx = new Transaction(            //create and add transaction to queue
                Guid.NewGuid().ToString(),
                accountNumber,
                amount,
                DateTime.Now,
                type,
                toAccountNumber
            )
            {
                Status = "Pending",              // all transactions start as pending
                BalanceAfter = fromAcc.Balance   
            };

            transactions.Add(tx);

            
            if (type == "Transfer")                  //confirmation message in console
                ColorHelper.ShowSuccessMessage($"🕒 Överföring på {amount} kr från {accountNumber} till {toAccountNumber} är registrerad och körs om 15 minuter.");
            else if (type == "Deposit")
                ColorHelper.ShowSuccessMessage($"🕒 Insättning på {amount} kr är registrerad och körs om 15 minuter.");
            else if (type == "Withdraw")
                ColorHelper.ShowSuccessMessage($"🕒 Uttag på {amount} kr är registrerat och körs om 15 minuter.");

            return true;
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;    
        }

        public void ProcessPendingTransactions(bool verbose = false) //Run older transactions than 15 min
        {
            var ready = transactions     //find transactions ready to be processed
                .Where(t => t.Status == "Pending" &&
                            DateTime.Now - t.Timestamp >= TimeSpan.FromSeconds(3))
                .ToList();

            if (ready.Count == 0) return; // Stop if no transaction are ready

            foreach (var tx in ready)
            {
                var fromAcc = FindAccount(tx.AccountNumber); 
                var toAcc = tx.Type == "Transfer" ? FindAccount(tx.ToAccountNumber) : null;

                switch (tx.Type)
                {
                    case "Deposit":
                        fromAcc.Deposit(tx.Amount); //add money to account
                        tx.Status = "Completed";
                        tx.BalanceAfter = fromAcc.Balance;
                        if (verbose)
                            ColorHelper.ShowSuccessMessage($"✅ Insättning: +{tx.Amount} kr till {fromAcc.AccountNumber}. Nytt saldo: {tx.BalanceAfter} kr.");
                        break;

                    case "Withdraw":
                        fromAcc.Withdraw(tx.Amount); //withdraw money from account
                        tx.Status = "Completed";
                        tx.BalanceAfter = fromAcc.Balance;
                        if (verbose)
                            ColorHelper.ShowSuccessMessage($"✅ Uttag: -{tx.Amount} kr från {fromAcc.AccountNumber}. Nytt saldo: {tx.BalanceAfter} kr.");
                        break;

                    case "Transfer":
                        fromAcc.Withdraw(tx.Amount); //transfer money between two accounts
                        toAcc.Deposit(tx.Amount);
                        tx.Status = "Completed";
                        tx.BalanceAfter = fromAcc.Balance; // Balance after processed transaction
                        if (verbose)
                            ColorHelper.ShowSuccessMessage($"✅ Överföring: -{tx.Amount} kr från {fromAcc.AccountNumber} till {toAcc.AccountNumber}. Avsändarens saldo: {tx.BalanceAfter} kr.");
                        break;
                }
            }
        }

        public void Transactions(User currentUser)
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("== Transaktioner ==\n");

            
            ProcessPendingTransactions(verbose: true); //Run pending transactions silently aftr 15 min

            var accNo = currentUser.Accounts.First().AccountNumber; //get currentuser account number
            var allTx = GetAllTransactions()
                .Where(t => t.AccountNumber == accNo)
                .OrderBy(t => t.Timestamp)
                .ToList();

            // Tabellhuvud
            Console.WriteLine("Typ".PadRight(12) +                //Table headers
                              "Belopp".PadRight(12) +
                              "Från".PadRight(10) +
                              "Till".PadRight(10) +
                              "Saldo".PadRight(12) +
                              "Status".PadRight(12));
            Console.WriteLine(new string('-', 70));

            
            foreach (var tx in allTx)                 //show each transaction in one line
            {
                string toText = tx.Type == "Transfer" ? tx.ToAccountNumber ?? "-" : "-";
                string status = tx.Status == "Completed" ? "✅ Completed" : "⏳ Pending";

                Console.WriteLine(tx.Type.PadRight(12) +
                                  $"{tx.Amount} kr".PadRight(12) +
                                  tx.AccountNumber.PadRight(10) +
                                  toText.PadRight(10) +
                                  tx.BalanceAfter.ToString().PadRight(12) +
                                  status.PadRight(12));
            }

            Console.WriteLine();
            Pause();
        }



        public void ShowBalance()
        {
            foreach (var user in users)
            {
                decimal total = user.Accounts.Sum(a => a.Balance);
                Console.WriteLine($"{user.Name} {user.Id} - {total} kr");
            }
        }

        public List<Transaction> threeBiggestAmount()
        {
            return transactions //returnera värden med följande tre metoder i beaktning
            .OrderByDescending(t => t.Amount) //sorterar listan i fallande ordning (Lambda)
            .Take(3)
            .ToList(); //returnerar resultatet till en vanlig lista
        }

        public void ShowThreeBiggestTransactions()
        {
            Console.Clear();
            var topThree = threeBiggestAmount();
            ColorHelper.ShowHighlightedChoice("De tre största transaktionerna:\n");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"{"Tidstämpel:",-20}  {"Kontonummer:",-10} {"Typ:",-15} {"Summa:",15}");
            Console.WriteLine(new string('-', 70));

            foreach (var t in topThree)
            {
                Console.WriteLine($"{t.Timestamp,-20} | {t.AccountNumber,-10} | {t.Type,-15:C} | {t.Amount,15:C}");
            }
            Console.WriteLine(new string('-', 70));
        }


        public List<Transaction> LatestTransactions(string accountNumber)
        {
            Console.WriteLine("Vilken typ av transaktioner vill du se?");
            Console.WriteLine("1. Endast insättningar");
            Console.WriteLine("2. Endast uttag");
            Console.WriteLine("3. Endast överföringar");
            Console.WriteLine("4. Alla transaktioner");
            Console.WriteLine("5. Gå tillbaka till föregående meny");
            ColorHelper.ShowInputPrompt("Val: ");
            string choice = Console.ReadLine();
            
            IEnumerable<Transaction> filtered = Enumerable.Empty<Transaction>(); //Skapar en tom sekvens av Transaction-objekt, en tom lista som du kan fylla senare. 

            switch (choice)
            {
                case "1":
                    ColorHelper.ShowHighlightedChoice("Senaste insättningarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Deposit" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "2":
                    ColorHelper.ShowHighlightedChoice("Senaste uttagen:");
                    filtered = transactions
                        .Where(t => t.Type == "Withdraw" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "3":
                    ColorHelper.ShowHighlightedChoice("Senaste överföringarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Transfer" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "4":
                    ColorHelper.ShowHighlightedChoice("Alla transaktioner:");
                    filtered = transactions
                        .Where(t => t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp);
                    break;

                case "5":
                    Console.WriteLine("Går tillbaka till föregående meny.\n");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               

                default:
                    ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               
            }
            //Utskrift i tabellformat
            Console.WriteLine(new string('-', 55));
            Console.WriteLine($"{"Tidstämpel:",-20}  {"Typ:",-12}  {"Kontonummer:",-10} {"Summa:",15}");
            Console.WriteLine(new string('-', 55));

            foreach (var t in filtered)
            {
                Console.WriteLine($"{t.Timestamp,-20} | {t.Type,-12} | {t.AccountNumber,-10} | {t.Amount,15:C}");
            }
            Console.WriteLine(new string('-', 55));

            return filtered.ToList();
        }
        public void PrintTotalBalanceAll()
        {
            Console.Clear();
            // Gruppera alla konton per ägare (Owner)
            var grouped = accounts
                .GroupBy(a => a.Value.Owner)
                 .Select(g => new     // g är varje grupp av konton för en användare
                 {
                     Owner = g.Key,      // användaren
                     TotalBalance = g.Sum(a => a.Value.Balance) // summera alla konton i gruppen
                 })
                 .OrderByDescending(g => g.TotalBalance); //sortera i fallande ordning

            ColorHelper.ShowHighlightedChoice("Totalt saldo per användare:\n");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Användarnamn:",-20} {"Summa:",15}");
            Console.WriteLine(new string('-', 50));

            foreach (var g in grouped)
            {
                Console.WriteLine($"{g.Owner.Name,-20} | {g.TotalBalance,15:C}");
            }
            Console.WriteLine(new string('-', 50));
        }

        public void ShowBiggestTransactionPerUser()
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("Största insättning & uttag per användare:\n");

            var groupedByUser = accounts.Values
                                        .GroupBy(a => a.Owner.Name);

            foreach (var group in groupedByUser)
            {
                var allTransactions = group.SelectMany(a => a.Transactions);
                var biggestDeposit = allTransactions
                                        .Where(t => t.Amount > 0)
                                        .OrderByDescending(t => t.Amount)
                                        .FirstOrDefault();
                var biggestWithdraw = allTransactions
                                            .Where(t => t.Amount < 0)
                                            .OrderBy(t => t.Amount) // mest negativt
                                            .FirstOrDefault();

                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"\nAnvändare: {group.Key}");
                Console.WriteLine($"  Största insättning: {(biggestDeposit != null ? biggestDeposit.Amount + " kr" : "Ingen")}");
                Console.WriteLine($"  Största uttag: {(biggestWithdraw != null ? biggestWithdraw.Amount + " kr" : "Ingen")}\n");
            }
            Console.WriteLine(new string('-', 40));
        }

        public void ShowUserWithMostTransactions()
        {
            Console.Clear();
            var userWithMost = transactions
                .GroupBy(t => FindAccount(t.AccountNumber).Owner.Name)
                .Select(g => new
                {
                    User = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(u => u.Count)
                .FirstOrDefault();

            if (userWithMost != null)
            {
                ColorHelper.ShowHighlightedChoice($"\nAnvändare med flest transaktioner: \n{userWithMost.User} med {userWithMost.Count} st.");
            }
            else
            {
                ColorHelper.ShowWarningMessage("Inga transaktioner hittades.");
            }
        }


        public void SearchAccount()
        {
            Console.Clear();
            ColorHelper.ShowInputPrompt("Ange kontonummer eller namn: ");
            string input = Console.ReadLine().ToLower();

            var results = accounts.Values
                                  .Where(a => a.AccountNumber.ToLower().Contains(input) ||
                                              a.Owner.Name.ToLower().Contains(input))
                                  .ToList();

            if (results.Count == 0)
            {
                ColorHelper.ShowWarningMessage("Inga konton hittades.");
                return;
            }

            ColorHelper.ShowHighlightedChoice("\nResultat:\n");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"Användarnamn:",-20} {"Kontonummer",-15}  {"Saldo:",15}");
            Console.WriteLine(new string('-', 60));

            foreach (var acc in results)
            {
                Console.WriteLine($"{acc.Owner.Name,-20} | {acc.AccountNumber,-15} | {acc.Balance,15:C}");
            }
            Console.WriteLine(new string('-', 60));
        }

        public void ShowAllTransactions(User user)
        {
            Console.Clear();
            string accNo = "";
            Account accNumber = null;

            while (accNumber == null)
            {
                ColorHelper.ShowInputPrompt("Kontonummer: ");
                accNo = Console.ReadLine();
                accNumber = FindAccount(accNo, user);
                
                if (accNumber == null)
                {
                    ColorHelper.ShowWarningMessage("❌ Kontot hittades inte. Försök igen.");
                }
            }
               
            LatestTransactions(accNo); //Kontonumret skickas till metoden
        }

        public void ShowAllMyAccountsAndMoney(User user)
        {
            Console.Clear();
            var accounts = ListAccounts(user);

            //Om inga konto finns
            if (accounts == null || !accounts.Any())
            {
                ColorHelper.ShowWarningMessage("ℹ️Du har inga konton.");
            }

            //Utskrift i tabellformat
            ColorHelper.ShowHighlightedChoice("Dina konton och saldo:\n");
            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"{"Användarnamn:",-15}  {"Kontonummer:",-10} {"Saldo:",10}");
            Console.WriteLine(new string('-', 45));

            foreach (var acc in accounts)
            {
                Console.WriteLine($"{user.Name,-15} | {acc.AccountNumber,-10} | {acc.Balance,10:C}");
            }
            Console.WriteLine(new string('-', 45));
        }

        public IEnumerable<Account> ListAccounts(User user)
        {
            if (user == null)
            {
                return new List<Account>();
            }
            return user.Accounts;
        }

        public void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear(); // Rensar konsolen för en fräsch meny
        }

        public void AddNewAccount(User user)
        {
            Console.Clear();
            string accountNumber;

            do
            {
                accountNumber = GenerateAccountNumber();
            }
            while (accounts.ContainsKey(accountNumber)); //Check if account number already exists

            string accountType = null;
            while (true)
            {
                Console.WriteLine("Vilken typ av konto vill du skapa?");
                Console.WriteLine("1. Sparkonto");
                Console.WriteLine("2. Lönekonto");
                ColorHelper.ShowInputPrompt("Val: ");
                string choice = Console.ReadLine();

                if (choice == "1") accountType = "spar";
                else if (choice == "2") accountType = "Löning";
                else
                {
                    ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                    continue;
                }
                break;
            }

            string currency = null; //Currency selection
            while (true)
            {
                ColorHelper.ShowInputPrompt("Vilken valuta vill du ha på kontot? (SEK, EUR, USD): ");
                currency = Console.ReadLine()?.ToUpper();
                if (currency == "SEK" || currency == "EUR" || currency == "USD") break;
                ColorHelper.ShowWarningMessage("Ogiltig valuta, försök igen.");
            }

            Account newAccount; //Create account based on type
            if (accountType == "spar") newAccount = new SavingAccount(accountNumber, user);
            else newAccount = new CheckingAccount(accountNumber, user);

            newAccount.Currency = currency;

            accounts.Add(accountNumber, newAccount);
            user.AddAccount(newAccount);

            ColorHelper.ShowSuccessMessage($"Nytt {accountType}-konto öppnat med kontonummer: {accountNumber} ({currency}).");

            if (accountType == "spar") //If saving account, ask for initial deposit
            {
                ColorHelper.ShowInputPrompt("Hur mycket vill du sätta in på ditt nya sparkonto? ");
                if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) && initialDeposit > 0)
                {
                    newAccount.Deposit(initialDeposit);
                    decimal interestRate = 0.02m; // 2% intrest rate
                    decimal totalAfterOneYear = initialDeposit * (1 + interestRate);
                    Console.WriteLine($"Insatt: {initialDeposit:C}, Ränta: {interestRate:P}, Efter 1 år: {totalAfterOneYear:C}");
                }
                else
                {
                    ColorHelper.ShowWarningMessage("Ogiltigt belopp. Ingen insättning gjord.");
                }
            }
        }

        private string GenerateAccountNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString(); //Genererar ett slumpmässigt 4-siffrigt kontonummer
        }

        public void LoanMoney(User user)
        {
            Console.Clear();
            Account selectedAccount;
            if (user.Accounts.Count == 1) //Om användaren bara har ett konto
            {
                selectedAccount = user.Accounts.First();
                Console.WriteLine($"Lånet sätts automatiskt in på konto {selectedAccount.AccountNumber}.");
            }
            else
            {
                ColorHelper.ShowInputPrompt("Välj konto att sätta in lånet på:");
                int index = 1;
                foreach (var acc in user.Accounts)
                {
                    Console.WriteLine($"{index}. Konto: {acc.AccountNumber}, Saldo: {acc.Balance} kr");
                    index++;
                }

                int choice;
                while (true)
                {
                    ColorHelper.ShowInputPrompt("Ange kontonummer: ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= user.Accounts.Count)
                    {
                        break; // giltigt val, bryt loopen
                    }

                    ColorHelper.ShowWarningMessage("Ogiltigt val, försök igen.");
                }

                selectedAccount = user.Accounts.ElementAt(choice - 1);
            }

            decimal amount;
            decimal totalBalance = user.Accounts.Sum(acc => acc.Balance); //skapar variabel för summering av användarens innehav på banken
            while (true) //Lånebelopp
            {
                Console.WriteLine($"Ditt totala innehav hos banken är {totalBalance} kr.");
                Console.WriteLine($"Du kan låna upp till {totalBalance * 5} kr.");
                ColorHelper.ShowInputPrompt("Ange lånebelopp: ");
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0 && amount <= totalBalance * 5)
                {
                    break;
                }
                ColorHelper.ShowWarningMessage($"Beloppet måste vara större än 0kr och får inte överskrida {totalBalance * 5} kr.");
            }

            decimal interestRate = 0.08m; //8% ränta
            decimal totalRepayment = amount + amount * interestRate;

            selectedAccount.Balance += amount; //Sätter in lånet på kontot
            ColorHelper.ShowSuccessMessage($"\nDu har lånat {amount:C} till konto {selectedAccount.AccountNumber}.");
            Console.WriteLine($"Ränta: {interestRate:P}");
            Console.WriteLine($"Totalt att betala tillbaka: {totalRepayment} kr");
            Console.WriteLine($"Nytt saldo på kontot: {selectedAccount.Balance} kr");
        }

        public void UpdateExchangeRates()
        {
            Console.Clear();
            ColorHelper.ShowHighlightedChoice("\n=== Uppdatera växelkurser ===");
            foreach (var key in ExchangeRates.Keys.ToList())
            {
                ColorHelper.ShowInputPrompt($"Ange ny växelkurs för {key} (nuvarande: {ExchangeRates[key]}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate > 0)
                {
                    ExchangeRates[key] = rate;
                }
                else
                {
                    ColorHelper.ShowWarningMessage("Ogiltig växelkurs, försök igen.");
                }              
            }

            ColorHelper.ShowSuccessMessage("Växelkurser uppdaterade!");
        }

        public User LoginUser()
        {
            ColorHelper.ShowInputPromptFirstMenu("Ange namn: ");
            string name = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                ColorHelper.ShowWarningMessageFirstMenu("❌ Okänt namn.");
                Pause();
                return null;
            }

            if (user.IsLocked)
            {
                ColorHelper.ShowWarningMessageFirstMenu("🔒 Kontot är låst. Kontakta admin");
                Pause();
                return null;
            }

            int attempts = 0;
            while (attempts < 3)
            {
                ColorHelper.ShowInputPromptFirstMenu("Ange PIN: ");
                string pin = Console.ReadLine();

                if (user.Pin == pin)
                {
                    ColorHelper.ShowSuccessMessageFirstMenu($"✅ Inloggad som {user.Name}!");
                    return user;
                }
                else
                {
                    attempts++;
                    ColorHelper.ShowErrorMessageFirstMenu($"❌ Fel PIN ({attempts}/3)");
                }
            }

            user.IsLocked = true;
            ColorHelper.ShowErrorMessageFirstMenu("🚫 Kontot är nu låst. Kontakta admin");
            Pause();
            return null;
        }
    }
}