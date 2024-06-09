FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ../../src/Shared/StellarChat.Shared.Contracts/StellarChat.Shared.Contracts.csproj src/Shared/StellarChat.Shared.Contracts/
COPY ../../src/Client/StellarChat.Client.Web/StellarChat.Client.Web.csproj src/Client/StellarChat.Client.Web/

RUN dotnet restore ./src/Client/StellarChat.Client.Web/StellarChat.Client.Web.csproj

COPY ../../src/Shared/StellarChat.Shared.Contracts src/Shared/StellarChat.Shared.Contracts
COPY ../../src/Client/StellarChat.Client.Web src/Client/StellarChat.Client.Web

ARG BUILD_CONFIGURATION=Release
RUN dotnet build ./src/Client/StellarChat.Client.Web/StellarChat.Client.Web.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./src/Client/StellarChat.Client.Web/StellarChat.Client.Web.csproj -c $BUILD_CONFIGURATION -o /app/publish

FROM nginx:1.27.0-alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY /docker/webapp/nginx.conf /etc/nginx/nginx.conf
CMD ["/bin/sh", "-c", "envsubst < /usr/share/nginx/html/appsettings.template.json > /usr/share/nginx/html/appsettings.json && nginx -g 'daemon off;'"]
EXPOSE 7080
EXPOSE 7443
