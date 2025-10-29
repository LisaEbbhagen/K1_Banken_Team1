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
                        myBank.Pause(); //myBank.pause
                        break;

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
                        Console.WriteLine("4. Visa alla mina konton och saldo");
                        Console.WriteLine("5. Logga ut");
                        
                        Console.WriteLine("4. Visa saldo");
                        Console.WriteLine("5. Skapa nytt sparkonto");
                        Console.WriteLine("6. Ta ett banklån");
                        Console.WriteLine("7. Avsluta");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {

                            case "1":
                            {
                                //Sätter in penger på valt konto efter validering
                                Console.WriteLine("Kontonummer: ");
                                string accNoIn = Console.ReadLine();

                                var accIn = myBank.FindAccount(accNoIn);

                                if (accIn == null)
                                {
                                    Console.WriteLine("❌Kontot hittades inte.");
                                    myBank.Pause();
                                    break;
                                }
                                Console.WriteLine("belopp: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal depositAmount) || depositAmount <= 0)
                                {
                                    Console.WriteLine("❌Ogiltigt belopp! ");
                                    myBank.Pause();
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
                                myBank.Pause();
                                break;
                            }

                            case "2":
                            {
                                //ta ut penger på valt konto efter validering
                                Console.Write("Kontonummer: ");
                                string accNoOut = Console.ReadLine();
                                var accOut = myBank.FindAccount(accNoOut);

                                if (accOut == null)
                                {
                                    Console.WriteLine("❌ Kontot hittades inte.");
                                    myBank.Pause();
                                    break;
                                }

                                Console.Write("Belopp: ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount) || withdrawAmount <= 0)
                                {
                                    Console.WriteLine("❌ Ogiltigt belopp.");
                                    myBank.Pause();
                                    break;
                                }
                                if (withdrawAmount > accOut.Balance)
                                {
                                    Console.WriteLine("❌ För lite pengar på kontot.");
                                    myBank.Pause();
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
                                myBank.Pause();
                                break;
                            }

                            case "3": //Visa transaktioner
                            {
                                Console.Write("Kontonummer: ");
                                string accNo = Console.ReadLine();
                                var acc = myBank.FindAccount(accNo);

                                if (acc == null)
                                {
                                    Console.WriteLine("❌ Kontot hittades inte.");
                                    myBank.Pause();
                                    break;
                                }
                              
                                var top3 = myBank.LatestTransactions(accNo); //Hämtar top3 för kontot
                                if (!top3.Any())
                                {
                                     Console.WriteLine("ℹ️ Inga transaktioner ännu.");
                                     myBank.Pause();
                                     break;
                                }
                            
                                Console.WriteLine("\nTop3 transactioner:");
                                foreach (var t in top3)
                                     {
                                         Console.WriteLine($"{t.Type} | {t.Amount} kr");
                                     }
                                myBank.Pause();
                                break;
                            }

                            case "4":
                            {
                                //visa alla konton + saldo
                                var accounts = myBank.ListAccounts(currentUser);

                                //Om inga konto finns
                                if (accounts == null || !accounts.Any())
                                {
                                    Console.WriteLine("ℹ️Du har inga konton.");
                                    myBank.Pause();
                                    break;
                                }
                                Console.WriteLine("\nDina konton och saldo:");
                                Console.WriteLine("--------------------");

                                //Rubriker med justering
                                Console.WriteLine($"{"Namn",-10} | {"Konto",-10} | {"Saldo",10}");
                            
                                foreach (var acc in accounts)
                                {
                                    Console.WriteLine($"{currentUser.Name,-10} | {acc.AccountNumber,-10} | {acc.Balance,10:0} kr");
                                }
                                myBank.Pause();
                                break;
                            }


                            case "5":
                            {
                                Console.WriteLine($"🔒Du loggas nu ut, {currentUser.Name}..."); 
                                myBank.AddNewSavingsAccount(currentUser);
                                break;

                            case "6":
                                myBank.LoanMoney(currentUser);
                                break;

                            case "7":
                                Console.WriteLine($"Loggar ut {currentUser.Name}...");
                                loggedIn = false;
                                return;
                            }

                            default:
                            {
                                Console.WriteLine("⚠Ogiltigt val, försök igen.");
                                myBank.Pause();
                                break;
                            }


                        }
                    }
                }
            }
        }
    }
}
