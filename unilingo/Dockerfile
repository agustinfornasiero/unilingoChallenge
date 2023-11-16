#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["unilingo/unilingo.csproj", "unilingo/"]
RUN dotnet restore "unilingo/unilingo.csproj"
COPY . .
WORKDIR "/src/unilingo"
RUN dotnet build "unilingo.csproj" -c Docker -o /app/build

FROM build AS publish
RUN dotnet publish "unilingo.csproj" -c Docker -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "unilingo.dll"]