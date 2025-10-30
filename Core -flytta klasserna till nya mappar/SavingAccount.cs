using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Core
{
    public class SavingAccount : Account
    {
         public SavingAccount(string accountNumber, User owner) : base(accountNumber, owner) { }

        public override bool Deposit(decimal amount, bool verbose = true) //metod för att sätta in pengar.
        {
            if (amount <= 0)
            {
                Console.WriteLine("Error: Summan du väljer att sätta in måste vara mer än 0kr.");
                return false;
            }

            else
            {
                Balance += amount;
                Console.WriteLine($"Aktuellt saldo efter insättning: {Balance}");
                return true;
            }
        }

        public override bool Withdraw(decimal amount, bool verbose = true) //metod för att ta ut pengar.
        {
            if (amount <= 0 || amount > Balance)
            {
                Console.WriteLine("Error: Summan du väljer att sätta in måste vara mer än 0kr.");
                return false;
            }
            else
            {
                Balance -= amount -= 50;
                Console.WriteLine($"Aktuellt saldo efter uttag: {Balance}");
                return true;
            }
        }
    }
}

