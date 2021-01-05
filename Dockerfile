#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY ["MisFinder/MisFinder.csproj", "MisFinder/"]
COPY ["MisFinder.Domain/MisFinder.Domain.csproj", "MisFinder.Domain/"]
COPY ["MisFinder.Data/MisFinder.Data.csproj", "MisFinder.Data/"]
RUN dotnet restore "MisFinder/MisFinder.csproj"
COPY . .
WORKDIR "/app/MisFinder"

#RUN dotnet build "MisFinder.csproj" -c Release -o /app/build

#FROM build AS publish
RUN dotnet publish "MisFinder.csproj" -c Release -o /app/publish

FROM base AS final
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "MisFinder.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MisFinder.dll