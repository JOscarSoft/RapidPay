using Microsoft.EntityFrameworkCore;
using RapidPay.DAL;
using RapidPay.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Repositories
{
    public class PaymentFeeRepository : IPaymentFeeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<PaymentFee> _entities;
        public PaymentFeeRepository(DbContextOptions<DataContext> dbContextOptions)
        {
            _context = new DataContext(dbContextOptions);
            _entities = _context.Set<PaymentFee>();
        }

        public async Task<PaymentFee> GetPaymentFee()
        {
            return await _entities.FirstOrDefaultAsync();
        }

        public void Update(PaymentFee entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public int Add(PaymentFee entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentFee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PaymentFee> GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
