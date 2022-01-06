using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DL.DTO
{
    public class PaymentResponse
    {
        public int paymentId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalAmount 
        { 
            get
            {
                return Amount + Fee;
            }
        }
    }
}
