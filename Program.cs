namespace K1_Banken_Team1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)   //När running är true så körs meny loppen
            {
                Console.WriteLine("Välkommen till K1 Banken!\n");
                Console.WriteLine("Välj ett av följande alternativ!");

                Console.WriteLine("1. Logga in");
                Console.WriteLine("2. Skapa konto");
                Console.WriteLine("3. Avsluta");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        CreatAccount();
                        break;
                    case "3":
                        Console.WriteLine("Avslutar.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;


                }
            }
        }
    }
}
