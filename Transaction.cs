using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTrial
{
    public enum TransactionType //representerar en transaktion som är kopplad till ett konto
    {
        Deposit,
        Withdrawal
    }
    public class Transaction
    {
        public string Id { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Account Account { get; private set; }

        //Konstruktor
        public Transaction(string id, DateTime date, decimal amount, TransactionType type, Account account)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Type = type;
            Account = account;
        }
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd HH:mm} | {Type} | {Amount:0.00} SEK | Account: {Account?.AccountNumber}";
        }

    }
}
