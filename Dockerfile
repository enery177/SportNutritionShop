# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY ["SportNutritionShop.csproj", "./"]
RUN dotnet restore "SportNutritionShop.csproj"

# Копируем весь исходный код и собираем
COPY . .
RUN dotnet build "SportNutritionShop.csproj" -c Release -o /app/build

# Публикуем
FROM build AS publish
RUN dotnet publish "SportNutritionShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный этап
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Явно открываем порт 7003 внутри контейнера
EXPOSE 7003

# Переменные окружения: заставляем .NET слушать именно 7003
ENV ASPNETCORE_URLS=http://+:7003
ENV ConnectionStrings__DefaultConnection="Data Source=/data/sportshop.db"

# Переключаемся на root, чтобы создать папку с правами для пользователя app
USER root
RUN mkdir -p /data && chown -R app:app /data

# Возвращаемся на безопасного пользователя app
USER app

# Запуск приложения
ENTRYPOINT ["dotnet", "SportNutritionShop.dll"]