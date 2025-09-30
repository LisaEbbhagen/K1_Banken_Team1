namespace K1_Banken_Team1
{
    public class Bank
    {
        public List<User> users { get; private set; } = new List<User>(); //Lista för användare
        public List<Account> accounts { get; private set; } = new List<Account>(); //Lista för konton

        public void OpenAccount(User user, string accountNumber) //Metod för att öppna konto
        {
            if (accounts.Any(a => a.AccountNumber == accountNumber)) //Kollar om kontonumret redan finns
            {
                Console.WriteLine("Kontonumret finns redan.");
                return;
            }

            Account newAccount = new Account(accountNumber, user); //Skapar nytt konto
            accounts.Add(newAccount); //Lägger till kontot i listan
            user.AddAccount(newAccount); //Lägger till kontot i användarens lista
        }

        public Account FindAccount(string accountNumber)//Metod för att hitta konto
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount) //metod för att skicka/ta ut pengar och kollar konton.
        {
            Account from = FindAccount(fromAccountNumber);
            Account to = FindAccount(toAccountNumber);

            if(from == null || to == null)
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
            foreach(var user in users)
            {
                decimal total = user.Accounts.Sum(a => a.Balance);
                Console.WriteLine($"{user.Name} {user.Id} - {total}SEK");
            }
        }

    }
}
