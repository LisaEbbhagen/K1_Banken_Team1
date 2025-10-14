#UML-sketch *A User can have multiple Accounts (1 to ). *An Account can have multiple Transactions (1 to ). *Each Transaction is linked to exactly one Account.


+--------------------------------------------------------------------+
|        Bank                                                        |
+--------------------------------------------------------------------+
| - users : List<User>						                                   |    
| - accounts : Dictionary<string, Account>			                     |
| - transactions : List<Transaction>				                         |
| - admins : List<Admin>				    	                               |//Lägg till		     
+--------------------------------------------------------------------+
| + AddUser(user : User) : void					                               | 
| + OpenAccount(user : User, accountNumber : string) : void            |
| + FindAccount(accountNumber : string) : Account		                   |
| + Transfer(from : string, to : string, amount : decimal) : bool      |
| + PrintAccountsWithPositiveBalance() : void			                     |
| + ThreeBiggestAmount() : List<Transaction>			                     |
| + ShowBalance() : void					                                     |    
| + PrintTotalBalanceAll() : void				                               |
| + LoginAsAdmin(id, pin) : Admin		     		                           |//Lägg till?
| + Login(userId : string, pin : string) : User		                     |//bryt ut befintlig logik från program o skapa metod för login?	     
+--------------------------------------------------------------------+

        | manages
        v

+----------------------------------------+
|        Admin                           |//lägg till?
+----------------------------------------+
| - id : string                          |//lägg till?
| - pin : string			                   |//lägg till?
+----------------------------------------+
| + LoginAdmin : bool 			             |//lägg till?
| + ShowAdminMenu() : void	             |//lägg till?
+----------------------------------------+
	
	| manages
        v
	
+----------------------------------------+
|        User                            |
+----------------------------------------+
| - id : string                          |
| - name : string                        |
| - pin : string			                   |
| - accounts : HashSet<Account>		       |
+----------------------------------------+
| + AddAccount(account : Account) : void |
| + GetAccounts() : List<Account>        | //lägg till?
+----------------------------------------+

        | owns
        v

+-------------------------------------+
|      Account                        |
+-------------------------------------+
| - accountNumber : string  	        |
| - balance : decimal		              |
| - owner : User		                  |
+-------------------------------------+
| + Deposit(amount : decimal) : bool  |
| + Withdraw(amount : decimal) : bool |
| + PrintTransactions() : void        |
| + GetBalance() : decimal	          | //lägg till? 
+-------------------------------------+

        | logs
        v

+----------------------+
|     Transaction      |
+----------------------+
| - id : string	       |
| - acountNumber : decimal   
| - amount : decimal   |
| - timestamp : DateTime    
| - type : string      |
+----------------------+
| + ToString() : string| //Lägg till? 
+----------------------+
