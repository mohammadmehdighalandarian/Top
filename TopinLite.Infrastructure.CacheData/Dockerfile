
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 3010

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TopinLite.Infrastructure.CacheData/TopinLite.Infrastructure.CacheData.csproj", "TopinLite.Infrastructure.CacheData/"]
COPY ["NatsRpcFoundation/NatsRpcFoundation.csproj", "NatsRpcFoundation/"]
COPY ["TopinLite.Domain/TopinLite.Domain.csproj", "TopinLite.Domain/"]
COPY ["TopinLite.Infra.InMemoryDb/TopinLite.Infra.InMemoryDb.csproj", "TopinLite.Infra.InMemoryDb/"]
RUN dotnet restore "./TopinLite.Infrastructure.CacheData/TopinLite.Infrastructure.CacheData.csproj"
COPY . .
WORKDIR "/src/TopinLite.Infrastructure.CacheData"
RUN dotnet build "./TopinLite.Infrastructure.CacheData.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TopinLite.Infrastructure.CacheData.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
USER root
RUN sed -i 's/https:\/\/dl-cdn.alpinelinux.org\/alpine\/v3.22\/main/https:\/\/mirror.arvancloud.ir\/alpine\/v3.22\/main/g' /etc/apk/repositories
RUN sed -i 's/https:\/\/dl-cdn.alpinelinux.org\/alpine\/v3.22\/community/https:\/\/mirror.arvancloud.ir\/alpine\/v3.22\/community/g' /etc/apk/repositories
RUN apk update && apk upgrade && apk add --no-cache tzdata
ENV TZ=Asia/Tehran
RUN cp /usr/share/zoneinfo/$TZ /etc/localtime
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TopinLite.Infrastructure.CacheData.dll"]