using System.Collections.Generic;

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

        public void CreateUser()
        {
            Console.Clear();
            Console.WriteLine("🧑‍💻 Skapa ny användare\n");

            string name;
            while (true)
            {
                Console.WriteLine("Ange namn: ");
                name = Console.ReadLine();

                if (users.Any(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase))) //kontrollera om namnet finns
                {
                    Console.WriteLine($"❌ Det finns redan en användare registrerad med namnet '{name}'. Välj ett annat namn.\n");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("❌ Namn får inte vara tomt.\n");
                    continue;
                }
                break; // namn är ok. gå vidare
            }

            string pin;
            while (true)
            {
                Console.WriteLine("Ange PIN (4 siffror): ");
                pin = Console.ReadLine();

                if (pin.Length == 4 && pin.All(char.IsDigit))
                {
                    break; // pin ok
                }
                Console.WriteLine("❌ Ogiltig PIN. Ange exakt 4 siffror.\n");

            }

            string id;
            while(true)
            {
                Console.WriteLine("Ange ID");
                id = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("❌ ID får inte vara tomt.\n ");
                    continue;
                }

                if (users.Any(u => u.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"❌ ID '{id}' andvänds redan. välj ett annat ID.");
                    continue;
                }
                break;
            }

            var newUser = new User(name, pin, id); //användaren skapas 
            AddUser(newUser);                     // användaren läggs till listan

            Console.WriteLine($"✅ Användaren'{name}' har skapats och lagts till i systemet!");
            Pause();

        }

        public void ListAllAccounts() //listar alla Konton, ägare, Saldo som är registrerad hos banken
        {
            Console.Clear();
            Console.WriteLine("Alla konton i banken \n");
            Console.WriteLine($"{"Konto",-15} {"Ägare",-10} {"Saldo",-10}");
            Console.WriteLine(new string('-', 40));
               
            foreach (var acc in accounts.Values)
            {
                Console.WriteLine($"{acc.AccountNumber, -15} {acc.Owner.Name, -10} {acc.Balance, -10}");
            }
            Pause();
        }

        public void ShowAllUsers()
        {
            Console.Clear();
            Console.WriteLine("👥 Alla användare i systemet \n");
            Console.WriteLine($"{"Name", -15} {"Id",-10}");
            Console.WriteLine(new string('-',30));

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name,-15} {user.Id,-10}");
            }
            Pause();
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
        public void OpenAccount(User user, string accountNumber, string currency = null) //Metod för att öppna konto
        {
            if (accounts.ContainsKey(accountNumber)) //Kollar om kontonumret redan finns
            {
                Console.WriteLine("Kontonumret finns redan.");
                return;
            }

            if (currency == null) //Ny valuta om ingen är vald
            {
                Console.WriteLine("Vilken valuta vill du ha på kontot? (SEK, EUR, USD)");
                currency = Console.ReadLine()?.ToUpper();
                if (currency != "SEK" && currency != "EUR" && currency != "USD")
                {
                    Console.WriteLine("Ogiltig valuta. Standardvaluta (SEK) används.");
                    currency = "SEK";
                }
            }

            Account newAccount = new Account(accountNumber, user)
            {
                Currency = currency
            };

            accounts.Add(accountNumber, newAccount);
            user.AddAccount(newAccount);

            Console.WriteLine($"Nytt konto öppnat med kontonummer: {accountNumber} ({currency}).");
        }

        public void UnLockUserMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Lås upp användare === \n");

            var lockedUsers = users.Where(u => u.IsLocked).ToList(); //Hämta alla låsta användare

            if (!lockedUsers.Any()) //Om inga låsta användare finns
            {
                Console.WriteLine("\nℹ️ Det finns inga låsta användare just nu.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Låsta användare:");
                foreach (var u in lockedUsers)
                {
                    Console.WriteLine($"-{u.Name}");
                }

                Console.WriteLine("\nAnge namnet på användaren du vill låsa upp:");
                string name = Console.ReadLine();

                var userToUnlock = lockedUsers //Hitta användaren
                    .FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (userToUnlock == null) //om användaren inte hittas
                {
                    Console.WriteLine("❌ Ingen användare hittades med det namnet. Ange namn igen. \n");
                    continue;
                }

                userToUnlock.IsLocked = false; //omanvändaren hittas

                Console.WriteLine($"🔒 Kontot för {userToUnlock.Name} har låsts upp!");

                return;
            }
        }

        //Metod för att hitta konto, user tillagd för att kunna modifiera koden utifrån användare eller admin.
        //Vid användning: om currentUser skickas in kommer programmet endast söka i användarens egna konton, skickas inte den parametern med kan alla konton väljas (exAdmin)
        public Account FindAccount(string accountNumber, User user = null)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Kontonumret får inte vara tomt.");
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
                Console.WriteLine("Konto finns ej.");
                return null;
            }
        }

        //Alla transaktionstyper samlade i en metod:
        public bool ExecuteTransaction(string type, string accountNumber, decimal amount, string toAccountNumber = null)

        {
            if (amount <= 0)
            {
                Console.WriteLine("Ogiltigt belopp. Ange ett belopp större än 0.");
                return false;
            }

            var account = FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Kontot hittades inte.");
                return false;
            }

            switch (type)
            {
                case "Deposit":
                    if (account.Deposit(amount))
                    {
                        var transD = account.Transactions.LastOrDefault(); //Transaktionen hämtas från bankens translista
                        if (transD != null) transactions.Add(transD); //Om transaktionen inte är null läggs den till i kontots translista
                        Console.WriteLine($"Insättning på {amount} kr till konto {accountNumber} genomförd.");
                        return true;
                    }
                    return false;

                case "Withdraw":
                    if (account.Withdraw(amount))
                    {
                        var transW = account.Transactions.LastOrDefault(); //Transaktionen hämtas från bankens translista
                        if (transW != null) transactions.Add(transW); //Om transaktionen inte är null läggs den till i kontots translista
                        Console.WriteLine($"Uttag på {amount} kr från konto {accountNumber} genomförd.");
                        return true;
                    }
                    Console.WriteLine("Uttag misslyckades, kontrollera saldo.");
                    return false;

                case "Transfer":
                    var toAccount = FindAccount(toAccountNumber);
                    if (toAccount == null)
                    {
                        Console.WriteLine("Mottagarkontot hittades inte.");
                        return false;
                    }

                    decimal finalAmount = amount;
                    if (account.Currency != toAccount.Currency) //Växling om kontona har olika valutor
                    {
                        if (!ExchangeRates.ContainsKey(account.Currency) || !ExchangeRates.ContainsKey(toAccount.Currency))
                        {
                            Console.WriteLine("Växelkurs saknas för en av valutorna.");
                            return false;
                        }

                        decimal amountInSEK = amount * ExchangeRates[account.Currency]; //Omvandla till SEK först
                        finalAmount = amountInSEK / ExchangeRates[toAccount.Currency]; //Omvandla till mottagarens valuta

                        Console.WriteLine($"Växlar {amount} {account.Currency} till {finalAmount:F2} {toAccount.Currency} enligt aktuell kurs.");
                    }
                    //verbose tystar withdraw metoden och skriver istället ut det vi vill nedan
                    if (!account.Withdraw(amount, verbose: false))
                    {
                        Console.WriteLine("Överföring misslyckades. Kontrollera saldo.");
                        return false;
                    }

                    if (!toAccount.Deposit(amount, verbose: false))
                    {
                        account.Deposit(amount, verbose: false);
                        Console.WriteLine("Överföring misslyckades vid insättning till mottagare.");
                        return false;
                    }

                    var transFrom = account.Transactions.LastOrDefault(); //Överföringen hämtas från bankens translista
                    var transTo = toAccount.Transactions.LastOrDefault(); //Överföringen hämtas från bankens translista
                    if (transFrom != null) transactions.Add(transFrom); //Om överföringen inte är null läggs den till i från-kontots translista
                    if (transTo != null) transactions.Add(transTo); //Om överföringen inte är null läggs den till i till-kontots translista

                    Console.WriteLine($"\nÖverförde {amount} {account.Currency} ({finalAmount:F2} {toAccount.Currency}) från {accountNumber} till {toAccountNumber}.");
                    Console.WriteLine($"Aktuellt saldo på ditt konto ({accountNumber}): {account.Balance} {account.Currency}\n");
                    return true;

                default:
                    Console.WriteLine("Ogiltig transaktionstyp.");
                    return false;
            }
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

        public void ShowThreeBiggestTransactions()
        {
            var topThree = threeBiggestAmount();
            Console.WriteLine("De tre största transaktionerna:");
            foreach (var t in topThree)
            {
                Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount} kr – Konto: {t.AccountNumber}"); //*fixa till utskriften, svenska o engelska blandas
            }
        }


        public List<Transaction> LatestTransactions(string accountNumber)
        {
            Console.WriteLine("Vilken typ av transaktioner vill du se?");
            Console.WriteLine("1. Endast insättningar");
            Console.WriteLine("2. Endast uttag");
            Console.WriteLine("3. Endast överföringar");
            Console.WriteLine("4. Alla transaktioner");
            Console.WriteLine("5. Gå tillbaka till föregående meny");

            string choice = Console.ReadLine();
            IEnumerable<Transaction> filtered = Enumerable.Empty<Transaction>(); //Skapar en tom sekvens av Transaction-objekt, en tom lista som du kan fylla senare. 

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Senaste insättningarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Deposit" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "2":
                    Console.WriteLine("Senaste uttagen:");
                    filtered = transactions
                        .Where(t => t.Type == "Withdraw" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "3":
                    Console.WriteLine("Senaste överföringarna:");
                    filtered = transactions
                        .Where(t => t.Type == "Transfer" && t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(10);
                    break;

                case "4":
                    Console.WriteLine("Alla transaktioner:");
                    filtered = transactions
                        .Where(t => t.AccountNumber == accountNumber)
                        .OrderByDescending(t => t.Timestamp);
                    break;

                case "5":
                    Console.WriteLine("Går tillbaka till föregående meny.\n");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               

                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    return new List<Transaction>(); // returnerar tom lista vilket gör att vi undviker ev krascher               
            }
            //Utskrift i tabellformat
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Tidstämpel:",-20}  {"Typ:",-12}  {"Summa:",-15}  {"Kontonummer:",-10}");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

            foreach (var t in filtered)
            {
                Console.WriteLine($"{t.Timestamp,-20} | {t.Type,-12} | {t.Amount,-15:C} | {t.AccountNumber,-10}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

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


        public void SearchAccount()
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

            Console.WriteLine("\nResultat:");  //Kolla denna efter main merge
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"{"Kontonummer",-15} {"Ägare",-20} {"Saldo",10}"); // -15 -20=Vänsterjustera och reservera 15 resp 20 tecken, 10=högerjustera o reservera 10 tecken. :C = formaterar som valuta
            Console.WriteLine("--------------------------------------------------");

            foreach (var acc in results)
            {
                Console.WriteLine("\nResultat:");
                Console.WriteLine($"{acc.AccountNumber} {acc.Owner.Name} {acc.Balance} kr");
            }
        }

        public void DepositMoney(User user) //Sätter in pengar på valt konto efter validering
        {
            Account accIn = null;
            decimal depositAmount = 0;

            while (true)
            {
                Console.WriteLine("Kontonummer: ");
                string accNoIn = Console.ReadLine();
                accIn = FindAccount(accNoIn, user);

                if (accIn == null)
                {
                    Console.WriteLine("❌Kontot hittades inte. Försök igen");
                    continue;
                }

                Console.WriteLine("belopp: ");
                if (!decimal.TryParse(Console.ReadLine(), out depositAmount) || depositAmount <= 0)
                {
                    Console.WriteLine("❌Ogiltigt belopp! ");
                    continue;
                }
                break; //om båda inmatmingarna är OK bryts loopen
            }
            
            if (ExecuteTransaction("Deposit", accIn.AccountNumber, depositAmount))
            {
                Console.WriteLine($"{depositAmount} kr insatt på konto {accIn.AccountNumber}." +
                    $"Nytt saldo: {accIn.Balance} kr.");
            }
            else
            {
                Console.WriteLine("Insättning misslyckades.");
            }
        }

        public void WithdrawMoney(User user) //ta ut pengar från valt konto efter validering
        {
            Account accOut = null;
            decimal withdrawAmount = 0;

            while (true)
            {
                Console.Write("Kontonummer: ");
                string accNoOut = Console.ReadLine();
                accOut = FindAccount(accNoOut, user);

                if (accOut == null)
                {
                    Console.WriteLine("❌ Kontot hittades inte. Försök igen");
                    continue;
                }

                Console.Write("Belopp: ");
                if (!decimal.TryParse(Console.ReadLine(), out withdrawAmount) || withdrawAmount <= 0)
                {
                    Console.WriteLine("❌ Ogiltigt belopp. Försök igen");
                    continue;
                }
                if (withdrawAmount > accOut.Balance)
                {
                    Console.WriteLine("❌ För lite pengar på kontot.");
                    continue;
                }
                break;
            }

                if (ExecuteTransaction("Withdraw", accOut.AccountNumber, withdrawAmount))
                {
                    Console.WriteLine($"✅ {withdrawAmount} kr uttaget från konto {accOut.AccountNumber}. Nytt saldo: {accOut.Balance} kr.");
                }
                else
                {
                    Console.WriteLine("Uttag misslyckades.");
                }
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

        public void AddNewSavingsAccount(User user)
        {
            string accountNumber;

            do
            {
                accountNumber = GenerateAccountNumber();
            }
            while (accounts.ContainsKey(accountNumber)); //Kollar så att kontonumret inte redan finns

            Console.WriteLine("Vilken valuta vill du ha på kontot? (SEK, EUR, USD)"); //Val av valuta
            string currency = Console.ReadLine()?.ToUpper();

            if (currency != "SEK" && currency != "EUR" && currency != "USD")
            {
                Console.WriteLine("Ogiltig valuta, standardvalutan SEK används.");
                currency = "SEK";
            }

            SavingAccount newSavingsAccount = new SavingAccount(accountNumber, user) //Skapar nytt sparkonto med vald valuta
            {
                Currency = currency
            };
            
            accounts.Add(accountNumber, newSavingsAccount); //Lägger till kontot
            user.AddAccount(newSavingsAccount);

            Console.WriteLine($"Nytt sparkonto skapat med kontonummer: {accountNumber} ({currency})");
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
            Console.WriteLine($"Valuta: {currency}");
            Console.WriteLine($"Insatt belopp: {initialDeposit:C}");
            Console.WriteLine($"Ränta: {íntrestRate:P}");
            Console.WriteLine($"Efter 1 år: {totalAfterOneYear:C}");
        }

        private string GenerateAccountNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString(); //Genererar ett slumpmässigt 4-siffrigt kontonummer
        }

        public void LoanMoney(User user)
        {
            Account selectedAccount;
            if (user.Accounts.Count == 1) //Om användaren bara har ett konto
            {
                selectedAccount = user.Accounts.First();
                Console.WriteLine($"Lånet sätts automatiskt in på konto {selectedAccount.AccountNumber}.");
            }
            else
            {
                Console.WriteLine("Välj konto att sätta in lånet på:");
                int index = 1;
                foreach (var acc in user.Accounts)
                {
                    Console.WriteLine($"{index}. Konto: {acc.AccountNumber}, Saldo: {acc.Balance:C}");
                    index++;
                }

                int choice;
                while (true)
                {
                    Console.Write("Ange kontonummer: ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= user.Accounts.Count)
                    {
                        break; // giltigt val, bryt loopen
                    }

                    Console.WriteLine("Ogiltigt val, försök igen.");
                }

                selectedAccount = user.Accounts.ElementAt(choice - 1);
            }

            decimal amount;
            decimal totalBalance = user.Accounts.Sum(acc => acc.Balance); //skapar variabel för summering av användarens innehav på banken
            while (true) //Lånebelopp
            {
                Console.WriteLine($"Ditt totala innehav hos banken är {totalBalance:C}.");
                Console.WriteLine($"Du kan låna upp till {totalBalance * 5:C}.");
                Console.Write("Ange lånebelopp: ");
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0 && amount <= totalBalance * 5)
                {
                    break;
                }
                Console.WriteLine($"Beloppet måste vara större än 0kr och får inte överskrida {totalBalance * 5:C}..");
            }

            decimal interestRate = 0.08m; //8% ränta
            decimal totalRepayment = amount + amount * interestRate;

            selectedAccount.Balance += amount; //Sätter in lånet på kontot
            Console.WriteLine($"\nDu har lånat {amount:C} till konto {selectedAccount.AccountNumber}.");
            Console.WriteLine($"Ränta: {interestRate:P}");
            Console.WriteLine($"Totalt att betala tillbaka: {totalRepayment:C}");
            Console.WriteLine($"Nytt saldo på kontot: {selectedAccount.Balance:C}");
        }

        public void UpdateExchangeRates()
        {
            Console.WriteLine("\n=== Uppdatera växelkurser ===");
            foreach (var key in ExchangeRates.Keys.ToList())
            {
                Console.Write($"Ange ny växelkurs för {key} (nuvarande: {ExchangeRates[key]}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate > 0)
                {
                    ExchangeRates[key] = rate;
                }
                else
                {
                    Console.WriteLine("Ogiltig växelkurs, försök igen.");
                }              
            }

            Console.WriteLine("Växelkurser uppdaterade!");
        }

        public User LoginUser()
        {
            Console.Write("Ange namn: ");
            string name = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                Console.WriteLine("❌ Okänt namn.");
                Pause();
                return null;
            }

            if (user.IsLocked)
            {
                Console.WriteLine("🔒 Kontot är låst.");
                Pause();
                return null;
            }

            int attempts = 0;
            while (attempts < 3)
            {
                Console.Write("Ange PIN: ");
                string pin = Console.ReadLine();

                if (user.Pin == pin)
                {
                    Console.WriteLine($"✅ Inloggad som {user.Name}!");
                    return user;
                }
                else
                {
                    attempts++;
                    Console.WriteLine($"❌ Fel PIN ({attempts}/3)");
                }
            }

            user.IsLocked = true;
            Console.WriteLine("🚫 Kontot är nu låst.");
            Pause();
            return null;
        }
    }
}