﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.Domain.Interfaces
{
    public interface IUserAccountMeasures
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
    }
}