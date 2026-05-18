using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Services;

public interface ICartService
{
    Task<List<CartItem>> GetCartItemsAsync();
    Task AddToCartAsync(Product product, int quantity = 1);
    Task RemoveFromCartAsync(int productId);
    Task UpdateQuantityAsync(int productId, int quantity);
    Task ClearCartAsync();
    Task<decimal> GetTotalAmountAsync();
    int GetItemsCount();
    event Action? OnChange;
}