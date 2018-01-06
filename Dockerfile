FROM microsoft/aspnetcore-build:1.1.5 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/aspnetcore:1.1.5
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "DotNetAPI.dll"]
