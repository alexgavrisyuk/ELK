using ContractManager.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregatesModels.ContractAggregate;
using OrderService.Domain.AggregatesModels.DishAggregate;
using OrderService.Domain.AggregatesModels.LaneAggregate;
using OrderService.Domain.AggregatesModels.OrderAggregate;
using OrderService.Domain.AggregatesModels.RegionAggregate;
using OrderService.Domain.AggregatesModels.TariffAggregate;
using OrderService.Domain.AggregatesModels.TariffExtra;
using OrderService.Infrastructure.EntityConfigurations;

namespace OrderService.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());

            modelBuilder.ApplyConfiguration(new DishTypeConfiguration());
            
            modelBuilder.ApplyConfiguration(new OrderItemTypeConfiguration());
        
            
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
          
            modelBuilder.ApplyConfiguration(new LaneConfiguration());
            
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            
            modelBuilder.ApplyConfiguration(new TariffConfiguration());
            
            modelBuilder.ApplyConfiguration(new TariffExtraConfiguration());
        }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        
        
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Lane> Lanes { get; set; }

        public DbSet<Region> Regions { get; set; }
        
        public DbSet<Tariff> Tariffs { get; set; }
       
        public DbSet<TariffExtra> TariffExtras { get; set; }
        
    }
}