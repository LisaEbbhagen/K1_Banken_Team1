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
        public User Owner { get; private set; } // Each account has an owner.

        List<Transaction> transactions = new List<Transaction>(); // Each account has a list of transactions.

        public IReadOnlyList<Transaction> Transactions => transactions.AsReadOnly(); // make the transactions list read-only.

        public string Currency { get; set; } = "SEK"; // Standard currency is SEK.

        public Account(string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0; // New accounts start with a balance of 0.
        }

        //metod for deposit money.
        public virtual bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;
           
            Balance += amount;

            var transaction = new Transaction(
                Guid.NewGuid().ToString(), // unique id for the transaction
                AccountNumber,
                amount,
                DateTime.Now,
                "Deposit");

            transactions.Add(transaction);
            return true;
        }


        public virtual bool Withdraw(decimal amount) //method to take out money.
        {
            if (amount <= 0 || amount > Balance) return false;
     
                Balance -= amount;
                var transaction = new Transaction(
                Guid.NewGuid().ToString(),
                AccountNumber,
                -amount, // negative amount for withdrawal
                DateTime.Now,
                "Withdraw");

                transactions.Add(transaction);
                return true;
        }     
    }
}
