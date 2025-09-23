using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1
{
    public class User
    {
    }

    internal class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public User Owner { get; set; }

        public Account(string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Balance = 0;
            Owner = owner;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}
