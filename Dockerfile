# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app

COPY src/*.csproj .
RUN dotnet restore

COPY .config/ ./.config
RUN dotnet tool restore

COPY src/ .
RUN dotnet publish -c Release -o out

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "plcbase.dll"]