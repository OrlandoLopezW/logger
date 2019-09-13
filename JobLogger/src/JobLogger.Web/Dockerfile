FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -y build-essential nodejs
	
WORKDIR /src
COPY ["src/JobLogger.Web/JobLogger.Web.csproj", "src/JobLogger.Web/"]
RUN dotnet restore "src/JobLogger.Web/JobLogger.Web.csproj"
COPY . .
WORKDIR "/src/src/JobLogger.Web"
RUN dotnet build "JobLogger.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JobLogger.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JobLogger.Web.dll"]