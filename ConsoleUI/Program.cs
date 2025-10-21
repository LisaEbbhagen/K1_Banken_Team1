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
                        Console.WriteLine("5. Avsluta");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {

                            case "1":
                                Console.WriteLine("Belopp: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                                {
                                    if (userAccount.Deposit(depositAmount))
                                        Console.WriteLine($"Du satte in {depositAmount} SEK.");

                                    else
                                        Console.WriteLine("Felaktigt belopp.");
                                }
                                break;

                            case "2":
                                Console.WriteLine("Ta ut belopp: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                                {
                                    if (userAccount.Withdraw(withdrawAmount))
                                        Console.WriteLine($"Du tog ut {withdrawAmount} SEK.");
                                    else
                                        Console.WriteLine("Felaktigt belopp eller för lite saldo.");
                                }
                                break;

                            case "3": //visa transaktioner BEHÖVER FIXAS
                                var latest = myBank.LatestTransactions();
                                //Console.WriteLine("De tre största transaktionerna är:");
                                ////Sortera transaktioner efter belopp, visa dem tre största
                                //var topThree = myBank.threeBiggestAmount();
                                //foreach (var trans in topThree)
                                //{
                                //    Console.WriteLine(trans);
                                //}
                                break;

                            case "4":
                                Console.WriteLine($"Saldo: {userAccount.Balance} SEK");
                                break;

                            case "5":
                                Console.WriteLine($"Loggar ut {currentUser.Name}...");
                                loggedIn = false;
                                break;

                            default:
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                break;


                        }
                    }

                }
            }
        }
    }
}
