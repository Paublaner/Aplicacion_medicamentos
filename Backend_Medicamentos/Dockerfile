FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet publish -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish /app

ENTRYPOINT ["dotnet", "Backend_Medicamentos.dll"]