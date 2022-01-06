using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Contracts
{
    public interface IPaymentFeeService
    {
        Task<decimal> CalculatePaymentFee();
    }
}
