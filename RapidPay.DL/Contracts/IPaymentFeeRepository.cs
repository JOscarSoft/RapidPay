using RapidPay.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Contracts
{
    public interface IPaymentFeeRepository : IBaseRepository<PaymentFee>
    {
        Task<PaymentFee> GetPaymentFee();
    }
}
