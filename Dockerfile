FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /main/ControleDeMateriais.Api
COPY ["ControleDeMateriais.Api.csproj", "."]
RUN dotnet restore "/main/ControleDeMateriais.Api/ControleDeMateriais.Api.csproj"
COPY . .

RUN dotnet build "/main/ControleDeMateriais.Api/ControleDeMateriais.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/main/ControleDeMateriais.Api/ControleDeMateriais.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControleDeMateriais.Api.dll"]