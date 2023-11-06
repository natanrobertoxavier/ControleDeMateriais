FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /main/ControleDeMateriais.Api
COPY ["ControleDeMateriais.Api.csproj", "main/"]
RUN dotnet restore "main/ControleDeMateriais.Api.csproj"
COPY . .

RUN dotnet build "ControleDeMateriais.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControleDeMateriais.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControleDeMateriais.Api.dll"]