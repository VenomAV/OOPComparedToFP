module Program

open System
open BankAccount
open DataAccess

let printConfirmWithdraw = fun bankAccount -> 
  printf "%s new balance will be %M. Confirm operation? [y/n] " 
    bankAccount.Owner bankAccount.Balance

let confirmWithdraw = 
  fun bankAccount amount -> amount |> withdraw bankAccount |> printConfirmWithdraw
                            Console.ReadLine()

let withdrawFromAccount = fun db bankAccount -> 
  (printfn "%s has %M$" bankAccount.Owner bankAccount.Balance
   printf "How much you want to withdraw? "
   let amount = Console.ReadLine() |> decimal
   match confirmWithdraw bankAccount amount with
     | "y" -> amount |> withdraw bankAccount |> updateBankAccount db
     | _ -> ())
  
let tryWithdrawFromAccount = fun db ->
  printf "Type owner (or 'q' to exit): "
  match Console.ReadLine() with
    | "q" -> false
    | owner -> (match tryGetBankAccountByOwner db owner with
                  | None -> ()
                  | Some bankAccount -> withdrawFromAccount db bankAccount
                true)

[<EntryPoint>]
let main argv = 
  let db = dbSchema.GetDataContext()
  while tryWithdrawFromAccount(db) do ()
  0