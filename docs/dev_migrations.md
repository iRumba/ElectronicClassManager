# Разработческие миграции

## Создание миграций
Переместиться в папку 
```src\ElectronicClassManager.Entities.Storage```

открыть cmd или PowerShell

Выполнить команду 

```dotnet ef migrations add ИмяМиграции -s ../ElectronicClassManager.Db.Configuration/ElectronicClassManager.Db.Configuration.csproj```

Или перейти в папку utils и выполнить команду 
```cm.bat ИмяМиграции```