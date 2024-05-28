FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Hub/Presentation/Hub.Api/Hub.Api.csproj", "Services/Hub/Presentation/Hub.Api/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Mail/BuildingBlocks.Infrastructure.Mail.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Mail/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/Core/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/Core/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Kernel/BuildingBlocks.Kernel.csproj", "BuildingBlocks/Core/BuildingBlocks.Kernel/"]
COPY ["BuildingBlocks/Shared/BuildingBlocks.Shared/BuildingBlocks.Shared.csproj", "BuildingBlocks/Shared/BuildingBlocks.Shared/"]
COPY ["Services/Shared/Domain/Domain.csproj", "Services/Shared/Domain/"]
COPY ["Services/Shared/Presentation/Presentation.csproj", "Services/Shared/Presentation/"]
COPY ["BuildingBlocks/Presentation/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/Presentation/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/BuildingBlocks.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/BuildingBlocks.Infrastructure.Cloudinary.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/BuildingBlocks.Infrastructure.EfCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/BuildingBlocks.Infrastructure.HangFire.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/BuildingBlocks.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/BuildingBlocks.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/"]
COPY ["Services/Shared/Application/Application.csproj", "Services/Shared/Application/"]
COPY ["Services/Shared/IntegrationEvents/IntegrationEvents.csproj", "Services/Shared/IntegrationEvents/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/BuildingBlocks.Infrastructure.CloudflareR2.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/"]
COPY ["Services/Hub/Core/Hub.Application/Hub.Application.csproj", "Services/Hub/Core/Hub.Application/"]
COPY ["Services/Hub/Core/Hub.Domain/Hub.Domain.csproj", "Services/Hub/Core/Hub.Domain/"]
COPY ["Services/Hub/Core/Hub.IntegrationEventHandlers/Hub.IntegrationEventHandlers.csproj", "Services/Hub/Core/Hub.IntegrationEventHandlers/"]
COPY ["Services/Hub/Hub.Infrastructure.SignalR/Hub.Infrastructure.SignalR.csproj", "Services/Hub/Hub.Infrastructure.SignalR/"]
COPY ["Services/Hub/Hub.Infrastructure.MongoDb/Hub.Infrastructure.MongoDb.csproj", "Services/Hub/Hub.Infrastructure.MongoDb/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.MongoDb/BuildingBlocks.Infrastructure.MongoDb.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.MongoDb/"]
RUN dotnet restore "Services/Hub/Presentation/Hub.Api/Hub.Api.csproj"
COPY . .
WORKDIR "/src/Services/Hub/Presentation/Hub.Api"
RUN dotnet build "Hub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hub.Api.dll"]
