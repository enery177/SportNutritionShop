using Microsoft.EntityFrameworkCore;
using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SportShopDbContext _context;

    public OrderRepository(SportShopDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}