namespace K1_Banken_Team1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank myBank = new Bank();

            User Abdalle = new User("Abdalle", "4729", "U01");
            User Lisa = new User("Lisa", "5842", "U02");
            User Rolf = new User("Rolf", "1406", "U03");

            myBank.AddUser(Abdalle);
            myBank.AddUser(Lisa);
            myBank.AddUser(Rolf);

            myBank.OpenAccount(Abdalle, "A01");
            myBank.OpenAccount(Lisa, "A02");
            myBank.OpenAccount(Rolf, "A03");

            bool running = true;

            while (running)   //När running är true så körs meny loppen
            {
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
                        decimal dAmount = decimal.Parse(Console.ReadLine());
                        //mer kod?
                        break;
                    case "2":
                        Console.WriteLine("Belopp: ");
                        decimal wAmount = decimal.Parse(Console.ReadLine());
                        //mer kod?
                        break;
                    case "3": //visa transaktioner 
                        Console.WriteLine("De tre största transaktionerna är:");
                        //Sortera transaktioner efter belopp, visa dem tre största
                        var topThree = myBank.threeBiggestAmount();
                        foreach (var trans in topThree)
                        {
                            Console.WriteLine(trans);
                        }
                        break;
                    case "4":
                        myBank.ShowBalance();
                        break;
                    case "5":
                        Console.WriteLine("Avslutar.");
                        running = false; //Sätter running till false för att avsluta loopen
                        break;

                }
            }

        }
    }
}
