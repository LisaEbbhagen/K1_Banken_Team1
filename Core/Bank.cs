using K1_Banken_Team1.Core;

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
                Console.WriteLine("3. Visa de tre största transaktioner");
                Console.WriteLine("4. Visa total saldo per användare");
                Console.WriteLine("5. Visa största insättning & uttag per användare");
                Console.WriteLine("6. Visa användare med flest transaktioner");
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
                        Pause();
                        break;

                    case "2":
                        PrintAccountsWithPositivBalance();
                        Pause();
                        break;

                    case "3":
                        var topThree = threeBiggestAmount();
                        Console.WriteLine("De tre största transaktionerna:");
                        foreach (var t in topThree)
                        {
                            Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount} kr – Konto: {t.AccountNumber}");
                        }
                        Pause();
                        break;

                    case "4":
                        PrintTotalBalanceAll();
                        Pause();
                        break;

                    case "5":
                        ShowBiggestTransactionPerUser();
                        Pause();
                        break;

                    case "6":
                        ShowUserWithMostTransactions();
                        Pause();
                        break;

                    case "7":
                        SearchAccount();
                        Pause();
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

        public List<Transaction> LatestTransactions()
        {
            Console.WriteLine("Vilken typ av transaktioner vill du se?");
            Console.WriteLine("1. Endast insättningar");
            Console.WriteLine("2. Endast uttag");
            Console.WriteLine("3. Endast överföringar");
            Console.WriteLine("4. Alla transaktioner");
            Console.WriteLine("5. Gå tillbaka till föregående meny");

            string choice = Console.ReadLine();
            IEnumerable<Transaction> filtered = Enumerable.Empty<Transaction>(); //Skapar en tom sekvens av Transaction-objekt, en tom lista som du kan fylla senare. 



            //return transactions //returnera värden med följande tre metoder i beaktning
            //   .OrderByDateTime(t => t.Amount) //sorterar listan i fallande ordning (Lambda)
            //   .Take(10)
            //   .ToList(); //returnerar resultatet till en vanlig lista
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Senaste insättningarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Deposit")
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "2":
                    Console.WriteLine("Senaste uttagen:");
                    filtered = transactions
                        .Where(t => t.Type == "Withdraw")
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "3":
                    Console.WriteLine("Senaste överföringarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Transfer")
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "4":
                    Console.WriteLine("Alla senaste transaktioner:");
                    filtered = transactions
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "5":
                    Console.WriteLine("Går tillbaka till föregående meny.\n");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               

                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               
            }
            //Console.WriteLine("\nDe senaste transaktionerna:");
            foreach (var t in filtered)
            {
                Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount:C} -Konto: {t.AccountNumber}");
            }

            return filtered.ToList();
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
                 })
                 .OrderByDescending(g => g.TotalBalance); //sortera i fallande ordning

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

            Console.WriteLine("\nResultat:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"{"Kontonummer",-15} {"Ägare",-20} {"Saldo",10}"); // -15 -20=Vänsterjustera och reservera 15 resp 20 tecken, 10=högerjustera o reservera 10 tecken. :C = formaterar som valuta
            Console.WriteLine("--------------------------------------------------");

            foreach (var acc in results)
            {
                Console.WriteLine($"{acc.AccountNumber,-15} {acc.Owner.Name,-20} {acc.Balance,10:C}");
            }
        }

        private void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear(); // Rensar konsolen för en fräsch meny
        }

        public void AddNewSavingsAccount(User user)
        {
            string accountNumber;

            do
            {
                accountNumber = GenerateAccountNumber();
            }
            while (accounts.ContainsKey(accountNumber)); //Kollar så att kontonumret inte redan finns

            SavingAccount newSavingsAccount = new SavingAccount(accountNumber, user); //Skapar nytt sparkonto
            accounts.Add(accountNumber, newSavingsAccount); //Lägger till kontot
            user.AddAccount(newSavingsAccount);

            Console.WriteLine($"Nytt sparkonto skapat med kontonummer: {accountNumber}");
            Console.WriteLine("Hur mycket vill du sätta in på ditt nya sparkonto?");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) && initialDeposit > 0)
            {
                newSavingsAccount.Deposit(initialDeposit);
            }
            else
            {
                Console.WriteLine("Ogiltigt belopp. Inget satt in på sparkontot.");
                return;
            }

            decimal íntrestRate = 0.02m; //2% ränta
            decimal yearlyIntrest = initialDeposit * íntrestRate;
            decimal totalAfterOneYear = initialDeposit + yearlyIntrest;

            Console.WriteLine($"Kontonummer: {accountNumber}");
            Console.WriteLine($"Insatt belopp: {initialDeposit:C}");
            Console.WriteLine($"Ränta: {íntrestRate:P}");
            Console.WriteLine($"Efter 1 år: {totalAfterOneYear:C}");
        }

        private string GenerateAccountNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString(); //Genererar ett slumpmässigt 4-siffrigt kontonummer
        }
    }
}