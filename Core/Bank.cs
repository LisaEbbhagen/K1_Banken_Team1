namespace K1_Banken_Team1
{
    public class Bank
    {
        public void AdminMenu()
        {
            bool running = true;

            while (running)
            {

                Console.WriteLine("\n=== Admin Meny ===");
                Console.WriteLine("1. Lista alla konton");
                Console.WriteLine("2. Visa konton med positivt saldo");
                Console.WriteLine("3. Visa tre största transaktioner");
                Console.WriteLine("4. Visa total saldo per användare");
                Console.WriteLine("5. Visa största insättning eller uttag per användare");
                Console.WriteLine("6. Visa användare med flerst transaktioner");
                Console.WriteLine("7. Sök konto (kontonummer eller namn)");
                Console.WriteLine("8. Logga ut");
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
                            Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount} kr – Konto: {t.AccountNumber}");
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
                        Console.WriteLine("Loggar ut från Admin...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }



        public List<User> users { get; private set; } = new List<User>(); //Lista för användare

        public void AddUser(User user)
        {
            if (!users.Any(u => u.Id == user.Id))
            {
                users.Add(user);
                Console.WriteLine($"Användare {user.Name} med ID {user.Id} har lagts till.");
            }
            else
            {
                Console.WriteLine("Användaren finns redan.");
            }
        }

        public Dictionary<string, Account> accounts = new Dictionary<string, Account>(); //Dictionary för konton

        public List<Account> AccountsWithPositivBalance() //LINQ för konton med positivt saldo
        {
            return accounts.Values.Where(accounts => accounts.Balance > 0).ToList();
        }

        public void PrintAccountsWithPositivBalance() //Metod för att skriva ut konton med positivt saldo
        {
            var positivAccounts = AccountsWithPositivBalance();
            if (positivAccounts.Count == 0)
            {
                Console.WriteLine("Inga konton med positivt saldo.");
                return;
            }

            Console.WriteLine("Konton med positivt saldo:");
            foreach (var account in positivAccounts)
            {
                Console.WriteLine($"Konto: {account.AccountNumber}, Saldo: {account.Balance}SEK.");
            }
        }

        public List<Transaction> transactions { get; private set; } = new List<Transaction>(); //Lista för transaktioner
        public void OpenAccount(User user, string accountNumber) //Metod för att öppna konto
        {
            if (accounts.ContainsKey(accountNumber)) //Kollar om kontonumret redan finns
            {
                Console.WriteLine("Kontonumret finns redan.");
                return;
            }

            Account newAccount = new Account(accountNumber, user); //Skapar nytt konto
            accounts.Add(accountNumber, newAccount); //Lägger till kontot
            user.AddAccount(newAccount); //Lägger till kontot i användarens lista
        }

        public Account FindAccount(string accountNumber)//Metod för att hitta konto
        {
            accounts.TryGetValue(accountNumber, out Account account);
            return account;
        }

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            Account from = FindAccount(fromAccountNumber);
            Account to = FindAccount(toAccountNumber);

            if (from == null || to == null)
            {
                Console.WriteLine("Ett eller båda kontonumren är fel");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return false;
            }

            if (from.Withdraw(amount))
            {
                to.Deposit(amount);

                var transaction = new Transaction(
                    Guid.NewGuid().ToString(),
                    fromAccountNumber,
                    amount,
                    DateTime.Now,
                    "Transfer");

                // Lägg till i Bank.transactions
                transactions.Add(transaction);

                // Lägg till i respektive kontos transaktionslista
                from.AddTransaction(transaction);
                to.AddTransaction(transaction);

                Console.WriteLine($"För över {amount} från {fromAccountNumber} till {toAccountNumber}");
                return true;
            }

            Console.WriteLine("Överföring misslyckades.");
            return false;
        }

        public void ShowBalance()
        {
            foreach (var user in users)
            {
                decimal total = user.Accounts.Sum(a => a.Balance);
                Console.WriteLine($"{user.Name} {user.Id} - {total}SEK");
            }
        }

        public List<Transaction> threeBiggestAmount()
        {
            return transactions //returnera värden med följande tre metoder i beaktning
                .OrderByDescending(t => t.Amount) //sorterar listan i fallande ordning (Lambda)
                .Take(3)
                .ToList(); //returnerar resultatet till en vanlig lista
        }


        public void PrintTotalBalanceAll()
        {
            // Gruppera alla konton per ägare (Owner)
            var grouped = accounts
                .GroupBy(a => a.Value.Owner)
                 .Select(g => new     // g är varje grupp av konton för en användare
                 {
                     Owner = g.Key,      // användaren
                     TotalBalance = g.Sum(a => a.Value.Balance) // summera alla konton i gruppen
                 });

            Console.WriteLine("Totalt saldo per användare:");
            foreach (var g in grouped)
            {
                Console.WriteLine($"{g.Owner.Name} - {g.TotalBalance} SEK");
            }
        }

        public void ShowBiggestTransactionPerUser()
        {
            Console.WriteLine("Största insättning eller uttag per användare:");

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

                Console.WriteLine($"\nAnvändare: {group.Key}");
                Console.WriteLine($"  Största insättning: {(biggestDeposit != null ? biggestDeposit.Amount + " kr" : "Ingen")}");
                Console.WriteLine($"  Största uttag: {(biggestWithdraw != null ? biggestWithdraw.Amount + " kr" : "Ingen")}");
            }

        }

        public void ShowUserWithMostTransactions()
        {
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
                Console.WriteLine($"\nAnvändare med flest transaktioner: {userWithMost.User} med {userWithMost.Count} st.");
            }
            else
            {
                Console.WriteLine("Inga transaktioner hittades.");
            }
        }


        private void SearchAccount()
        {
            Console.WriteLine("\nAnge kontonummer eller namn:");
            string input = Console.ReadLine().ToLower();

            var results = accounts.Values
                                  .Where(a => a.AccountNumber.ToLower().Contains(input) ||
                                              a.Owner.Name.ToLower().Contains(input))
                                  .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Inga konton hittades.");
                return;
            }

            foreach (var acc in results)
            {           
                Console.WriteLine("\nResultat:");
                Console.WriteLine($"{acc.AccountNumber} {acc.Owner.Name} {acc.Balance} kr");
            }
        }
    }
}