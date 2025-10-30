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

        public MenuHandler(Bank myBank, AdminMenu adminMenu, MainMenu mainMenu, UserMenu userMenu)
        {
            _myBank = myBank;
            this.adminMenu = adminMenu;
            this.mainMenu = mainMenu;
            this.userMenu = userMenu;
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
                        adminMenu.RunAdminMenu();
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
