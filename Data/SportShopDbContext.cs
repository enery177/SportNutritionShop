using Microsoft.EntityFrameworkCore;
using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Data;

public class SportShopDbContext : DbContext
{
    public SportShopDbContext(DbContextOptions<SportShopDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)");

        // Связи Fluent API
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        // Seed-данные: Категории
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Протеины" },
            new Category { Id = 2, Name = "Витамины" },
            new Category { Id = 3, Name = "Аксессуары" });

        // Seed-данные: Товары
        modelBuilder.Entity<Product>().HasData(
            // --- Категория 1: Протеины ---
            new Product
            {
                Id = 1,
                Name = "Whey Gold Standard",
                Description = "Сывороточный протеин премиум-качества для эффективного роста мышц.",
                Price = 2499.00m,
                CategoryId = 1,
                ImageUrl = "https://placehold.co/300x200/2563eb/ffffff?text=Whey"
            },
            new Product
            {
                Id = 4,
                Name = "Whey Isolate Premium",
                Description = "Изолят сывороточного белка с высокой степенью очистки, без сахара и лактозы.",
                Price = 2890.00m,
                CategoryId = 1,
                ImageUrl = "https://placehold.co/300x200/2563eb/ffffff?text=Isolate"
            },

            // --- Категория 2: Витамины ---
            new Product
            {
                Id = 2,
                Name = "Omega-3 Premium",
                Description = "Рыбий жир высокой концентрации для поддержки сердца, сосудов и суставов.",
                Price = 890.00m,
                CategoryId = 2,
                ImageUrl = "https://placehold.co/300x200/10b981/ffffff?text=Omega"
            },
            new Product
            {
                Id = 5,
                Name = "Omega-3 Ultra Concentrate",
                Description = "Усиленная формула Омега-3 с повышенным содержанием активных кислот EPA и DHA.",
                Price = 1350.00m,
                CategoryId = 2,
                ImageUrl = "https://placehold.co/300x200/10b981/ffffff?text=Omega+Ultra"
            },

            // --- Категория 3: Аксессуары ---
            new Product
            {
                Id = 3,
                Name = "Шейкер Pro 500ml",
                Description = "Пластиковый шейкер с мерной сеткой для быстрого смешивания коктейлей.",
                Price = 450.00m,
                CategoryId = 3,
                ImageUrl = "https://placehold.co/300x200/f59e0b/ffffff?text=Shaker"
            },
            new Product
            {
                Id = 6,
                Name = "Smartshaker 3-в-1 600ml",
                Description = "Трехкомпонентный шейкер с дополнительными контейнерами под капсулы и порошок.",
                Price = 650.00m,
                CategoryId = 3,
                ImageUrl = "https://placehold.co/300x200/f59e0b/ffffff?text=Smart+Shaker"
            });
    }
}