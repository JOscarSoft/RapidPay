using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.DAL
{
    public class Payment : BaseEntity
    {
        public int CreditCardId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Commerce { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comments { get; set; }

        public virtual Card Card { get; set; }
    }
}
