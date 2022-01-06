using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DAL
{
    public class PaymentFee : BaseEntity
    {
        public decimal FeePrice { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
