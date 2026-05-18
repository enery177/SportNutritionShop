using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Data.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
}