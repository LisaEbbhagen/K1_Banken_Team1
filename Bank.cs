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


    }
}
