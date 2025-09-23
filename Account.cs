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

        public decimal Balance { get; private set; }
        public User Owner { get; private set; } // varje konto har en ägare

        public Account (string accountNumber, User owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0; // nytt konto har 0 i saldo
        }

    }
}
