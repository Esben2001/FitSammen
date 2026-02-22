# syntax=docker/dockerfile:1

# -------------------------
# Build stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiér kun csproj først (bedre caching)
COPY FitSammen_API.csproj ./
RUN dotnet restore ./FitSammen_API.csproj

# Kopiér resten
COPY . ./

# Publish
RUN dotnet publish ./FitSammen_API.csproj -c Release -o /app/publish --no-restore

# -------------------------
# Runtime stage
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "FitSammen_API.dll"]