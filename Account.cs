using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTrial
{
    public class Account //Representerar ett bankkonto med nummer, saldo, ägare
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public User Owner { get; private set; }


        public Account(string accountNumber, User owner, decimal openingBalance = 0m) //konstruktor
        {
            if (string.IsNullOrWhiteSpace(accountNumber)) //kontrollerar att kontonumret är inte tomt
                throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));
            if (owner is null)             //kontrollerar att ägaraen är inte Null
                throw new ArgumentNullException(nameof(owner), "owner cannot be null.");
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = openingBalance;
        }
        public bool Deposit(decimal amount) //method för insättning
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0,");
                return false;
            }
            Balance += amount;
            Console.WriteLine($"Deposit successful. New balance: {Balance: 0.00} SEK.");
            return true;
        }

        public bool Withdraw(decimal amount) //method för uttag
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0.");
                return false;
            }

            if (amount > Balance)
            {
                Console.WriteLine($"Insuffiecient funds. Current balance: {Balance:0.00} SEK.");
                return false;
            }

            Balance -= amount;
            Console.WriteLine($"Withdrawal successful. New Balance: {Balance: 0.00} SEK.");
            return true;
        }
    }
}