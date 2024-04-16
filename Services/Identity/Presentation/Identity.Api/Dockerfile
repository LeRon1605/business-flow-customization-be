FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Presentation/Identity.Api/Identity.Api.csproj", "Services/Identity/Presentation/Identity.Api/"]
COPY ["BuildingBlocks/Shared/BuildingBlocks.Shared/BuildingBlocks.Shared.csproj", "BuildingBlocks/Shared/BuildingBlocks.Shared/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Kernel/BuildingBlocks.Kernel.csproj", "BuildingBlocks/Core/BuildingBlocks.Kernel/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/BuildingBlocks.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/Presentation/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/Presentation/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.EfCore/BuildingBlocks.Infrastructure.EfCore.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/Core/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/Core/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Redis/BuildingBlocks.Infrastructure.Redis.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Redis/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.HangFire/BuildingBlocks.Infrastructure.HangFire.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.HangFire/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Cloudinary/BuildingBlocks.Infrastructure.Cloudinary.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Cloudinary/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Serilog/BuildingBlocks.Infrastructure.Serilog.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Serilog/"]
COPY ["Services/Identity/Core/Identity.Application/Identity.Application.csproj", "Services/Identity/Core/Identity.Application/"]
COPY ["Services/Identity/Core/Identity.Domain/Identity.Domain.csproj", "Services/Identity/Core/Identity.Domain/"]
COPY ["Services/Shared/IntegrationEvents/IntegrationEvents.csproj", "Services/Shared/IntegrationEvents/"]
COPY ["Services/Identity/Infrastructure/Identity.Infrastructure.EfCore/Identity.Infrastructure.EfCore.csproj", "Services/Identity/Infrastructure/Identity.Infrastructure.EfCore/"]
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
