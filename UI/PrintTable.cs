using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.UI
{
    public static class PrintTable
    {
        public static void PrintHeader()
        {
            Console.WriteLine("|-------------------------" + 
        "-------------------------|");
        }

        public static void PrintHeadColumns()
        {
            Console.WriteLine("| {0,-9} | {1,-9}| {2,-9} | {3,-9} |", "Transaction Date", "Description",
                "Transaction Amount", "Account Balance");
        }

        public static void PrintHeadPockets()
        {
            Console.WriteLine("|-------------------------" + 
            "-----------------------|");
        }
    }
}
