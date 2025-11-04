Abdalle
i veckan gjorde jag följande
-Användare ska kunna se en lista med alla sina bankkontotn och saldot på dessa
      -Skapade metoden ListAccount() i bank.cs för att hämta användarens konto
      -lade till Menyvalet "Visa mina konton" i UserMenu.
-Adminstratör kan skapa nya användare
      -Lade till MenyVal i adminMenu:
                -case "10" Skapa ny användare
                -case "11"visa alla användare
      -Skapade metoder createUser() och ShowallUsers() i Bank.cs
-Användare som misslyckas att logga in 3 ggr ska låsas och bara Admin kan ta bort spärren
      -la till lås status IsLocked i User klassen
      -blockerade låsta användare från att logga in
      -Skapade admin-funktionför upplåsning
-Transaktioner(Överföring, Insättning, Uttag) sker efter 15 min
      -Implementerat Pending-system för All transaktionstyper 
      -la till metoden ProcessPendingTransactions()som hanterar körningen automatiskt
      -förbättrade strukturen i UserMenu(case1-4) så att alla transaktioner hanteras enhetligt.
      
Hinder/ Abdalle
Många gånger behövde jag göra om kodrader för att antingen jag missförtådd instruktioner eller så man kom på Ideer som förbättrar strukturer 
exemplvis 
-om användare blir låst vem gör upplåsning
-transaktioner ska ske efter 15 min är det bara transfer , eller det gäller alla transaktionstyper med mera.
      
    
