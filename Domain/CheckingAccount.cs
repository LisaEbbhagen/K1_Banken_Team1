using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string accountNumber, User owner) : base(accountNumber, owner) { }
    }
}
