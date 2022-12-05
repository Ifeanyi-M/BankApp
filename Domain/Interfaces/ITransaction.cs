using MYBANKAPP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.Domain.Interfaces
{
    public interface ITransaction
    {
        void InsertTransaction(long userBankAccountId, TransactionType trantype, decimal tranAmount, string description);
        void ViewTransaction();
    }
}
