using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1
{
    public class User
    {
        public string Name { get; private set; }
        public string Pin { get; private set; }
        public string Id { get; private set; }

        public List<Account> Accounts { get; private set; }     // Aggregation/komposition → användaren äger sina konton


        public User(string name, string pin, string id)
        {
            Name = name;
            Pin = pin;
            Id = id;
            Accounts = new List<Account>(); // viktigt! annars är listan null
        }

        public void AddAccount(Account account)
        {
            if (!Accounts.Contains(account)) //kollar så att kontot inte redan finns i listan  
            {
                Accounts.Add(account);
            }

        }
    }
}