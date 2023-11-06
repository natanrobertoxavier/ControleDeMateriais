FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /main
COPY ["ControleDeMateriais.Api/ControleDeMateriais.Api.csproj", "ControleDeMateriais.Api/"]
RUN dotnet restore "ControleDeMateriais.Api/ControleDeMateriais.Api.csproj"
COPY . .

RUN dotnet build "ControleDeMateriais.Api/ControleDeMateriais.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControleDeMateriais.Api/ControleDeMateriais.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControleDeMateriais.Api.dll"]
