using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } //Val av type ska vara:
                                         //“Deposit”/“Withdraw”/“Transfer”
        
        public Transaction (string id, string accountNumber, decimal amount, DateTime timestamp, string type)
        {
            Id = id;
            AccountNumber = accountNumber;
            Amount = amount;
            Timestamp = timestamp;
            Type = type;
        }
    }
}
