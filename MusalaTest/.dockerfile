FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

COPY MusalaTest/*.csproj MusalaTest/

WORKDIR /app/MusalaTest
RUN dotnet restore
WORKDIR /app

COPY . .
WORKDIR /app/MusalaTest
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
COPY --from=build /app/MusalaTest/out .
ENTRYPOINT ["dotnet", "MusalaTest.dll"]