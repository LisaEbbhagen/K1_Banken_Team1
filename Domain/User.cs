using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Pin { get; private set; }
        public string Id { get; private set; }
        public bool IsAdmin { get; set; } = false; // Indicates whether the user has admin privileges.
        public bool IsLocked { get; set; } = false; // Indicates whether the user’s account is locked.

        public HashSet<Account> Accounts { get; private set; }     // Aggregation/Composition → The user owns their accounts.


        public User(string name, string pin, string id, bool isAdmin = false)
        {
            Name = name;
            Pin = pin;
            Id = id;
            IsAdmin = isAdmin;
            Accounts = new HashSet<Account>(); // Important! Otherwise the list will be null.
        }

        public void AddAccount(Account account)
        {
            if (!Accounts.Contains(account)) //Checks if the account is already in the list.
            {
                Accounts.Add(account);
            }

        }

        
       
    }
}
