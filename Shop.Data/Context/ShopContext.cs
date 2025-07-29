using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using Shop.Application.Extensions;
using Shop.Data.Seeders;
using Shop.Domain.Models.Roles;
using Shop.Domain.Models.Users;

namespace Shop.Data.Context
{
    public class ShopContext : IdentityDbContext<User, Role, int>
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
            
        }        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.RegisterAllEntities<IEntity>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserSeeder).Assembly);

            modelBuilder.ConfigureEnumValues();
            
            #region Spesific Query Filters

            modelBuilder.Entity<User>().HasQueryFilter(r => r.Id != 1);

            #endregion
            
        }
    }
}