using Microsoft.EntityFrameworkCore;
using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SportShopDbContext _context;

    public ProductRepository(SportShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products.Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId).ToListAsync();
    }
}