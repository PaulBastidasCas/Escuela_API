FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Escuela.Api/Escuela.Api.csproj", "Escuela.Api/"]
RUN dotnet restore "Escuela.Api/Escuela.Api.csproj"
COPY . .


WORKDIR "/src/Escuela.Api"

RUN dotnet build "Escuela.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Escuela.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Escuela.Api.dll"]