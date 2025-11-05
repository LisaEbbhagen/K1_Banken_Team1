# K1_Banken_Team1 
# R.A.L´s bank
## - a console-based BankApp in C#

### General info
This BankApp is a collaborative group project developed by us during the Fullstack .NET Developer program at Chas Academy. The application simulates core banking operations and is built using object-oriented principles, .NET 8, Entity Framework Core, and SQL Server LocalDB. The project follows an agile workflow guided by a prioritized backlog and iterative feedback from the teacher, who acts as the product owner. It simulates banking operations such as deposits, withdrawels, tranfers and account management. Our goal has been to learn as much as possible and work as a strong team. Our focus have been to accieve clean architecture, good user experience and maintanable code. 

### Technologies
- Language: C#
- Framework: .NET 8
- IDE: Visual Studio
- Version Control: Git (shared team repository with branching and pull requests)

### Features
To meet the project requirements, the application includes:
- Login system with username and PIN 
- Role-based views for admin and regular users
- Account overview with balance display
- Transactions: deposit, withdrawal, transfer, and open new account
- Error handling logic
  
### Architecture & Design
- Main method only handles program startup – no business logic
- SOLID principles applied across services and models
- Separation of concerns between UI, business logic, and data access
- LINQ and lambda expressions used for filtering, grouping, and transforming data
- Data structures include List, Dictionary, and HashSet, chosen for clarity and performance

### Getting started 
1. Clone the repository:
  git clone https://github.com/LisaEbbhagen/K1_Banken_Team1.git
2. Open the solution in Visual Studio
3. Ensure SQL Server LocalDB is installed
4. Run database migrations (if applicable)
5. Start the application with Ctrl + F5

### How to use
This is a console-based app. You’ll navigate through menus by typing the number (or name) of your choice and pressing Enter. 
It’s simple, fast, and designed to feel intuitive.

**User flow:**
1. Start the app by running `dotnet run` or launching it from Visual Studio.
2. At the main menu choose `1. Logga in` and enter your username and PIN when prompted.
3. After login you'll see the `UserMenu`, where you can:
   - View your accounts and balances
   - Deposit money
   - Withdraw money
   - Transfer between accounts (you´ll need both account numbers)
   - Open a new account
   - View recent or all transactions
   - Borrow money
4. Follow the on-screen instructions. Amounts are entered as numbers (e.g. `250.00`).
5. When finished choose the logout or back option to return to the main menu.

**Admin flow:**
1. From the main menu choose `2. Admin`.
2. Admin menu shows options that include:
   - List all users and accounts
   - Generate reports (e.g. largest transactions, totals)
   - Update exchange rates
   - Create new user
   - Unlock locked user accounts
             
**Common tasks — examples:**
- Deposit: Choose deposit, enter the account number, then the amount (e.g. `500`).
- Transfer: Choose transfer, enter source account number, destination account number, and amount.
- Open account: Choose the open account option and follow prompts for account type and currency.

**Troubleshooting & tips:**
- If no users exist yet, use the `Admin` menu to create a user before attempting to log in.
- Invalid inputs will trigger warning messages, re-enter values as prompted.
- Use the `Exit` option from the main menu to close the app.

### Authors

RoffeRuff42, Abdalle-Abdulkadir, LisaEbbhagen
