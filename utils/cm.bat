echo off
SET wd=..\src\ElectronicClassManager.Entities.Storage
SET su=..\ElectronicClassManager.Db.Configuration\ElectronicClassManager.Db.Configuration.csproj
SET p=..\src\ElectronicClassManager.Entities.Storage\ElectronicClassManager.Entities.Storage.csproj
SET sufull=..\src\ElectronicClassManager.Db.Configuration\ElectronicClassManager.Db.Configuration.csproj

rem cd %wd%

dotnet ef migrations add %1 -s %sufull% -p %p%