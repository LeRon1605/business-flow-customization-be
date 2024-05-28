FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Presentation/Identity.Api/Identity.Api.csproj", "Services/Identity/Presentation/Identity.Api/"]
COPY ["Services/Shared/Presentation/Presentation.csproj", "Services/Shared/Presentation/"]
COPY ["BuildingBlocks/Presentation/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/Presentation/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/BuildingBlocks.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/Shared/BuildingBlocks.Shared/BuildingBlocks.Shared.csproj", "BuildingBlocks/Shared/BuildingBlocks.Shared/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Kernel/BuildingBlocks.Kernel.csproj", "BuildingBlocks/Core/BuildingBlocks.Kernel/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/Core/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/Core/BuildingBlocks.Domain/"]
COPY ["Services/Shared/Domain/Domain.csproj", "Services/Shared/Domain/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/BuildingBlocks.Infrastructure.Cloudinary.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Cloudinary/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/BuildingBlocks.Infrastructure.EfCore.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/BuildingBlocks.Infrastructure.HangFire.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.HangFire/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/BuildingBlocks.Infrastructure.Redis.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Redis/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/BuildingBlocks.Infrastructure.Serilog.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.Serilog/"]
COPY ["Services/Shared/Application/Application.csproj", "Services/Shared/Application/"]
COPY ["Services/Shared/IntegrationEvents/IntegrationEvents.csproj", "Services/Shared/IntegrationEvents/"]
COPY ["BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/BuildingBlocks.Infrastructure.CloudflareR2.csproj", "BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.CloudflareR2/"]
COPY ["Services/Identity/Core/Identity.Application/Identity.Application.csproj", "Services/Identity/Core/Identity.Application/"]
COPY ["Services/Identity/Core/Identity.Domain/Identity.Domain.csproj", "Services/Identity/Core/Identity.Domain/"]
COPY ["Services/Identity/Infrastructure/Identity.Infrastructure.EfCore/Identity.Infrastructure.EfCore.csproj", "Services/Identity/Infrastructure/Identity.Infrastructure.EfCore/"]
COPY ["Services/Identity/Infrastructure/Identity.Infrastructure.IdentityServer/Identity.Infrastructure.IdentityServer.csproj", "Services/Identity/Infrastructure/Identity.Infrastructure.IdentityServer/"]
RUN dotnet restore "Services/Identity/Presentation/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Presentation/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
