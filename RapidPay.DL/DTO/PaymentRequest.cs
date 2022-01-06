using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DL.DTO
{
    public class PaymentRequest
    {
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public string ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public string Commerce { get; set; }
        public string Comments { get; set; }
    }
}
