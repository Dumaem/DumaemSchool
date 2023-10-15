FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY DumaemSchool.sln ./
COPY DumaemSchool.BlazorWeb/*.csproj ./DumaemSchool.BlazorWeb/
COPY DumaemSchool.Core/*.csproj ./DumaemSchool.Core/
COPY DumaemSchool.Database/*.csproj ./DumaemSchool.Database/
COPY DumaemSchool.Auth/*.csproj ./DumaemSchool.Auth/
COPY DumaemSchool.Migrator/*.csproj ./DumaemSchool.Migrator/
COPY DumaemSchool.SMTP/*.csproj ./DumaemSchool.SMTP/

RUN dotnet restore
COPY . .
WORKDIR "/src/DumaemSchool.Core"
RUN dotnet build "DumaemSchool.Core.csproj" -c Release -o /app/build

WORKDIR "/src/DumaemSchool.Database"
RUN dotnet build "DumaemSchool.Database.csproj" -c Release -o /app/build

WORKDIR "/src/DumaemSchool.SMTP"
RUN dotnet build "DumaemSchool.SMTP.csproj" -c Release -o /app/build

WORKDIR "/src/DumaemSchool.Auth"
RUN dotnet build "DumaemSchool.Auth.csproj" -c Release -o /app/build

WORKDIR "/src/DumaemSchool.Migrator"
RUN dotnet build "DumaemSchool.Migrator.csproj" -c Release -o /app/build

WORKDIR "/src/DumaemSchool.BlazorWeb"
RUN dotnet build "DumaemSchool.BlazorWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DumaemSchool.BlazorWeb.dll"]
