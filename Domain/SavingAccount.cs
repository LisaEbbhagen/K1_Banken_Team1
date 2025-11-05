using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class SavingAccount : Account
    {
         public SavingAccount(string accountNumber, User owner) : base(accountNumber, owner) { }

        public override bool Withdraw(decimal amount) //method for withdraw with fee.
        {
            if (amount <= 0 || amount > Balance)
            {
                Console.WriteLine("Error: Summan du väljer att sätta in måste vara mer än 0kr.");
                return false;
            }
            else
            {
                Balance -= (amount + 50);
                Console.WriteLine($"Aktuellt saldo efter uttag: {Balance}");
                return true;
            }
        }
    }
}

