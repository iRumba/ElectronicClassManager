echo off
SET p=..\src\ElectronicClassManager.Entities.Storage\ElectronicClassManager.Entities.Storage.csproj
SET sufull=..\src\ElectronicClassManager.Db.Configuration\ElectronicClassManager.Db.Configuration.csproj

dotnet ef database update %1 -s %sufull% -p %p% --no-build