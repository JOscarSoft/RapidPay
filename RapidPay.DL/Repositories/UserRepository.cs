using Microsoft.EntityFrameworkCore;
using RapidPay.DAL;
using RapidPay.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.DL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _entities;
        public UserRepository(DataContext context) : base(context)
        {
            _entities = context.Set<User>();
        }

        public async Task<User> GetByUserName(string UserName)
        {
            return await _entities.FirstOrDefaultAsync(p => p.UserName == UserName);
        }
    }
}
