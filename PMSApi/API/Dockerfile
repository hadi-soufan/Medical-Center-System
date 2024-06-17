## Build stage
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#
## Copy and restore projects
#COPY ["API/API.csproj", "API/"]
#COPY ["../Application/Application.csproj", "Application/"]
#COPY ["../Domain/Domain.csproj", "Domain/"]
#COPY ["../Persistence/Persistence.csproj", "Persistence/"]
#COPY ["../Infrastructure/Infrastructure.csproj", "Infrastructure/"]
#RUN dotnet restore "API/API.csproj"
#
## Copy everything else and build
#COPY . .
#WORKDIR "/src/API"
#RUN dotnet build "API.csproj" -c Release -o /app/build
#
## Publish stage
#FROM build AS publish
#RUN dotnet publish "API.csproj" -c Release -o /app/publish
#
## Final stage
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#COPY API/.env .
#COPY API/appsettings.json .
#
#ENV ConnectionStrings__DefaultConnection="Server=postgres;Port=5432;Database=PMS;User Id=hatan;Password=rfayum159;"
#ENV TokenKey=KYHwK3FwWb2sE9V4xLQ8B3pDm5nG8Rj1tE7fW4bN5eP2qR5tM7yH9xW3pU7kY2hV4wS6zL7cF8bD4pV5cK7jX8sL3eF5xN4gU2wD8tK2sW8jX9sD2gY6vA9zD2nE4vW7cX5vH3jQ9mD8sL6nF2kC4pW7zF2kE8mY5nS9bA6zS2bV7kX8pZ9xP3cW4bT9mZ8lH9kX6vA9zD3nF4kX7jZ8xP3cW4bN5eP2qR5tM7yH9xW3pU7kY2hV4wS6zL7cF8bD4pV5cK7jX8sL3eF5xN4gU2wD8tK2sW8jX9sD2gY6vA9zD2nE4vW
#ENTRYPOINT ["dotnet", "API.dll"]
#