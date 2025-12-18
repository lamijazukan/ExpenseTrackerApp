FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi/ExpenseTrackerApp.WebApi.csproj", "ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi/"]
COPY ["ExpenseTrackerApp/src/ExpenseTrackerApp.Application/ExpenseTrackerApp.Application.csproj", "ExpenseTrackerApp/src/ExpenseTrackerApp.Application/"]
COPY ["ExpenseTrackerApp/src/ExpenseTrackerApp.Domain/ExpenseTrackerApp.Domain.csproj", "ExpenseTrackerApp/src/ExpenseTrackerApp.Domain/"]
COPY ["ExpenseTrackerApp/src/ExpenseTrackerApp.Contracts/ExpenseTrackerApp.Contracts.csproj", "ExpenseTrackerApp/src/ExpenseTrackerApp.Contracts/"]
COPY ["ExpenseTrackerApp/src/ExpenseTrackerApp.Infrastructure/ExpenseTrackerApp.Infrastructure.csproj", "ExpenseTrackerApp/src/ExpenseTrackerApp.Infrastructure/"]
RUN dotnet restore "ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi/ExpenseTrackerApp.WebApi.csproj"
COPY . .
WORKDIR "/src/ExpenseTrackerApp/src/ExpenseTrackerApp.WebApi"
RUN dotnet build "./ExpenseTrackerApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExpenseTrackerApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTrackerApp.WebApi.dll"]
