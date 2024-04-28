FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/BusinessFlow/Presentation/BusinessFlow.Api/BusinessFlow.Api.csproj", "Services/BusinessFlow/Presentation/BusinessFlow.Api/"]
COPY ["BuildingBlocks/Presentation/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/Presentation/BuildingBlocks.Presentation/"]
COPY ["Services/Shared/Domain/Domain.csproj", "Services/Shared/Domain/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/BuildingBlocks.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/Shared/BuildingBlocks.Shared/BuildingBlocks.Shared.csproj", "BuildingBlocks/Shared/BuildingBlocks.Shared/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Kernel/BuildingBlocks.Kernel.csproj", "BuildingBlocks/Core/BuildingBlocks.Kernel/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/BuildingBlocks.Infrastructure.Cloudinary.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/Core/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/Core/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/BuildingBlocks.Infrastructure.EfCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/BuildingBlocks.Infrastructure.HangFire.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/BuildingBlocks.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/BuildingBlocks.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/BuildingBlocks.Infrastructure.CloudflareR2.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/"]
COPY ["Services/Shared/Application/Application.csproj", "Services/Shared/Application/"]
COPY ["Services/Shared/Presentation/Presentation.csproj", "Services/Shared/Presentation/"]
COPY ["Services/BusinessFlow/Core/BusinessFlow.Application/BusinessFlow.Application.csproj", "Services/BusinessFlow/Core/BusinessFlow.Application/"]
COPY ["Services/BusinessFlow/Core/BusinessFlow.Domain/BusinessFlow.Domain.csproj", "Services/BusinessFlow/Core/BusinessFlow.Domain/"]
COPY ["Services/BusinessFlow/Core/BusinessFlow.IntegrationEventHandlers/BusinessFlow.IntegrationEventHandlers.csproj", "Services/BusinessFlow/Core/BusinessFlow.IntegrationEventHandlers/"]
RUN dotnet restore "Services/BusinessFlow/Presentation/BusinessFlow.Api/BusinessFlow.Api.csproj"
COPY . .
WORKDIR "/src/Services/BusinessFlow/Presentation/BusinessFlow.Api"
RUN dotnet build "BusinessFlow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessFlow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessFlow.Api.dll"]
