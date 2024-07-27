FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

RUN adduser --disabled-password --gecos "" stellar

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../../src/Server src/Server
COPY ../../src/Shared src/Shared

RUN dotnet restore "./src/Server/StellarChat.Server.Api/StellarChat.Server.Api.csproj"
COPY . .
WORKDIR "/src/Server/StellarChat.Server.Api"
RUN dotnet build "./StellarChat.Server.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StellarChat.Server.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN mkdir -p /app/_data && chown -R stellar:stellar /app/_data
USER stellar

VOLUME ["/app/_data"]

ENTRYPOINT ["dotnet", "StellarChat.Server.Api.dll"]
