FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57665
EXPOSE 44387

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["./NetCore.API/NetCore.API.csproj", "NetCore.API/"]
COPY ["./NetCore.Infrastructure/NetCore.Infrastructure.csproj", "NetCore.Infrastructure/"]
COPY ["./NetCore.Core/NetCore.Core.csproj", "NetCore.Core/"]
RUN dotnet restore "NetCore.API/NetCore.API.csproj"
COPY . .
WORKDIR "/src/NetCore.API"
RUN dotnet build "NetCore.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetCore.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetCore.API.dll"]