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

        public double Balance { get; private set; }

        public Account (string accountNumber)
        {
            AccountNumber = accountNumber;
            Balance = 0; // nytt konto har 0 i saldo
        }

    }
}
