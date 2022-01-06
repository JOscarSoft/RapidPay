using RapidPay.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Services
{
    public class PaymentFeeService : IPaymentFeeService
    {
        private readonly IPaymentFeeRepository _paymentFeeRepository;
        public PaymentFeeService(IPaymentFeeRepository paymentFeeRepository)
        {
            _paymentFeeRepository = paymentFeeRepository;
        }

        public async Task<decimal> CalculatePaymentFee()
        {
            var fee = await _paymentFeeRepository.GetPaymentFee();
            if (fee != null)
            {
                if(DateTime.Now.Subtract(fee.UpdateTime).TotalMinutes >= 60)
                {
                    var random = new Random();
                    fee.FeePrice *= Convert.ToDecimal(random.NextDouble() * 2);
                    fee.UpdateTime = DateTime.Now;

                    _paymentFeeRepository.Update(fee);
                }

                return fee.FeePrice;
            }
            else
                throw new Exception("No payment fee data found!");
        }
    }
}
