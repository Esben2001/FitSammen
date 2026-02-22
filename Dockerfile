# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiér alt (enkelt og ok til start)
COPY . .

# TODO: ret stien til din API .csproj (eksempel)
# fx: FitSammen/FitSammen.Api/FitSammen.Api.csproj
ARG PATH_TO_API_CSPROJ=FitSammen.Api/FitSammen.Api.csproj

RUN dotnet restore "$PATH_TO_API_CSPROJ"
RUN dotnet publish "$PATH_TO_API_CSPROJ" -c Release -o /app/publish

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# ASP.NET lytter typisk på 8080 i container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

# TODO: ret til navnet på din API dll (eksempel)
# fx: FitSammen.Api.dll
ARG API_DLL_NAME=FitSammen.Api.dll
ENTRYPOINT ["dotnet", "FitSammen_API.dll"]