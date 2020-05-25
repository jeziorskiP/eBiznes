using DataContext.Model;
using DataContext.Model.Shopping_Cart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataContext
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> Products { get; set; }
        // public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<PackingNote> PackingNotes { get; set; }
        public DbSet<PackingNoteLine> PackingNoteLines { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<DeliveryNoteLine> DeliveryNoteLines { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .HasOne(a => a.Invoice)
                .WithOne(b => b.Order)
                .HasForeignKey<Invoice>(b => b.OrderId);

            /*
            builder.Entity<Order>()
                .HasOne(a => a.PackingNote)
                .WithOne(b => b.Order)
                .HasForeignKey<PackingNote>(b => b.OrderId);
                */
            /*
        builder.Entity<Order>()
            .HasOne(a => a.PackingNoteLine)
            .WithOne(b => b.Order)
            .HasForeignKey<PackingNoteLine>(b => b.OrderId);
            */
            builder.Entity<Order>()
                .HasOne(a => a.DeliveryNote)
                .WithOne(b => b.Order)
                .HasForeignKey<DeliveryNote>(b => b.OrderId);

            base.OnModelCreating(builder);
        }

    }
}
