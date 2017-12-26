namespace CarDealer.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Log> Logs { get; set; }
        
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CarPart>()
                .HasKey(cp => new {cp.CarId, cp.PartId});

            builder.Entity<Car>()
                .HasMany(c => c.Parts)
                .WithOne(p => p.Car)
                .HasForeignKey(cp => cp.CarId);

            builder.Entity<Part>()
                .HasMany(p => p.Cars)
                .WithOne(c => c.Part)
                .HasForeignKey(cp => cp.PartId);

            builder.Entity<Sale>()
                .HasOne<Customer>(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Entity<Sale>()
                .HasOne<Car>(s => s.Car)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .HasOne<Supplier>(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p => p.SupplierId);

            base.OnModelCreating(builder);
        }
    }
}
