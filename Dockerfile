# Etapa 1: build da aplicação
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

# Copia apenas os arquivos de projeto para restaurar mais rápido
COPY ["GamePlatform.Api/GamePlatform.Api.csproj", "GamePlatform.Api/"]
COPY ["GamePlatform.Application/GamePlatform.Application.csproj", "GamePlatform.Application/"]
COPY ["GamePlatform.Domain/GamePlatform.Domain.csproj", "GamePlatform.Domain/"]
COPY ["GamePlatform.Infrastructure/GamePlatform.Infrastructure.csproj", "GamePlatform.Infrastructure/"]

# Restaurar os pacotes
RUN dotnet restore -a $TARGETARCH "GamePlatform.Api/GamePlatform.Api.csproj"

# Copiar tudo e compilar
COPY . .
WORKDIR "/src/GamePlatform.Api"
RUN dotnet publish -a $TARGETARCH "GamePlatform.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Install New Relic agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb [signed-by=/usr/share/keyrings/newrelic-apt.gpg] http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget -O- https://download.newrelic.com/NEWRELIC_APT_2DAD550E.public | gpg --import --batch --no-default-keyring --keyring /usr/share/keyrings/newrelic-apt.gpg \
&& apt-get update \
&& apt-get install -y newrelic-dotnet-agent

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "GamePlatform.Api.dll"]