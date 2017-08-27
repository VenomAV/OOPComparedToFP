module BankAccount

type BankAccount = {Id:int; Owner:string; Balance:decimal}

let withdraw = fun bankAccount (amount:decimal) -> 
  ({ bankAccount with Balance = bankAccount.Balance - amount })