FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DrumBot/DrumBot.csproj", "DrumBot/"]
RUN dotnet restore "DrumBot/DrumBot.csproj"
COPY . .
WORKDIR "/src/DrumBot"
RUN dotnet build "DrumBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DrumBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DrumBot.dll"]
