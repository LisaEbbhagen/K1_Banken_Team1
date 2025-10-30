using K1_Banken_Team1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Banken_Team1.Presentation.Menus
{
    public class MainMenu
    {
        public void RunMainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Välkommen till K1 Banken ===");
                Console.WriteLine("1. Logga in");
                Console.WriteLine("2. Skapa konto (Ej implementerat)");
                Console.WriteLine("3. Admin");
                Console.WriteLine("4. Avsluta");

                Console.Write("Val: ");
                string startChoice = Console.ReadLine();

                switch (startChoice)
                {
                    case "1":
                        {
                            int failedAttempts = 0; //Räknare för misslyckade försök
                            Console.Write("Ange namn: ");
                            string name = Console.ReadLine();

                            var user = myBank.users  //kontrollera om kontot finns 
                            .FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                            if (user == null)
                            {
                                Console.WriteLine("❌ Okänt namn. Försök igen.");
                                myBank.Pause();
                                break;
                            }
                            if (user.IsLocked) // om kontot finns - kolla om det redan är låst
                            {
                                Console.WriteLine("🔒 Ditt konto är låst. Kontakta en administratör för att låsa upp det.");
                                myBank.Pause();
                                break;
                            }

                            while (failedAttempts < 3) // frågar efter PIN-kod
                            {
                                Console.WriteLine("Ange PIN-kod: ");
                                string pin = Console.ReadLine();
                                if (user.Pin == pin)
                                {
                                    Console.WriteLine($"\n✅ Inloggad som {user.Name}!");
                                    RunUserMenu(myBank, user); //gå vidare till användarmenyn
                                    break;
                                }
                                else
                                {
                                    failedAttempts++; //öka misslyckade försök

                                    if (failedAttempts < 3)
                                    {
                                        Console.WriteLine($"❌ Fel PIN-kod. Försök igen. ({failedAttempts}/3)");
                                    }
                                    else
                                    {
                                        user.IsLocked = true; //efter tredje försök- lås kontot
                                        Console.WriteLine("🚫 3 misslyckade fel PIN-kod. Kontot är Låst."
                                            + "Kontakta en administratör för att låsa upp ditt konto");
                                    }
                                }
                            }
                            myBank.Pause();
                            break;
                        }

                    case "2": //Skapa konto (Ej implementerat)

                        myBank.Pause();
                        break;

                    case "3":
                        myBank.AdminMenu();
                        break;

                    case "4":
                        Console.WriteLine("Avslutar programmet...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ReadKey();
                        myBank.Pause();
                        break;
                }
            }
        }
    }
}


