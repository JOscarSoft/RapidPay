using RapidPay.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Contracts
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<Card> GetByCardNumber(long number);
    }
}
