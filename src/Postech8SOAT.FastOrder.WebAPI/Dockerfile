#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Postech8SOAT.FastOrder.WebAPI/Postech8SOAT.FastOrder.WebAPI.csproj", "src/Postech8SOAT.FastOrder.WebAPI/"]
COPY ["src/Postech8SOAT.FastOrder.Controllers/Postech8SOAT.FastOrder.Controllers.csproj", "src/Postech8SOAT.FastOrder.Controllers/"]
COPY ["src/Postech8SOAT.FastOrder.Gateways/Postech8SOAT.FastOrder.Gateways.csproj", "src/Postech8SOAT.FastOrder.Gateways/"]
COPY ["src/Postech8SOAT.FastOrder.Domain/Postech8SOAT.FastOrder.Domain.csproj", "src/Postech8SOAT.FastOrder.Domain/"]
COPY ["src/Postech8SOAT.FastOrder.Infra.Data/Postech8SOAT.FastOrder.Infra.Data.csproj", "src/Postech8SOAT.FastOrder.Infra.Data/"]
COPY ["src/Postech8SOAT.FastOrder.Presenters/Postech8SOAT.FastOrder.Presenters.csproj", "src/Postech8SOAT.FastOrder.Presenters/"]
COPY ["src/Postech8SOAT.FastOrder.UseCase/Postech8SOAT.FastOrder.UseCases.csproj", "src/Postech8SOAT.FastOrder.UseCase/"]
COPY ["src/Postech8SOAT.FastOrder.Infra.IOC/Postech8SOAT.FastOrder.Infra.IOC.csproj", "src/Postech8SOAT.FastOrder.Infra.IOC/"]
COPY ["src/Postech8SOAT.FastOrder.Infra.Environment/Postech8SOAT.FastOrder.Infra.Env.csproj", "src/Postech8SOAT.FastOrder.Infra.Environment/"]
COPY ["src/CleanArch.UseCase/CleanArch.UseCase.csproj", "src/CleanArch.UseCase/"]
RUN dotnet restore "./src/Postech8SOAT.FastOrder.WebAPI/Postech8SOAT.FastOrder.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Postech8SOAT.FastOrder.WebAPI"
RUN dotnet build "./Postech8SOAT.FastOrder.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Postech8SOAT.FastOrder.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Copiar certificados para pasta app o container
COPY ./certs/ /app/certs/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech8SOAT.FastOrder.WebAPI.dll"]