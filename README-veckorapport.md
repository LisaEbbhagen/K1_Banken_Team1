# Under uppbyggnad:

v.44 

Deltagare (närvaro): Lisa, Rolf, Abdalle
Sprintmål (veckans DoD): Fortsatte arbetet med vår Backlog.

Vad vi gjorde:
Vi skapade en backlog i notion med storys och tasks. Vi uppskattade tillsammans hur svåra och tidskrävande dem var samt vilket prioritet uppgiften skulle få. Vi fördelade uppgifterna med prio hög och medel via slumpmässiga tärningskast.
Har sedan arbetat utifrån vår nya backlog i notion. 
Lisa: Alla transaktioner som är kopplat till specifikt konto ska nu kunna hittas, det är även begränsat så du inte kan hantera andras pengar. Däremot finns nu funktion för att föra över pengar från dig till annan persons konto. Utskrifterna av transaktioner i adminmenyn funkar nu. Skapade veckorapport, boilerroomrapport och en ReadMe på engelska. Skapade en ny klass "ColorHelper" med förslag på färgsättande metoder. Påbörjat refaktorering, brutit ut logik från main och menyerna samt skapat ny mappstruktur. 
Rolf: gjorde en ny metod för att kunna låna pengar. Samt la till felhantering direkt i FindAccount metoden så man slipper skriva in felhantering varje gång.
Abdalle: 

Problem/hinder:
Lisa: Hade i början problem med att endast insättningarna visades på våra hårdkodade exempel. Visade sig att jag skrivit in uttagen före insättningarna på våra hårdkodade konton vilket gjorde att de aldrig skapades (ändrade ordning, funkar nu fint)..Tampades även lite med utskrifter som ville skivas ut flera gånger pga metoder som inkluderades, löste det genom att lägga till en bool verbose som ger oss möjligheten att tysta metoder om det är så att vi vill ha andra utskrifter när vi kallar på metoderna. 
Rolf: Upptäckte efter arbetet med ny lånmetod var klar att han suttit o jobbat i dev. Vid försök att flytta över detta till en annan branch så stod det att det gick bra MEN ändå inte vilket var nytt så förlorade allt han gjort. Som tur var gick det rätt snabbt att skriva om allt.
Abdalle: 
___________________________________________________________________________________________________________________________
