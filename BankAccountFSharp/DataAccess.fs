module DataAccess

open Microsoft.FSharp.Data.TypeProviders
open BankAccount

[<Literal>]
let connectionString = "Data Source=(localdb)\mssqllocaldb;Initial Catalog=BankAccounts;Integrated Security=True"

type dbSchema = SqlDataConnection<connectionString>

let tryGetBankAccountByOwner =
  fun (db:dbSchema.ServiceTypes.SimpleDataContextTypes.BankAccounts) owner -> 
    (query { for ba in db.BankAccounts1 do 
             where (ba.Owner = owner); 
             select { 
               Id = ba.Id; 
               Owner = ba.Owner; 
               Balance = ba.Balance
             }
           } |> Seq.tryHead)

let updateBankAccount =
  fun (db:dbSchema.ServiceTypes.SimpleDataContextTypes.BankAccounts) bankAccount -> 
    (query { for ba in db.BankAccounts1 do 
             where (ba.Id = bankAccount.Id); 
             select ba 
           } |> Seq.iter(fun ba -> 
                             ba.Owner <- bankAccount.Owner; 
                             ba.Balance <- bankAccount.Balance)
     db.DataContext.SubmitChanges())
