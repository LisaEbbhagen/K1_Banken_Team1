namespace K1_Banken_Team1
{
    public class Bank
    {
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

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount) //metod för att skicka/ta ut pengar och kollar konton.
        {
            Account from = FindAccount(fromAccountNumber);
            Account to = FindAccount(toAccountNumber);

            if (from == null || to == null)
            {
                Console.WriteLine("Ett eller båda kontonumren är fel");
                return false;
            }

            if (from.Withdraw(amount))
            {
                to.Deposit(amount);
                Console.WriteLine($"Förtöver {amount} från {fromAccountNumber} till {toAccountNumber}");
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
                .GroupBy(a => a.Owner)    
                 .Select(g => new     // g är varje grupp av konton för en användare
                 {
                    Owner = g.Key,      // användaren
                    TotalBalance = g.Sum(a => a.Balance) // summera alla konton i gruppen
                 });

            Console.WriteLine("Totalt saldo per användare:");
            foreach (var g in grouped)
            {
                Console.WriteLine($"{g.Owner.Name} - {g.TotalBalance} SEK");
            }
        }
    }
}
