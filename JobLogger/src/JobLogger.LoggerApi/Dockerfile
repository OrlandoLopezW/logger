FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/JobLogger.LoggerApi/JobLogger.LoggerApi.csproj", "src/JobLogger.LoggerApi/"]
RUN dotnet restore "src/JobLogger.LoggerApi/JobLogger.LoggerApi.csproj"
COPY . .
WORKDIR "/src/src/JobLogger.LoggerApi"
RUN dotnet build "JobLogger.LoggerApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JobLogger.LoggerApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JobLogger.LoggerApi.dll"]