#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ftalive-webapi/ftalive-webapi.csproj", "ftalive-webapi/"]
RUN dotnet restore "ftalive-webapi/ftalive-webapi.csproj"
COPY . .
WORKDIR "/src/ftalive-webapi"
RUN dotnet build "ftalive-webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ftalive-webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ftalive-webapi.dll"]