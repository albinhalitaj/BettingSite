using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Bets> Bets { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result =  await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BettingSite;User ID=sa;Password=SuperS3cur3Pa55word;Integrated Security=False;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        {
            var result = base.Entry(entity);
            return result;
        }

        public override EntityEntry Update(object entity)
        {
            var result = base.Update(entity);
            return result;
        }
    }
}