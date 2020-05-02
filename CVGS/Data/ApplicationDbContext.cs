using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CVGS.ViewModels;

namespace CVGS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Game> Game { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<CreditCard> CreditCard { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Friend> Friend { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<Wishlist> Wishlist { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Province> Province { get; set; }

        public DbSet<GenreUser> GenreUser { get; set; }

        public DbSet<GameGenre> GameGenre { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<CVGS.Models.GameImage> GameImage { get; set; }

        public DbSet<CVGS.ViewModels.GameStore> GameStoreViewModel { get; set; }

        public DbSet<CVGS.Models.Cart> Cart { get; set; }

        public DbSet<CVGS.Models.Checkout> Checkout { get; set; }

    }
}
