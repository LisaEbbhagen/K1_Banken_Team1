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

                foreach (char c in logo)   // Print logo with colors
                {
                    if (c == '$')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(c);
                }

                logo = string.Join("\n", logo
                              .Split('\n')
                               .Where(line => !string.IsNullOrEmpty(line)));  //Remove empty lines to make logo look better

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("                                               === Välkommen till R.A.L Banken ==="); //Rough version to make menu more appealing/centerd
                Console.WriteLine("                                                         1. Logga in");
                Console.WriteLine("                                                         2. Admin");
                Console.WriteLine("                                                         3. Avsluta");

                Console.Write("                                                            Val: ");
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


