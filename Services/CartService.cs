using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Services;

public class CartService : ICartService
{
    private readonly List<CartItem> _cartItems = new();

    public CartService()
    {
        Console.WriteLine("✅ CartService создан (in-memory)");
    }

    public event Action? OnChange;

    public Task<List<CartItem>> GetCartItemsAsync()
    {
        return Task.FromResult(_cartItems);
    }

    public int GetItemsCount()
    {
        return _cartItems.Sum(c => c.Quantity);
    }

    public Task AddToCartAsync(Product product, int quantity = 1)
    {
        Console.WriteLine($"🛒 AddToCartAsync: {product.Name}");

        var existingItem = _cartItems.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            Console.WriteLine($"🔄 Обновлен: {existingItem.Quantity} шт.");
        }
        else
        {
            _cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            Console.WriteLine("➕ Добавлен новый товар");
        }

        Console.WriteLine($"📦 Всего товаров в корзине: {_cartItems.Count}");
        NotifyStateChanged();
        return Task.CompletedTask;
    }

    public Task RemoveFromCartAsync(int productId)
    {
        var item = _cartItems.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            _cartItems.Remove(item);
            NotifyStateChanged();
        }
        return Task.CompletedTask;
    }

    public Task UpdateQuantityAsync(int productId, int quantity)
    {
        var item = _cartItems.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                _cartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            NotifyStateChanged();
        }
        return Task.CompletedTask;
    }

    public Task ClearCartAsync()
    {
        _cartItems.Clear();
        NotifyStateChanged();
        return Task.CompletedTask;
    }

    public Task<decimal> GetTotalAmountAsync()
    {
        var total = _cartItems.Sum(c => c.Product.Price * c.Quantity);
        return Task.FromResult(total);
    }

    private void NotifyStateChanged()
    {
        Console.WriteLine("🔔 NotifyStateChanged");
        OnChange?.Invoke();
    }
}