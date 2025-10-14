using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1
{
    public class Account
    {
        public string AccountNumber { get; private set; }

        public decimal Balance { get; protected set; } //För att subklasserna ska komma åt värdet behöver det vara protected
        public User Owner { get; private set; } // varje konto har en ägare

        List<Transaction> transactions = new List<Transaction>(); // varje konto har en lista med transaktioner
        public IReadOnlyList<Transaction> Transactions => transactions.AsReadOnly(); // gör listan med transaktioner read-only

        public Account(string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0; // nytt konto har 0 i saldo
        }

        public virtual bool Deposit(decimal amount) //metod för att sätta in pengar.
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error: Summan du väljer att sätta in måste vara mer än 0kr.");
                return false;
            }

                Balance += amount;
                Console.WriteLine($"Aktuellt saldo efter insättning: {Balance}");
                var transaction = new Transaction(
                Guid.NewGuid().ToString(), // unikt id för transaktionen
                AccountNumber,
                amount,
                DateTime.Now,
                "Deposit");

                transactions.Add(transaction);
                return true;
            }
        }

        public virtual bool Withdraw(decimal amount) //metod för att ta ut pengar.
        {
            if (amount <= 0 || amount > Balance)
            {
                Console.WriteLine("Error: Summan du väljer att sätta in måste vara mer än 0kr.");
                return false;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Aktuellt saldo efter uttag: {Balance}");
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

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public void PrintTransactions() //metod för att skriva ut transaktioner
        {
            Console.WriteLine($"\nTransaktioner för konto {AccountNumber}:");
            if (transactions.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Timestamp}: {t.Type} {t.Amount} kr");
            }
        }
    }
}
