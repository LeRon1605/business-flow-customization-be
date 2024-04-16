FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
COPY ["BuildingBlocks/Presentation/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/Presentation/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/Shared/BuildingBlocks.Shared/BuildingBlocks.Shared.csproj", "BuildingBlocks/Shared/BuildingBlocks.Shared/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Kernel/BuildingBlocks.Kernel.csproj", "BuildingBlocks/Core/BuildingBlocks.Kernel/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/BuildingBlocks.EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus.RabbitMQ/"]
COPY ["BuildingBlocks/EventBus/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.EfCore/BuildingBlocks.Infrastructure.EfCore.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/Core/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/Core/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/Core/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Redis/BuildingBlocks.Infrastructure.Redis.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Redis/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.HangFire/BuildingBlocks.Infrastructure.HangFire.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.HangFire/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Cloudinary/BuildingBlocks.Infrastructure.Cloudinary.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Cloudinary/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure.Serilog/BuildingBlocks.Infrastructure.Serilog.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure.Serilog/"]
RUN dotnet restore "ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
