using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SportNutritionShop.Components;
using SportNutritionShop.Data;
using SportNutritionShop.Data.Repositories;
using SportNutritionShop.Services;
using SportNutritionShop.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 1. DI и EF Core SQLite
builder.Services.AddDbContext<SportShopDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Репозитории и сервисы
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartService, CartService>();

// 3. Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.EnableDetailedErrors = true;
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    });

// 4. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CheckoutValidator>();
//builder.Services.AddFluentValidationAutoValidation();

// 5. Healthchecks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<SportShopDbContext>("database");



var app = builder.Build();

// Pipeline конфигурация
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Health check endpoint
app.MapHealthChecks("/health");

// Применяем миграции при запуске
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SportShopDbContext>();
    db.Database.Migrate();
}

app.Run();

public partial class Program { }