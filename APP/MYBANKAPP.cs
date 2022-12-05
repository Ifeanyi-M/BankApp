

using BetterConsoleTables;
using ConsoleTables;
using MYBANKAPP.Domain.Entities;
using MYBANKAPP.Domain.Enums;
using MYBANKAPP.Domain.Interfaces;
using MYBANKAPP.UI;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Validator = MYBANKAPP.UI.Validator;

public class MyBankApp:IUserLogin, IUserAccountMeasures, ITransaction
{
    private List<UserAccount> userAccountList;
    private UserAccount chosenAccount;
    private List<Transaction> transactionList;
    private const decimal minimalResidue = 1000;
    private readonly APPMIRROR mirror;
    public MyBankApp()
    {
        mirror = new APPMIRROR();   
    }
    

    public void Run()
    {
        var helpers = new APPMIRRORHelpers();
        APPMIRROR.Welcome();
        checkUserNameAndPassword();
        helpers.WelcomeCustomer(chosenAccount.FullName);
        while (true)
        {
            APPMIRROR.giveAppMenu();
            processMenuOption();
        }
    }

    public void checkUserNameAndPassword()
    {
        bool isCorrectLogin = false;
        while (isCorrectLogin == false)
        {
            UserAccount accountInput = APPMIRROR.UserLoginForm();
            APPMIRROR.LoginProgress();
            foreach(UserAccount account in userAccountList)
            {
                chosenAccount = account;
                if (accountInput.CardNumber.Equals(chosenAccount.CardNumber))
                {
                    chosenAccount.TotaLogin++;

                    if (accountInput.CardPin.Equals(chosenAccount.CardPin))
                    {
                        chosenAccount = account;

                        if(chosenAccount.IsLockedOut || chosenAccount.TotaLogin > 3) 
                        { 
                         // print a lock message.
                          APPMIRROR.PrintLockScreen();

                        }
                        else
                        {
                            chosenAccount.TotaLogin = 0;
                            isCorrectLogin = true;
                            break;
                        }
                        


                    }

                }
                if (isCorrectLogin == false)
                {
                    Service.PrintMessage("\n Either your card number or PIN is invalid", false);
                    chosenAccount.IsLockedOut = chosenAccount.TotaLogin == 3;
                    if (chosenAccount.IsLockedOut)
                    {
                        APPMIRROR.PrintLockScreen();
                    }
                }
                Console.Clear();
            }
        }
        
    }
    private void processMenuOption()
    {
       switch(Validator.Transform<int>("an option: "))
        {
            case (int) AppMenu.CheckBalance:
                CheckBalance();
                break;

            case (int)AppMenu.PlaceDeposit:
                PlaceDeposit();
                break;

            case (int)AppMenu.MakeWithdrawal:
                MakeWithdrawal();
                break;

            case (int)AppMenu.LocalTransfer:
                var internalTransfer = mirror.InternalTransferForm();
                ProcessInternalTransfer(internalTransfer);
                break;

            case (int)AppMenu.ViewTransactions:
                ViewTransaction();
                break;

            case (int)AppMenu.Logout:
                APPMIRROR.LogOutProgress();
                Service.PrintMessage("You have successfully logged out of the system");
                Run();
                break;

            default:
                Service.PrintMessage("This option is invalid", false);
                break;




        }
    }
  


    public void InitializeData()
    {
        userAccountList = new List<UserAccount>
        {
         new UserAccount
         {Id = 1, FullName = "Ifeanyi Nzube Mojekwu", AccountNumber = 2063455422,
             CardNumber = 567890,CardPin = 056290, AccountBalance = 431000000.00M, AccountType = "Savings", IsLockedOut = false 
         },
         new UserAccount
         {
             Id = 2, FullName = "Somto Amala Mojekwu", AccountNumber = 5321908988,
             CardNumber = 890123,CardPin = 0105667, AccountBalance = 31300000.00M, AccountType = "Current", IsLockedOut = false  
         },
            new UserAccount
            {
                Id = 3, FullName = "Josephine Chima Mojekwu", AccountNumber = 8543986100,
             CardNumber = 123456,CardPin = 291044, AccountBalance = 6000000.00M, AccountType = "Savings", IsLockedOut = true 
            }
        };
        transactionList = new List<Transaction>();
    }

    public void CheckBalance()
    {
        Service.PrintMessage($"Account Balance is:{Service.CurrencyForm(chosenAccount.AccountBalance)}");
    }

    public void PlaceDeposit()
    {
        Console.WriteLine("\nOnly multiples of 500 and 1000 pounds allowed.\n");
        var amounttransaction = Validator.Transform<int>($"amount {APPMIRROR.cur}");
        decimal newAccountBalance = 0;

        //simulate count
        Console.WriteLine("\nCounting.");
        Service.PrintDotAnime();
        Console.WriteLine("");

        //some guard clause
        if(amounttransaction <= 0)
        {
           
            return   ;
        }
        //Service.PrintMessage("Please, the amount needs to be greater than zero. Retry!", false);
        if (amounttransaction % 500 != 0)
        {
           
            return  ;
        }
       // Service.PrintMessage($"Please enter your deposit in multiples of 500 or 1000. Retry!", false);
        if (PeekBankBillsCount(amounttransaction) == false)
        {
           
            return  ;
        }
        //bind transaction details to transaction object
        InsertTransaction(chosenAccount.Id, TransactionType.Deposit, amounttransaction, "Deposit");
        //update account balance
        chosenAccount.AccountBalance += amounttransaction;
        
        //print success message
        Service.PrintMessage($"Your deposit of {Service.CurrencyForm(amounttransaction)} was successful.", true);



    }

    

    public void MakeWithdrawal()
    {
        var amounttransaction = 0;
        var pickAmount = new APPMIRRORHelpers();
       
        int chosenAmount = pickAmount.PickAmount();
        if(chosenAmount == -1)
        {
            MakeWithdrawal();
            return;
        }
        else if(chosenAmount != 0)   
        {
            amounttransaction = chosenAmount;
        }
        else
        {
            amounttransaction = Validator.Transform<int>($"amount {APPMIRROR.cur}");
        }

        //INPUT VALIDATION
        if (amounttransaction <= 0)
        {
           // Service.PrintMessage("Amount needs to be greater than zero. Please retry!", false);
            return;
        }
        if(amounttransaction % 500 != 0)
        {
           // Service.PrintMessage("You can only withdraw amount in multiples of 500 or 1000 pounds. Retry!", false);
            return;
        }
        //Business Logic Validation
        if (amounttransaction > chosenAccount.AccountBalance)
        {
          //  Service.PrintMessage($"Insufficient Funds. Your balance is lower than {Service.CurrencyForm(chosenAmount)}", false);
            return;
        }
        if((chosenAccount.AccountBalance - amounttransaction) < minimalResidue)
        {
           // Service.PrintMessage($"You cannot make this withdrawal. Your account needs to have a minimum of {Service.CurrencyForm(minimalResidue)}", false);
            return;
        }
        
        //bind withdrawl details to transaction object
        InsertTransaction(chosenAccount.Id, TransactionType.Withdrawal, -amounttransaction, "Withdrawal");
        //update account balance
        chosenAccount.AccountBalance -= amounttransaction;
        
        
        //success message
        Service.PrintMessage($"You have successfully made a withdrawal of {Service.CurrencyForm(amounttransaction)}.", true);


    }

    private bool PeekBankBillsCount(int cash)
    {
        int thousandBillsCount = cash / 1000;
        int fiveHundredBillsCount = (cash % 1000)/500;

        Console.WriteLine("\nSummary");
        Console.WriteLine("-------"); 
        Console.WriteLine($"{APPMIRROR.cur}1000 X {thousandBillsCount} = {1000 * thousandBillsCount }");
        Console.WriteLine($"{APPMIRROR.cur}500  X {fiveHundredBillsCount} = {500 * fiveHundredBillsCount }");
        Console.WriteLine($"Total Amount: {Service.CurrencyForm(cash)}\n\n");

        int action = Validator.Transform<int>("Press 1 to confirm");
        return action.Equals(1);

    }

    public void InsertTransaction(long userBankAccountId, TransactionType trantype, decimal tranAmount, string description)
    {
        //create a new transaction object
        var transaction = new Transaction()
        {
            transactionId = Service.GetTransactionId(),
            UserBankAccountId = userBankAccountId,
            TransactionDate = DateTime.Now,
            TransactionType = trantype,
            TransactionAmount = tranAmount,
            Description = description
            
        };
        //add transaction object to the list
        transactionList.Add(transaction);   

    }

    public void ViewTransaction()
    {
        var filteredTransactionList =  transactionList.Where(t => t.UserBankAccountId == chosenAccount.Id).ToList();
        // check if there's a transaction
        if(filteredTransactionList.Count <= 0)
        {
             Service.PrintMessage("You haven't made any transactions yet.", false); 
            
            
        }
        else
        {
            Console.WriteLine("Account Number : {0}, Account Name : {1}", chosenAccount.AccountNumber, chosenAccount.FullName);
            
            var newTable = new ConsoleTable("Transaction Date", "Description", "Transaction Amount", "Account Balance");
            foreach(var trans in filteredTransactionList)
            {
                newTable.AddRow( trans.TransactionDate, trans.Description, trans.TransactionAmount, chosenAccount.AccountBalance);
            }
            newTable.Options.EnableCount = false;
            newTable.Write();
            Service.PrintMessage($"You have made {filteredTransactionList.Count} transaction(s)", true);
        }
    }
    private void ProcessInternalTransfer(InternalTransfer internalTransfer)
    {
        if(internalTransfer.TransferAmount <= 0)
        {
            Service.PrintMessage("Amount has to be more than zero. Retry!", false);
            return;
        } 
        //check sender's balance
        if (internalTransfer.TransferAmount > chosenAccount.AccountBalance)
        {
            Service.PrintMessage($"Unable to transfer. Transfer Amount of {Service.CurrencyForm(internalTransfer.TransferAmount)} exceeds your current account balance.", false);
            return;
        }
        //check the minimmalresidue
        if((chosenAccount.AccountBalance - internalTransfer.TransferAmount) < minimalResidue)
        {
            Service.PrintMessage($"Unable to transfer. Your account needs to have a minimum of {Service.CurrencyForm(minimalResidue)}", false);
            return;
        }
        //check if receiver's bank account number is valid
        var chosenReceiverBankAccount = (from userAcc in userAccountList where userAcc.AccountNumber == internalTransfer.ReceiverBankAccountNumber select userAcc).FirstOrDefault();
        if(chosenReceiverBankAccount == null)
        {
            Service.PrintMessage("Unable to Transfer. The receipient's bank account number does not exist", false);
            return;
            

        }
        //check receiver's name
        if(chosenReceiverBankAccount.FullName != internalTransfer.ReceiverBankAccountName)
        {
            Service.PrintMessage("Unable to Transfer. Receipient's bank account name does not exist", false);
            return;
        }
        //add transaction to transaction record - sender
        InsertTransaction(chosenAccount.Id, TransactionType.Transfer, (int)-internalTransfer.TransferAmount, "Transferred" +
            $"to {chosenReceiverBankAccount.AccountNumber} ({chosenReceiverBankAccount.FullName})");
        // update sender's account
        chosenAccount.AccountBalance -= internalTransfer.TransferAmount;
        //add transaction record - receiver
        InsertTransaction(chosenReceiverBankAccount.Id, TransactionType.Transfer, (int)internalTransfer.TransferAmount, "Transferred from" +
            $"{chosenAccount.AccountNumber} ({chosenAccount.FullName})");
        //update receiver account balance
        chosenReceiverBankAccount.AccountBalance += internalTransfer.TransferAmount;   
        Service.PrintMessage($"You have successfully transferred {Service.CurrencyForm(internalTransfer.TransferAmount)} to {internalTransfer.ReceiverBankAccountName}", true);



    }
}