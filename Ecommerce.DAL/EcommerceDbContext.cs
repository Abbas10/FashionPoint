using Ecommerce.DAL.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL
{
    public class EcommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
                                ApplicationUserRole, IdentityUserLogin<string>,IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Refresh Tokens
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public DbSet<Unit> Units { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Order Detail
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Shopping cart
        /// </summary>
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
