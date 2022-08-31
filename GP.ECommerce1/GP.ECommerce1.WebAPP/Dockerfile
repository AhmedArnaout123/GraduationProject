FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ./src/Nuget.Docker.config ./Nuget.config

WORKDIR /src/src

COPY ./src/*.sln ./
COPY ./src/*/*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

WORKDIR /src

RUN dotnet restore --configfile=Nuget.config ./src/STB.App/STB.App.csproj

COPY . .

RUN dotnet publish "src/STB.App/STB.App.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish

RUN apt-get update
RUN apt-get install -y libc6 -f -o APT::Immediate-Configure=0
RUN apt-get install -y libgdiplus libicu-dev libharfbuzz0b libfontconfig1 libfreetype6 libpango-1.0-0 libpangocairo-1.0-0

WORKDIR /app
COPY --from=build /publish .

EXPOSE 5006

ENTRYPOINT ["dotnet", "STB.App.dll"]