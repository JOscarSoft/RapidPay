using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DL.DTO
{
    public class CardBalanceResponse
    {
        public decimal LimitAmount { get; set; }
        public decimal Balance { get; set; }
    }
}
