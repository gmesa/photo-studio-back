#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PhotoStudio.WebApi/PhotoStudio.WebApi.csproj", "PhotoStudio.WebApi/"]
COPY ["PhotoStudio.Application/PhotoStudio.Application.csproj", "PhotoStudio.Application/"]
COPY ["PhotoStudio.Domain/PhotoStudio.Domain.csproj", "PhotoStudio.Domain/"]
COPY ["PhotoStudio.ServicesDTO/PhotoStudio.ServicesDTO.csproj", "PhotoStudio.ServicesDTO/"]
COPY ["PhotoStudio.Infrastructure.Commons/PhotoStudio.Infrastructure.Commons.csproj", "PhotoStudio.Infrastructure.Commons/"]
COPY ["PhotoStudio.Infrastructure.Data/PhotoStudio.Infrastructure.Data.csproj", "PhotoStudio.Infrastructure.Data/"]
RUN dotnet restore "PhotoStudio.WebApi/PhotoStudio.WebApi.csproj"
COPY . .
WORKDIR "/src/PhotoStudio.WebApi"
RUN dotnet build "PhotoStudio.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoStudio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoStudio.WebApi.dll"]