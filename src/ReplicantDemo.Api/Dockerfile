FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ReplicantDemo.Api/ReplicantDemo.Api.csproj", "ReplicantDemo.Api/"]
COPY ["ReplicantDemo.Core/ReplicantDemo.Core.csproj", "ReplicantDemo.Core/"]
COPY ["ReplicantDemo.Infrastructure/ReplicantDemo.Infrastructure.csproj", "ReplicantDemo.Infrastructure/"]
RUN dotnet restore "ReplicantDemo.Api/ReplicantDemo.Api.csproj"
COPY . .
WORKDIR "/src/ReplicantDemo.Api"
RUN dotnet build "ReplicantDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReplicantDemo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReplicantDemo.Api.dll"]