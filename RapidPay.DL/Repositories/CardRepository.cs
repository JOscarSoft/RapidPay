using Microsoft.EntityFrameworkCore;
using RapidPay.DAL;
using RapidPay.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly DbSet<Card> _entities;
        public CardRepository(DataContext context) : base(context)
        {
            _entities = context.Set<Card>();
        }

        public async Task<Card> GetByCardNumber(long number)
        {
            return await _entities.FirstOrDefaultAsync(p => p.CardNumber == number);
        }
    }
}
