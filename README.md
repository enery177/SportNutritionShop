# SportNutritionShop

Web-сайт спортивного питания и аксессуаров на **.NET 8 + Blazor Server**.

## 🚀 Функционал

- Каталог товаров с фильтрацией и поиском
- Корзина (хранение в памяти сессии)
- Оформление заказа без авторизации
- Валидация форм через FluentValidation
- Healthchecks (/health)
- Контейнеризация через Docker

## 🛠️ Технологии

- .NET 8
- Blazor Server
- Entity Framework Core (Code First, SQLite)
- FluentValidation
- Docker + Docker Compose
- Git + GitHub

## 📦 Установка и запуск

### Локально

```bash
# Клонируйте репозиторий
git clone https://github.com/enery177/SportNutritionShop.git
cd SportNutritionShop

# Восстановите зависимости
dotnet restore

# Примените миграции
dotnet ef database update

# Запустите приложение
dotnet run --urls http://localhost:7003