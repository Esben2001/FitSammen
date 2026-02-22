# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiér hele repo ind i build-container
COPY . .

# Din API csproj (fra din repo struktur)
ARG PATH_TO_API_CSPROJ=FitSammen/FitSammen_API/FitSammen_API.csproj

# Restore + publish
RUN dotnet restore "$PATH_TO_API_CSPROJ"
RUN dotnet publish "$PATH_TO_API_CSPROJ" -c Release -o /app/publish --no-restore

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# API skal lytte på 8080 inde i container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Kopiér det publish'ede output
COPY --from=build /app/publish .

# Navn på DLL (kan ændres via build-arg hvis nødvendigt)
ARG API_DLL_NAME=FitSammen_API.dll
ENV API_DLL_NAME=${API_DLL_NAME}

# Start API
ENTRYPOINT ["sh", "-c", "dotnet $API_DLL_NAME"]