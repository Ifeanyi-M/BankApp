using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.Domain.Entities
{
    public class InternalTransfer
    {
        public decimal TransferAmount { get; set; }
        public long ReceiverBankAccountNumber  { get; set; }
        public string ReceiverBankAccountName { get; set; }
    }
}
