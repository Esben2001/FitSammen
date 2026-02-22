# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy repo
COPY . .

# Restore + publish API
RUN dotnet restore "FitSammen/FitSammen_API/FitSammen_API.csproj" \
 && dotnet publish "FitSammen/FitSammen_API/FitSammen_API.csproj" -c Release -o /app/publish --no-restore

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FitSammen_API.dll"]