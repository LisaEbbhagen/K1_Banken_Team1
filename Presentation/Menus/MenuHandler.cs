using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class MenuHandler
    {
        private readonly AdminMenu adminMenu;
        private readonly MainMenu mainMenu;
        private readonly UserMenu userMenu;

        public MenuHandler(AdminMenu adminMenu, MainMenu mainMenu, UserMenu userMenu)
        {
            this.adminMenu = adminMenu;
            this.mainMenu = mainMenu;
            this.userMenu = userMenu;
        }

        public void Start()
        {
            while (true)
            {

            }
        }
    }
}
