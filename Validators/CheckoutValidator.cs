using FluentValidation;
using SportNutritionShop.Data.Entities;

namespace SportNutritionShop.Validators;

public class CheckoutValidator : AbstractValidator<Customer>
{
    public CheckoutValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Введите ФИО")
            .MinimumLength(3).WithMessage("ФИО должно содержать не менее 3 символов");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Введите телефон")
            .Matches(@"^[\d\s\-\+\(\)]+$").WithMessage("Неверный формат телефона");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Введите email")
            .EmailAddress().WithMessage("Неверный формат email");

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty().WithMessage("Введите адрес доставки")
            .MinimumLength(10).WithMessage("Адрес слишком короткий");
    }
}