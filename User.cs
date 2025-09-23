using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTrial
{
    public class User   //representerar en användare som kan äga ett eller flera bankkonton
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; } = new List<Account>(); //lista med användarens konton


        public User(string id, string name) //konstruktor
        {
            Id = id;
            Name = name;
            Accounts = new List<Account>();
        }
    }
}