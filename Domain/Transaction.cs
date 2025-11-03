using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class Transaction
    {
        public Transaction() { } //
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } //Val av type ska vara:
                                         //“Deposit”/“Withdraw”/“Transfer”
        public string Status { get; set; } = "Pending"; //Anger att transactionen är väntade 
        public decimal BalanceAfter { get; set; } //
        public string ToAccountNumber {  get; set; } //
        public Transaction(string id, string accountNumber, decimal amount, DateTime timestamp, string type, string toAccountNumber = null)
        {
            Id = id;
            AccountNumber = accountNumber;
            Amount = amount;
            Timestamp = timestamp;
            Type = type;
            ToAccountNumber = toAccountNumber;
        }
    }
}
