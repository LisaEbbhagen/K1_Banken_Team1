using K1_Banken_Team1.Domain;
using K1_Banken_Team1.Presentation;
using K1_Banken_Team1.Presentation.Menus;
using System.Diagnostics;
namespace K1_Banken_Team1
{
    internal class Program
    {

       
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;


            Bank myBank = new Bank();

            //Ta bort hårdkodningen innan inlämning
            User Abdalle = new User("Abdalle", "1111", "U01");
            User Lisa = new User("Lisa", "2222", "U02");
            User Rolf = new User("Rolf", "3333", "U03");

            myBank.AddUser(Abdalle);
            myBank.AddUser(Lisa);
            myBank.AddUser(Rolf);

            myBank.OpenAccount(Abdalle, "A01", "SEK"); //fix för att inte behöva välja valuta varje gång
            myBank.OpenAccount(Lisa, "A02", "SEK");
            myBank.OpenAccount(Rolf, "A03", "SEK");
           

            //myBank.ExecuteTransaction("Deposit", "A01", 20000);
            //myBank.ExecuteTransaction("Withdraw", "A01", 5000);
            //myBank.ExecuteTransaction("Deposit", "A02", 15000);
            //myBank.ExecuteTransaction("Withdraw", "A02", 7000);
            //myBank.ExecuteTransaction("Deposit", "A03", 25000);
            //myBank.ExecuteTransaction("Withdraw", "A03", 500);

            var mainMenu = new MainMenu(myBank); // Skapa MainMenu-instans
            var adminMenu = new AdminMenu(myBank); // Skapa AdminMenu-instans
            var userMenu = new UserMenu(myBank); // Skapa UserMenu-instans

            var menuHandler = new MenuHandler(myBank, adminMenu, mainMenu, userMenu); // Skapa MenuHandler-instans

            menuHandler.Start(); // Starta menyhanteraren   

        }
    }
}