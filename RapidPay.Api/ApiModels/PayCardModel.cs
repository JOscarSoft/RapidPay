using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Api.ApiModels
{
    public class PayCardModel
    {
        public long Number { get; set; }
        public decimal Amount { get; set; }
    }
}
