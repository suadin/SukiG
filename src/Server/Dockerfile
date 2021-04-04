#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Server/SukiG.Server.csproj", "src/Server/"]
COPY ["src/Client/SukiG.Client.csproj", "src/Client/"]
COPY ["src/Shared/SukiG.Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/Server/SukiG.Server.csproj"
COPY . .
WORKDIR "/src/src/Server"
RUN dotnet build "SukiG.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SukiG.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SukiG.Server.dll"]