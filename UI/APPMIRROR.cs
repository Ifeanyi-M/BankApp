using MYBANKAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MYBANKAPP.UI
{
    public  class APPMIRROR
    {
        internal const string cur = " ";
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "My Bank App";
            Console.ForegroundColor = ConsoleColor.DarkCyan;          

            Console.WriteLine("\n\n------------ You are welcome to My Bank App! ------------\n");
            Console.WriteLine("Please insert your ATM card");
            Console.WriteLine("Please note : This ATM machine will accept and validate a virtual card. Read your card number and validate.");

            Service.HitEnterKey();
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempuserAccount = new UserAccount();

            tempuserAccount.CardNumber = Validator.Transform<long>("your card number");
            tempuserAccount.CardPin = Convert.ToInt32(Service.SecretInput("Enter your PIN"));
            return tempuserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nAnalyzing card number and PIN...");
            Service.PrintDotAnime();
        }
        internal static void PrintLockScreen()
        {
            Console.Clear();
            Service.PrintMessage("Your account is locked. Please head to the nearest branch to unlock it.", true);
            Service.HitEnterKey();
            Environment.Exit(1);

        }

        internal static void giveAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-------My Bank App Menu-------");
            Console.WriteLine(":                            :");
            Console.WriteLine("1. Account Balance           :");
            Console.WriteLine("2. Money Deposit             :");
            Console.WriteLine("3. Withdrawal                :");
            Console.WriteLine("4. Transfer                  :");
            Console.WriteLine("5. Transactions              :");
            Console.WriteLine("6. Logout                    :");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("We're grateful to you for banking with us.");
            Service.PrintDotAnime();
            Console.Clear();
        }
       
        internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.ReceiverBankAccountNumber = Validator.Transform<long>("receiver's account number:");
            internalTransfer.TransferAmount = Validator.Transform<decimal>($"amount {cur}");
            internalTransfer.ReceiverBankAccountName = Service.GetUserDeets("receiver's name:");
            return internalTransfer;
        }
    }
}
