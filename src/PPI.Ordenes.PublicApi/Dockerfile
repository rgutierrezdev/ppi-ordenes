FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_NOLOGO=1
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/PPI.Ordenes.PublicApi/PPI.Ordenes.PublicApi.csproj", "src/PPI.Ordenes.PublicApi/"]
COPY ["src/PPI.Ordenes.Application/PPI.Ordenes.Application.csproj", "src/PPI.Ordenes.Application/"]
COPY ["src/PPI.Ordenes.Domain/PPI.Ordenes.Domain.csproj", "src/PPI.Ordenes.Domain/"]
COPY ["src/PPI.Ordenes.Core/PPI.Ordenes.Core.csproj", "src/PPI.Ordenes.Core/"]
COPY ["src/PPI.Ordenes.Infrastructure/PPI.Ordenes.Infrastructure.csproj", "src/PPI.Ordenes.Infrastructure/"]
COPY ["src/PPI.Ordenes.Query/PPI.Ordenes.Query.csproj", "src/PPI.Ordenes.Query/"]
RUN dotnet restore "src/PPI.Ordenes.PublicApi/PPI.Ordenes.PublicApi.csproj"
COPY . .
WORKDIR "/src/src/PPI.Ordenes.PublicApi"
RUN dotnet build "PPI.Ordenes.PublicApi.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "PPI.Ordenes.PublicApi.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PPI.Ordenes.PublicApi.dll"]