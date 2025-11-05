using K1_Banken_Team1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace K1_Banken_Team1.Presentation.Menus
{
    public class MainMenu
    {
        private readonly Bank myBank;

        public MainMenu(Bank bank)
        {
            myBank = bank;
        }

        public string RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                string logo = @"                                                                                                                               
     $$\                                                                                                             $$\    
     $$ |                                                                                                            $$ |   
  $$$$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$\ $$$$$$$$\ 
  \__$$  __|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\____|\__$$  __|
     $$ |                                                                                                            $$ |   
     \__|                                                                                                            \__|                                                                                                                                                                                        
     $$\                   $$$$$$$\                   $$$$$$\                  $$\             $$\                   $$\ 
     $$ |                  $$  __$$\                 $$  __$$\                 $$ |            $  |                  $$ |
     $$ |                  $$ |  $$ |                $$ /  $$ |                $$ |            \_/$$$$$$$\           $$ |
     \__|                  $$$$$$$  |                $$$$$$$$ |                $$ |              $$  _____|          \__|
     $$\                   $$  __$$<                 $$  __$$ |                $$ |              \$$$$$$\            $$\ 
     $$ |                  $$ |  $$ |                $$ |  $$ |                $$ |               \____$$\           $$ |
     $$ |                  $$ |  $$ |      $$\       $$ |  $$ |      $$\       $$$$$$$$\         $$$$$$$  |          $$ |
     \__|                  \__|  \__|      \__|      \__|  \__|      \__|      \________|        \_______/           \__|
                                                                                                                                                                                                                                                              
     $$\                               $$$$$$$\   $$$$$$\  $$\   $$\ $$\   $$\                                       $$\     
     $$ |                              $$  __$$\ $$  __$$\ $$$\  $$ |$$ | $$  |                                      $$ |    
     $$ |                              $$ |  $$ |$$ /  $$ |$$$$\ $$ |$$ |$$  /                                       $$ |    
     \__|                              $$$$$$$\ |$$$$$$$$ |$$ $$\$$ |$$$$$  /                                        \__|    
     $$\                               $$  __$$\ $$  __$$ |$$ \$$$$ |$$  $$<                                         $$\     
     $$ |                              $$ |  $$ |$$ |  $$ |$$ |\$$$ |$$ |\$$\                                        $$ |    
     $$ |                              $$$$$$$  |$$ |  $$ |$$ | \$$ |$$ | \$$\                                       $$ |    
     \__|                              \_______/ \__|  \__|\__|  \__|\__|  \__|                                      \__|
     $$\                                                                                                             $$\    
     $$ |                                                                                                            $$ |   
  $$$$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$$$\ $$$$\ $$$$$$$$\ 
  \__$$  __|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\______|\____|\__$$  __|
     $$ |                                                                                                            $$ |   
     \__|                                                                                                            \__|
";


                ColorHelper.ShowColoredLogo(logo);

                logo = string.Join("\n", logo
                              .Split('\n')
                               .Where(line => !string.IsNullOrEmpty(line)));  //Remove empty lines to make logo look better

                ColorHelper.ShowTitle("                                               === Välkommen till R.A.L Banken ==="); //Rough version to make menu more appealing/centerd
                ColorHelper.ShowMenuChoice("                                                         1. Logga in");
                ColorHelper.ShowMenuChoice("                                                         2. Admin");
                ColorHelper.ShowInputPrompt("                                                         3. Avsluta");
                Console.WriteLine();
                Console.Write("                                                         Val: ");
                string startChoice = Console.ReadLine();

                switch (startChoice)
                {
                    case "1":
                        return "UserMenu";    // Return the value, do not run the UserMenu here, and return the value.
                    case "2":
                        return "AdminMenu";  // Return the value.
                    case "3":
                        return "Exit";
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        myBank.Pause();
                        return "MainMenu";
                }
            }
        }
    }
}


