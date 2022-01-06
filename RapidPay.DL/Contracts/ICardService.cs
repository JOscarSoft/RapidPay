using RapidPay.DAL;
using RapidPay.DL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Contracts
{
    public interface ICardService
    {
        Task<CardBalanceResponse> GetCardBalance(long number);
        Card CreateCard(string holderName, decimal limitAmount);
        Task PayCard(long number, decimal Amount);
        Task<PaymentResponse> NewPayment(PaymentRequest paymentRequest);
    }
}
