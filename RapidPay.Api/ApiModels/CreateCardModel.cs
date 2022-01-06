using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Api.ApiModels
{
    public class CreateCardModel
    {
        public string HolderName { get; set; }
        public decimal LimitAmount { get; set; }
    }
}
