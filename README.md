# TomatoAPI

Dockerized .NET Core 1.1 REST API boilerplate with Postgresql and .env vars

## Imperfections

Nothing is perfect and this project is **very far** from it. Bellow I document oddities to avoid unnecessary headices.

### 1. Must-have env vars (production only)
Make sure you set these env vars using `docker ... -e KEY="VALUE"` or similar

* `DOTNET_ENV="Production"`
* `CONNECTION_STRING="User ID=username;Password=password;Server=localhost;Port=5432;Database=dbname"`

### 2. `appsettings.json` is overrided

Many env-variables are stored in the `env` dict in `Globals.cs` to allow passing enviroment through docker, this causes `appsettings.json` to become unreliable. Currently both are being used and should be migrated to a better solution.

### 3. Migrations run with `dotnet run` (production only)
To allow migrations to be executed automatically they are being run on app start on production enviroments. This could be dangerous since **data may be lost** unexpectedly if an unwanted migration is commited and pushed
