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
                int updateTimeHours = (int)(DateTime.Now.Subtract(fee.UpdateTime).TotalMinutes / 60);
                if (updateTimeHours >= 1)
                {
                    var random = new Random();

                    //Recalculate the Fee for every hour that passed
                    for (int i = 0; i < updateTimeHours; i++)
                    {
                        fee.FeePrice *= Math.Round(Convert.ToDecimal(random.NextDouble() * 2), 2);
                    }

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
