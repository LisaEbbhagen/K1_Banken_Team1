using K1_Banken_Team1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class MenuHandler
    {
        private readonly Bank _myBank;
        private readonly AdminMenu adminMenu;
        private readonly MainMenu mainMenu;
        private readonly UserMenu userMenu;

        public MenuHandler()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Configerate console to support special characters.
            Console.InputEncoding = System.Text.Encoding.UTF8;

            _myBank = new Bank();

            var superAdmin = new User("superadmin", "admin123", "ADMIN", true); //"Superadmin" so we can access admin menu from "blank" program.
            _myBank.AddUser(superAdmin);

            var user1 = new User("Abdalle", "1111", "U01"); // Preload some users and accounts for testing(delete before turning in?).
            var user2 = new User("Lisa", "2222", "U02");
            var user3 = new User("Rolf", "3333", "U03");

            _myBank.AddUser(user1);
            _myBank.AddUser(user2);
            _myBank.AddUser(user3);

            _myBank.OpenAccount(user1, "A01", "spar", "SEK");
            _myBank.OpenAccount(user2, "A02", "spar", "SEK");
            _myBank.OpenAccount(user3, "A03", "spar", "SEK");

            mainMenu = new MainMenu(_myBank);
            adminMenu = new AdminMenu(_myBank);
            userMenu = new UserMenu(_myBank);
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                string nextMenu = mainMenu.RunMainMenu();

                switch (nextMenu)
                {
                    case "UserMenu":
                        User currentUser = _myBank.LoginUser();
                        if (currentUser != null)
                            userMenu.RunUserMenu(currentUser);
                        break;

                    case "AdminMenu":
                        User adminUser = _myBank.LoginUser();
                        if (adminUser != null && adminUser.IsAdmin)
                            adminMenu.RunAdminMenu();
                        else if (adminUser != null)
                            ColorHelper.ShowWarningMessage("Du har inte admin-behörighet.");
                        break;

                    case "Exit":
                        running = false;
                        break;
                }
            }

            Console.WriteLine("Programmet avslutas...");
        }    
    }
}
