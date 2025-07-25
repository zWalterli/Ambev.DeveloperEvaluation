FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ./DeveloperEvaluation.EventConsumer/*.csproj ./DeveloperEvaluation.EventConsumer/
RUN dotnet restore ./DeveloperEvaluation.EventConsumer/DeveloperEvaluation.EventConsumer.csproj

COPY ./DeveloperEvaluation.EventConsumer ./DeveloperEvaluation.EventConsumer
WORKDIR /src/DeveloperEvaluation.EventConsumer
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DeveloperEvaluation.EventConsumer.dll"]
