using Microsoft.EntityFrameworkCore;
using System;

namespace RapidPay.DAL
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.UserName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Password).HasMaxLength(250).IsRequired();
                e.HasData(new User { Id = 1, UserName = "admin", Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=" });
            });

            modelBuilder.Entity<Card>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.CardNumber).IsRequired();
                e.Property(e => e.CVV).IsRequired();
                e.Property(e => e.CardHolderName).IsRequired().HasMaxLength(80);
                e.Property(e => e.ExpirationDate).IsRequired().HasMaxLength(6);
            });

            modelBuilder.Entity<Payment>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.CreditCardId).IsRequired();
                e.Property(e => e.Comments).HasMaxLength(150).IsRequired();
                e.Property(e => e.Commerce).HasMaxLength(100).IsRequired();
                e.HasOne(p => p.Card)
                 .WithMany(t=> t.Payments)
                 .HasForeignKey(p => p.CreditCardId);
            });

            modelBuilder.Entity<PaymentFee>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.FeePrice).IsRequired();
                e.Property(e => e.UpdateTime).IsRequired();
                e.HasData(new PaymentFee { Id = 1, FeePrice = 3.5M, UpdateTime = DateTime.Now.AddHours(-1) });
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentFee> PaymentFees { get; set; }
    }
}
