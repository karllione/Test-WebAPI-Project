# Test WebAPI Project
Тестовое клиент-серверное приложение на ASP.NET WebAPI, реализующее базовые CRUD операции над таблицами Patients и Doctors. В качестве хранилища используется MS SQL Server.

## Перед запуском приложения

1. Откройте файл `appsettings.json`; 
2. Подключитесь к БД. Введите свою строку подключения к SQL Server, пример:

```
"ConnectionStrings": {
  "DefaultConnection": "Server= ___ ;Database=___;User Id=___;Password=___;"
}
```
3. Запустите консоль диспетчера пакетов NuGet и введите команду `Update-Database`;
4. Заполните все существующие таблицы в БД.

Приложение готово к использованию :space_invader:


