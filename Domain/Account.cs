using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class Account
    {
        public string AccountNumber { get; private set; }

        public decimal Balance { get; set; }
        public User Owner { get; private set; } // varje konto har en ägare

        List<Transaction> transactions = new List<Transaction>(); // varje konto har en lista med transaktioner
        
        public IReadOnlyList<Transaction> Transactions => transactions.AsReadOnly(); // gör listan med transaktioner read-only
        
        public string Currency { get; set; } = "SEK"; // Standard

        public Account(string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0; // nytt konto har 0 i saldo
        }

        //metod för att sätta in pengar.
        public virtual bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;
           
            Balance += amount;

            var transaction = new Transaction(
                Guid.NewGuid().ToString(), // unikt id för transaktionen
                AccountNumber,
                amount,
                DateTime.Now,
                "Deposit");

            transactions.Add(transaction);
            return true;
        }


        public virtual bool Withdraw(decimal amount) //metod för att ta ut pengar.
        {
            if (amount <= 0 || amount > Balance) return false;
     
                Balance -= amount;
                var transaction = new Transaction(
                Guid.NewGuid().ToString(),
                AccountNumber,
                -amount, // negativt belopp för uttag
                DateTime.Now,
                "Withdraw");

                transactions.Add(transaction);
                return true;
        }     
    }
}
