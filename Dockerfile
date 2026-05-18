# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SportNutritionShop.csproj", "./"]
RUN dotnet restore "SportNutritionShop.csproj"

COPY . .
RUN dotnet build "SportNutritionShop.csproj" -c Release -o /app/build

# Публикуем
FROM build AS publish
RUN dotnet publish "SportNutritionShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный этап
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENV ConnectionStrings__DefaultConnection="Data Source=/data/sportshop.db"

USER root
RUN mkdir -p /data && chown -R app:app /data

USER app
ENTRYPOINT ["dotnet", "SportNutritionShop.dll"]