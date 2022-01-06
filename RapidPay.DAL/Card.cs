using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DAL
{
    public class Card : BaseEntity
    {
        public long CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public int CVV { get; set; }
        public string ExpirationDate { get;set; }
        public decimal LimitAmount { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
