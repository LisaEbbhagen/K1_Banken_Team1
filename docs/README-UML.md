#UML-sketch 
*A User can have multiple Accounts (1 to ). 
*An Account can have multiple Transactions (1 to ). 
*Each Transaction is linked to exactly one Account.

+---------------------------------------------------------------+
|                            Bank                               |
+---------------------------------------------------------------+
| - ExchangeRates : Dictionary<string, decimal>                |
| - users : List<User>                                         |
| - accounts : Dictionary<string, Account>                     |
| - transactions : List<Transaction>                           |
+---------------------------------------------------------------+
| + AddUser(user: User) : void                                 |
| + CreateUser() : void                                        |
| + OpenAccount(user: User, accountNumber: string,             |
|     accountType?: string, currency?: string) : void          |
| + AddNewAccount(user: User) : void                           |
| + FindAccount(accountNumber: string, user?: User) : Account  |
| + ExecuteTransaction(type: string, currentUser?: User) : bool|
| + ProcessPendingTransactions(verbose?: bool) : void          |
| + GetAllTransactions() : List<Transaction>                   |
| + Transactions(currentUser: User) : void                     |
| + ListAllAccounts() : void                                   |
| + ShowAllUsers() : void                                      |
| + PrintAccountsWithPositivBalance() : void                   |
| + PrintTotalBalanceAll() : void                              |
| + ShowThreeBiggestTransactions() : void                      |
| + ShowBiggestTransactionPerUser() : void                     |
| + ShowUserWithMostTransactions() : void                      |
| + SearchAccount() : void                                     |
| + ShowAllTransactions(user: User) : void                     |
| + ShowAllMyAccountsAndMoney(user: User) : void               |
| + LoanMoney(user: User) : void                               |
| + UpdateExchangeRates() : void                               |
| + UnLockUserMenu() : void                                    |
| + LoginUser() : User                                         |
| + Pause() : void                                             |
+---------------------------------------------------------------+

manages 1..* 
    |
    v

+---------------------------------------------+
|                  User                       |
+---------------------------------------------+
| - Id : string                               |
| - Name : string                             |
| - Pin : string                              |
| - IsAdmin : bool                            |
| - IsLocked : bool                           |
| - Accounts : HashSet<Account>               |
+---------------------------------------------+
| + AddAccount(account: Account) : void       |
+---------------------------------------------+


owns 1..*
    |
    v
	
+--------------------------------------------------------+
|                      Account                           |
+--------------------------------------------------------+
| - AccountNumber : string                               |
| - Balance : decimal                                    |
| - Owner : User                                         |
| - Currency : string                                    |
| - Transactions : IReadOnlyList<Transaction>            |
+--------------------------------------------------------+
| + Deposit(amount: decimal) : bool                      |
| + Withdraw(amount: decimal) : bool                     |
+--------------------------------------------------------+

logs 1..*
    |
    v
	
+---------------------------------------------+
|               Transaction                   |
+---------------------------------------------+
| - Id : string                               |
| - AccountNumber : string                    |
| - ToAccountNumber : string?                 |
| - Amount : decimal                          |
| - Timestamp : DateTime                      |
| - Type : string                             |
| - Status : string                           |
| - BalanceAfter : decimal                    |
+---------------------------------------------+
| + (constructors)                            |
+---------------------------------------------+
       
