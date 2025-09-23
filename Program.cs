using System.Security.Principal;

namespace BankAppTrial
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Demo som skapar användare, ett konto och testar insättning/uttag
            var user = new User("U1", "Anton"); //Ny användare med ID och Namn
            var account = new Account("A1001", user, 0m); //Konto, StartSaldo

            account.Deposit(500m);
            account.Withdraw(200m);

            //Skapa ett test transaktion
            var transaction = new Transaction("T1", DateTime.Now, 500m, TransactionType.Deposit, account);
            Console.WriteLine(transaction);

            Console.ReadKey();
        }
    }
}