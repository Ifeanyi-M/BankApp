using MYBANKAPP.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.APP
{
    internal class Entry
    {
        static void Main(string[] args)
        {
           
            MyBankApp bankApp = new MyBankApp();
            bankApp.InitializeData();
            bankApp.Run();
           

            Service.HitEnterKey();
            
        }
    }
}
