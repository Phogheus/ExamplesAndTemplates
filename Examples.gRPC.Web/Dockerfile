#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Examples.gRPC.Web/Examples.gRPC.Web.csproj", "Examples.gRPC.Web/"]
RUN dotnet restore "Examples.gRPC.Web/Examples.gRPC.Web.csproj"
COPY . .
WORKDIR "/src/Examples.gRPC.Web"
RUN dotnet build "Examples.gRPC.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Examples.gRPC.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Examples.gRPC.Web.dll"]