# このステージは、VS から高速モードで実行するときに使用されます (デバッグ構成の既定値)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# このステージは、サービス プロジェクトのビルドに使用されます
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PostScheduler.WebApp/PostScheduler.WebApp/PostScheduler.WebApp.csproj", "src/PostScheduler.WebApp/PostScheduler.WebApp/"]
COPY ["src/PostScheduler.Domain/PostScheduler.Domain.csproj", "src/PostScheduler.Domain/"]
COPY ["src/PostScheduler/PostScheduler.csproj", "src/PostScheduler/"]
COPY ["src/PostScheduler.WebApp/PostScheduler.WebApp.Client/PostScheduler.WebApp.Client.csproj", "src/PostScheduler.WebApp/PostScheduler.WebApp.Client/"]
RUN dotnet restore "./src/PostScheduler.WebApp/PostScheduler.WebApp/PostScheduler.WebApp.csproj"
COPY . .
WORKDIR "/src/src/PostScheduler.WebApp/PostScheduler.WebApp"
RUN dotnet build "./PostScheduler.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# このステージは、最終ステージにコピーするサービス プロジェクトを公開するために使用されます
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PostScheduler.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# このステージは、運用環境または VS から通常モードで実行している場合に使用されます (デバッグ構成を使用しない場合の既定)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostScheduler.WebApp.dll"]
