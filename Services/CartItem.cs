using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Services;

public class CartItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}