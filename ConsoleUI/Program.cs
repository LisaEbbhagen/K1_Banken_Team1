using System.Diagnostics;

namespace K1_Banken_Team1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank myBank = new Bank();

            User Abdalle = new User("Abdalle", "1111", "U01");
            User Lisa = new User("Lisa", "2222", "U02");
            User Rolf = new User("Rolf", "3333", "U03");

            myBank.AddUser(Abdalle);
            myBank.AddUser(Lisa);
            myBank.AddUser(Rolf);

            myBank.OpenAccount(Abdalle, "A01");
            myBank.OpenAccount(Lisa, "A02");
            myBank.OpenAccount(Rolf, "A03");

            myBank.Transfer("B01", "A01", 50000);
            myBank.Transfer("B02", "A01", 2000);
            myBank.Transfer("A01", "B03", 5000);
            myBank.Transfer("A02", "B04", 300);
            myBank.Transfer("A02", "B05", 1000);
            myBank.Transfer("B06", "A02", 10000);
            myBank.Transfer("B07", "A02", 500);
            myBank.Transfer("B08", "A03", 2000);
            myBank.Transfer("B09", "A03", 30000);
            myBank.Transfer("A03", "B10", 500);

            bool running = true;

            while (running)   //När running är true så körs meny loppen
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
                        Console.Write("Ange namn: ");
                        string name = Console.ReadLine();

                        Console.Write("Ange PIN-kod: ");
                        string pin = Console.ReadLine();

                        User currentUser = myBank.users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && u.Pin == pin);
                        if (currentUser != null)
                        {
                            Console.WriteLine($"\nInloggad som {currentUser.Name}!");
                            RunUserMenu(myBank, currentUser); // gå till huvudmenyn
                        }
                        else
                        {
                            Console.WriteLine("Fel namn eller PIN-kod. Försök igen.");
                            Thread.Sleep(2000); // Pausa i 3 sekunder för att användaren ska hinna läsa meddelandet
                        }
                        break;

                    case "2": //Skapa konto (Ej implementerat)
                        
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
                        break;
                }

                static void RunUserMenu(Bank myBank, User currentUser)
                {
                    bool loggedIn = true;
                    Account userAccount = currentUser.Accounts.First(); // enkel version: varje användare har ett konto

                    while (loggedIn)
                    {
                        
                        Console.WriteLine("\n=== Meny ===");
                        Console.WriteLine("\nVälkommen till K1 Banken!\n");
                        Console.WriteLine("Välj ett av följande alternativ!");

                        Console.WriteLine("1. Sätta in pengar");
                        Console.WriteLine("2. Ta ut pengar");
                        Console.WriteLine("3. Visa transaktioner");
                        Console.WriteLine("4. Visa saldo");
                        Console.WriteLine("5. Skapa nytt sparkonto");
                        Console.WriteLine("6. Avsluta");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {

                            case "1":
                                //Sätter in penger på valt konto efter validering
                                Console.WriteLine("Kontonummer: ");
                                string accNoIn = Console.ReadLine();

                                var accIn = myBank.FindAccount(accNoIn);

                                if (accIn == null)
                                {
                                    Console.WriteLine("Kontot hittades inte.");
                                    break;
                                }
                                Console.WriteLine("belopp: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal depositAmount) || depositAmount <= 0)
                                {
                                    Console.WriteLine("Ogiltigt belopp! ");
                                    break;
                                }
                                else
                                {
                                    accIn.Balance += depositAmount;
                                    Console.WriteLine($"{depositAmount} kr insatt på konto {accIn.AccountNumber}." +
                                        $"Nytt saldo: {accIn.Balance} kr.");
                                }
                                   
                                break;

                            case "2":
                                //ta ut penger på valt konto efter validering
                                Console.Write("Kontonummer: ");
                                string accNoOut = Console.ReadLine();
                                var accOut = myBank.FindAccount(accNoOut);

                                if (accOut == null)
                                {
                                    Console.WriteLine("❌ Kontot hittades inte.");
                                    break;
                                }

                                Console.Write("Belopp: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount) || withdrawAmount <= 0)
                                {
                                    Console.WriteLine("❌ Ogiltigt belopp.");
                                    break;
                                }

                                if (withdrawAmount > accOut.Balance)
                                {
                                    Console.WriteLine("❌ För lite pengar på kontot.");
                                    break;
                                }

                                accOut.Balance -= withdrawAmount;
                                Console.WriteLine($"✅ {withdrawAmount} kr uttaget från konto {accOut.AccountNumber}. Nytt saldo: {accOut.Balance} kr.");
                                break;

                            case "3": //visa topp 3 transaktioner för valt konto 
                                Console.WriteLine("kontonummer:");
                                string accNoT = Console.ReadLine();

                                var top3 = myBank.transactions
                                    .Where(t => t.AccountNumber == accNoT)
                                    .OrderByDescending(t => t.Amount)
                                    .Take(3)
                                    .ToList();

                                if (top3.Count == 0)                    //om inga transaktioner finns
                                    Console.WriteLine("Inga transaktioner ännu.");
                                else                  // annars finns det minst 1 transaktion
                                {
                                    Console.WriteLine($"{top3[0].Type} | {top3[0].Amount} kr"); //skriv ut första
                                    if (top3.Count > 1) Console.WriteLine($"{top3[1].Type} | {top3[1].Amount} kr"); //om minst 2, skriv ut andra
                                    if (top3.Count > 2) Console.WriteLine($"{top3[2].Type} | {top3[2].Amount} kr"); //om minst 3, skriv ut tredje
                                }
                                break;

                            case "4":
                                Console.WriteLine("Kontonummer:"); // frågar om vilket konto saldot ska visas
                                string accNoBalance = Console.ReadLine();

                                var accBalance = myBank.FindAccount(accNoBalance); //letar upp kontot i banken

                                if (accBalance == null) // om kontot inte finns
                                {
                                    Console.WriteLine("Kontot hittades inte.");
                                    break;
                                }

                                //om kontot finns - skriv ut saldot
                                Console.WriteLine($"Saldo för konto {accBalance.AccountNumber}: {accBalance.Balance} kr");
                                break;


                            case "5":
                                myBank.AddNewSavingsAccount(currentUser);
                                break;

                            case "6":
                                Console.WriteLine($"Loggar ut {currentUser.Name}...");
                                loggedIn = false;
                                break;

                            default:
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                break;


                        }
                        
                        bool running = true;
                        while (running) // inre Loop - ger användaren val efter varje menyval (fortsätt eller avsluta)
                        {

                            Console.WriteLine("1. Forsätt\n" + "2. Avsluta");
                            string choice2 = Console.ReadLine();

                            if (choice2 == "1") 
                            { 
                                break; 
                            } //tillbaka till ytterloopen
                            else if (choice2 == "2") 
                            { 
                                running = false; 
                                break; 
                            }               //avsluta ytterloopen
                            else
                            {
                                Console.WriteLine("ogiltigt val! Skriv 1 eller 2.");
                            }

                        }
                    }

                }
            }
        }
    }
}
