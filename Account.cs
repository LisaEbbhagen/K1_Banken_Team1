using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public Decimal Balance { get; private set; }
        public User Owner {get; private set;}
    }

    public Account(string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0m;
        }
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            { 
              Console.WriteLine("enter an amount to deposit ");
            string input = Console.ReadLine();

            if (!decimal.TryParse(input, out decimal amount))
            {
                Console.WriteLine("Wrong: Invalid amount.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Wrong: The amount must be greater then zero");
                return;
            }

            Balance += amount;
            Console.WriteLine($"✅ {amount} kr Deposited. New Balance: {Balance} kr.");
        }


    }
}
