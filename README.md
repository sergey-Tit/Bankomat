# Краткое описание

Реализована простая систему банкомата с использованием базы данных PostgreSQL и использованием Dependency Injection. Так же в тестах используются моки.

# Функциональные требования

- создание счета
- просмотр баланса счета
- снятие денег со счета
- пополнение счета
- просмотр истории операций

# Не функциональные требования

- интерактивный консольный интерфейс
- возможность выбора режима работы (пользователь, администратор)
    - при выборе пользователя запрашиваются данные счета (номер, пин)
    - при выборе администратора запрашивается системный пароль
        - при некорректном вводе пароля - система прекращает работу
- системный пароль параметризуем
- при попытке выполнения некорректных операций выводятся сообщения об ошибке
- данные персистентно сохраняются в базе данных (PostgreSQL)
- приложение имеет хексагональную архитектуру

# Test cases

- снятие денег со счёта
    - при достаточном балансе проверяется что сохраняется счёт с корректно обновлённым балансом
    - при недостаточном балансе сервис возвращает ошибку
- пополнение счёта
    - проверяется что сохраняется счёт с корректно обновлённым балансом

В данных тестах используются моки для репозиториев.
https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage (очень плохие практики в примерах
кода, использовать только как шоукейс технологии)

# Примеры

https://github.com/is-oop-y27/workshop-5
