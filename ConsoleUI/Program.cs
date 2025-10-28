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

            myBank.ExecuteTransaction("Deposit", "A01", 20000);
            myBank.ExecuteTransaction("Withdraw", "A01", 5000);
            myBank.ExecuteTransaction("Deposit", "A02", 15000);
            myBank.ExecuteTransaction("Withdraw", "A02", 7000);
            myBank.ExecuteTransaction("Deposit", "A03", 25000);
            myBank.ExecuteTransaction("Withdraw", "A03", 500);

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
                        Console.WriteLine("3. Överför pengar"); 
                        Console.WriteLine("4. Visa transaktioner");
                        Console.WriteLine("5. Visa saldo");
                        Console.WriteLine("6. Avsluta");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {

                            case "1":
                                //Sätter in penger på valt konto efter validering
                                Console.WriteLine("Kontonummer: ");
                                string accNoIn = Console.ReadLine();

                                var accIn = myBank.FindAccount(accNoIn, currentUser); 

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
                           
                                if (myBank.ExecuteTransaction("Deposit", accIn.AccountNumber, depositAmount))
                                {
                                    Console.WriteLine($"{depositAmount} kr insatt på konto {accIn.AccountNumber}." +
                                        $"Nytt saldo: {accIn.Balance} kr.");
                                }
                                else
                                {
                                    Console.WriteLine("Insättning misslyckades.");
                                }
                                break;

                            case "2":
                                //ta ut penger på valt konto efter validering
                                Console.Write("Kontonummer: ");
                                string accNoOut = Console.ReadLine();
                                var accOut = myBank.FindAccount(accNoOut, currentUser);

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
                                if (myBank.ExecuteTransaction("Withdraw", accOut.AccountNumber, withdrawAmount))
                                { 
                                    Console.WriteLine($"✅ {withdrawAmount} kr uttaget från konto {accOut.AccountNumber}. Nytt saldo: {accOut.Balance} kr.");
                                }
                                else
                                {
                                    Console.WriteLine("Uttag misslyckades.");
                                }
                                break;

                            case "3": //Överför pengar
                                Console.Write("Vilket konto vill du överföra pengar från? ");
                                string fromAccNo = Console.ReadLine();
                                var fromAccNumber = myBank.FindAccount(fromAccNo, currentUser);

                                Console.Write("Vilket konto vill du överföra pengar till? ");
                                string toAccNo = Console.ReadLine();
                                var toAccNumber = myBank.FindAccount(toAccNo);

                                if (fromAccNumber == null )
                                {
                                    Console.WriteLine("❌ Kontot du vill överföra pengar från hittades inte.");
                                    break;
                                }

                                if (toAccNumber == null)
                                {
                                    Console.WriteLine("❌ Kontot du vill överföra pengar till hittades inte.");
                                    break;
                                }

                                Console.Write("Vilket belopp vill du överföra? ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount) || transferAmount <= 0)
                                {
                                    Console.WriteLine("\nOgiltigt belopp!");
                                    break;
                                }

                                else
                                {
                                    myBank.ExecuteTransaction("Transfer", fromAccNo, transferAmount, toAccNo);
                                }
                                break;                   

                            case "4": //Visa transaktioner
                                Console.Write("Kontonummer: ");
                                string accNo = Console.ReadLine();
                                var accNumber = myBank.FindAccount(accNo, currentUser);

                                if (accNumber == null)
                                {
                                    Console.WriteLine("❌ Kontot hittades inte.");
                                    break;
                                }
                                else
                                {
                                    myBank.LatestTransactions(accNo); //Kontonumret skickas till metoden 
                                }
                                break;

                       
                            case "5": //Visa saldo
                                Console.WriteLine("Kontonummer:"); // frågar om vilket konto saldot ska visas
                                string accNoBalance = Console.ReadLine();

                                var accBalance = myBank.FindAccount(accNoBalance, currentUser); //letar upp kontot i banken

                                if (accBalance == null) // om kontot inte finns
                                {
                                    Console.WriteLine("Kontot hittades inte.");
                                    break;
                                }

                                //om kontot finns - skriv ut saldot
                                Console.WriteLine($"Saldo för konto {accBalance.AccountNumber}: {accBalance.Balance} kr");
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
