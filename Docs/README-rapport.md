v.45 rapportör Abdalle

Deltagare (Närvaro): Abdalle, Lisa; Rolf

Sprintmål (veckans DoD): Fortsätte arbetet med Backlog.

Vad vi gjorde
fortsätt med backloggen och små fixade det sista innan inlämning och presentation
lisa Jobbat med refaktorering och updatering av alla metoder som har direkt koppling till menyval, med console.clear för att dölja menyn efter menyval.Har även sett över alla menyval där tabellutskrift är att föredra och lagt in tabeller på dem ställena av samma typ. Implementerar colorhelper-klassen för färgsättning i konsollen (inte riktigt klart)
Abdalle hade i uppgift att fixa små fel i transaktionerna och säkerställa korrekt validering när användaren anger konto eller pelopp, felaktiga konton eller ogiltiga summor ska nu stoppas direkt- allt fungerar som det ska
Rolf fixade logga med färg och såg till att den ser bra ut , han gjorde justeingar som behövdes så att CheckingAccount ska fungera väl 

Hinder:
vi stötte på några merge-konflikter och kodrader som behövde rättas till i samband med refaktoriseringen.
I övrigt uppstod inga större problem 

-------------------------------------------------------------------------------------------------------------------------------------------------
v.44 rapportör: Lisa

Deltagare (närvaro): Lisa, Rolf
Sprintmål (veckans DoD): Fortsätte arbetet med Backlog.

Vad vi gjorde:
Arbetade utifrån vår nya uppgiftslista/backlog i notion. 
Lisa begränsade våra metoder så användaren inte kan hantera andra än sina egna konton. La till menyval och logik för att användare ska kunna överföra pengar från något av sina konton till någon annans konto. 
Rolf gjorde en ny metod för att kunna låna pengar. Samt la till felhantering direkt i FindAccount metoden så man slipper skriva in felhantering varje gång.

Problem/hinder:
Lisa: Tampades lite med utskriften som ville skivas ut flera gånger pga metoderna som inkluderades (withdraw o deposit) löste det genom att lägga till en bool verbose som ger oss möjligheten att tysta withdraw o deposit metoderna om det är så att vi vill ha andra utskrifter när vi kallar på metoderna.
Rolf: Upptäckte efter arbetet med ny lånmetod var klar att han suttit o jobbat i dev. Vid försök att flytta över detta till en annan branch så stod det att det gick bra MEN ändå inte vilket var nytt så förlorade allt han gjort. Som tur var gick det rätt snabbt att skriva om allt.

___________________________________________________________________________________________________________________________


v.43 rapportör: Abdalle

Deltagare (närvaro): Lisa, Abdalle
Sprintmål (veckans DoD): Bygga vidare från föregående vecka och färdigställa menyvalen för insättning, uttag, visning av transaktioner samt saldo.

Vad vi gjorde:
Lisa och Abdalle gick igenom det senaste från föregående vecka. Abdalle implementerade funktioner för insättning, uttag och visning av senaste transaktioner.

Problem/hinder:
Abdalle hade problem med dev-branchen som visade fel version och inte gick att uppdatera. Efter att ha klonat om projektet kunde han fortsätta, men fick därefter merge-konflikter i pull requesten.
Lisa la då in Abdalles kod i menyvalen, vilket visade på ett bra samarbete i gruppen när något strular.
När arbetet skulle mergas skedde en felaktig merge till main, vilket gjorde det svårt att hitta tillbaka och orsakade ytterligare förseningar. Eftersom Rolf inte deltog denna vecka blev arbetet mer intensivt för Lisa och Abdalle.


___________________________________________________________________________________________________________________________

v.42 rapportör: Lisa
Deltagare (närvaro): Lisa, Rolf, Abdalle
Sprintmål (veckans DoD): Menygränssnitt (ConsoleUI) för inloggning: enkel loopad meny, se till att val för användaren endast visas i inloggat läge

Vad vi gjorde: Vi började med att gå igenom vad som gjorts förra veckan så alla var med på tåget. Rolf la till fler menyval och metoder för Admin. Lisa la till funktion att kunna visa transaktioner med filter samt uppdaterade UML. Abdalle jobbade med att hitta en lösning för att uttagen ska vara gratis/avgift/övertrassering. 

Problem/hinder: Abdalle hade till en början problem med att skapa pullrequests men vi löste det tillsammans. Vi hade också ett tag då två personer satt i samma uppdaterade dev-branch men den ena kunde köra programmet och den andra inte, hittade orsaken.
Kontraktet ej påskrivet av alla ännu. 
____________________________________________________________________________________________________________________-
v.41 rapportör: Rolf
Deltagare (närvaro): Lisa, Rolf
Sprintmål (veckans DoD): List, Dictionary, HashSet används på minst ett ställe var. Minst 3 LINQ-frågor (Where, OrderBy, GroupBy) fungerar och skriver ut resultat. Felhantering finns för tomma listor, null, ogiltig inmatning. Lokal demo körbar utan krascher.

Vad vi gjorde: Vi började med att gå igenom vad som gjorts förra veckan. Vi la till filtrering, sortering, gruppering och aggregering. Vi ändrade också några av våra List till Dictionary och HashSet där vi tror det skulle passa bra samt ändra de fel som kommer med att ändra List.

Problem/hinder: Bara två som var närvaro så det är svårt att få med allt så vi skulle behöva lite extra tid eller att alla är närvarande så vi hinner få med det vi missat från föregående workshops.
________________________________________________________________________________

v.40 rapportör: Rolf
Deltagare (närvaro): Abdalle, Rolf
Sprintmål (veckans DoD): Implementerat enkla use cases: skapa konto, lista konton, se saldo + Körbar demo 

Vad vi gjorde: Vi började dagen men ett möte med Malin där hon ville gå igenom Github igen med alla. Rolf ändrade menyn och la till en Transfer metod samt metod för att sätta in och ta ut pengar, Abdalle valde att göra SavingsAccount och en CheckingAccounts.

Problem/hinder: Bara två som var närvaro och båda är sjuka så blev mycket att göra för två sjuklingar. Abdelle stötte på massa buggar men han vet ej varför.
________________________________________________________________________________

v.39, rapportör: Lisa
Deltagare (närvaro): Abdalle, Rolf, Lisa 
Sprintmål (veckans DoD): Implementerat enkla use cases: skapa konto, lista konton, se saldo + Körbar demo

Vad vi gjorde: Bra struktur på mötet, lätt att komma igång. Alla bidrog. 
Skapade ett nytt projekt och gjorde förra veckans uppdrag på nytt. Ordnade med dokumentationen och mappstrukturen i GitHub. Kom nästan i mål med våra klasser, metoder o properties. Ingen färdig demo finns än. 

Problem/hinder: Problem att mergea från Abdalle och Rolfs brancher. Troligtvis berodde detta på att mappstrukturen fortfarande var under uppbyggnad när deras brancher skapades. Har fått ett nytt större konferensrum men det delas fortfarande med den andra Umeå-gruppen vilket upplevs lite distaherande. 

________________________________________________________________________________

v.38, rapportör: Lisa 
Team: Umeå/Sundsvall
Deltagare (närvaro): Robin, Niklas, Abdalle, Rolf, Lisa

Sprintmål (veckans DoD): UML + första POCO-klasser + README med teamets arbetsprocess. Check!

Vad vi gjorde: Vi har skapat en Consolapp och skapat ett repo med en main + develop. Vi har skapat klasserna användare, konto, transaktion. Vi har via inställningar i Github ställt in PR-krav med code review. Vi har skapat en mapp /docs i Github och skapat två textfiler. En för veckorapport och en med en UML-skiss.

Problem/hinder: Teknikstrul iom delat konferensrum, avsaknad av hörlurar mm. Vi hade lite utmaning med att komma igång.
