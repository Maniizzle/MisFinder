FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app

#COPY *.sln .
COPY ["MisFinder/MisFinder.csproj", "/MisFinder/"]
COPY ["MisFinder.Domain/MisFinder.Domain.csproj", "/MisFinder.Domain/"]
COPY ["MisFinder.Data/MisFinder.Data.csproj", "/MisFinder.Data/"]
Run dotnet restore /MisFinder/MisFinder.csproj

COPY . .
Run dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MisFinder.dll