# 🧪 Руководство по тестированию проекта userWebApi

## 📋 Обзор

Проект содержит полноценный набор юнит-тестов на базе **NUnit 3**, покрывающий:
- ✅ API контроллеры
- ✅ Модели данных
- ✅ Бизнес-логику

## 🚀 Быстрый старт

### Запуск всех тестов

```bash
# Из корневой директории проекта
cd userWebApi.Tests
dotnet test

# Или из корня решения
dotnet test userWebApi.Tests/userWebApi.Tests.csproj
```

### Запуск с подробным выводом

```bash
dotnet test --verbosity detailed
```

## 📊 Статистика проекта

```
┌─────────────────────────────────────────────────────────┐
│  Тестовый проект: userWebApi.Tests                      │
├─────────────────────────────────────────────────────────┤
│  Фреймворк:        NUnit 3.13.3                         │
│  Тестовых классов: 5                                    │
│  Всего тестов:     40+                                  │
│  Библиотеки:       Moq, FluentAssertions               │
└─────────────────────────────────────────────────────────┘
```

## 📁 Структура тестов

```
userWebApi.Tests/
│
├── Controllers/
│   └── DataFileControllerTests.cs      # 15+ тестов для API контроллера
│       ├── Health Check Tests          # Проверка работоспособности
│       ├── GetOrCreate Tests           # Создание/получение файлов
│       ├── DeleteFile Tests            # Удаление файлов
│       ├── UpdateFile Tests            # Обновление содержимого
│       ├── SetSymbols Tests            # Нумерация символов
│       └── Integration Tests           # Интеграционные сценарии
│
├── Model/
│   ├── DataClassTests.cs               # 10+ тестов для DataClass
│   ├── FileDataTests.cs                # 6+ тестов для FileData
│   ├── UserDataTests.cs                # 9+ тестов для UserData
│   └── ActionOnFileTests.cs            # Тесты перечисления
│
└── TestHelper.cs                       # Вспомогательные утилиты
```

## 🎯 Примеры тестов

### 1. Простой тест с FluentAssertions

```csharp
[Test]
public void Get_HealthCheck_ReturnsHealthyMessage()
{
    // Arrange
    var controller = new DataFileController(new DataClass());
    
    // Act
    var result = controller.Get();
    
    // Assert
    result.Should().Be("I'm healthy");
}
```

### 2. Параметризованный тест

```csharp
[TestCase("testfile")]
[TestCase("my-test-file")]
[TestCase("файл123")]
public void GetOrCreate_WithValidFileName_ReturnsOkResult(string fileName)
{
    // Act
    var result = _controller.GetOrCreate(fileName);

    // Assert
    result.Should().BeOfType<OkObjectResult>();
}
```

### 3. Интеграционный тест

```csharp
[Test]
public void Integration_CreateUpdateAndDelete_WorksCorrectly()
{
    // Arrange
    string fileName = "integration-test";
    
    // Act: Create → Update → Delete
    var createResult = _controller.GetOrCreate(fileName);
    var updateResult = _controller.UpdateFile(fileName, fileData);
    var deleteResult = _controller.DeleteFile(fileName);
    
    // Assert: все операции успешны
    createResult.Should().BeOfType<OkObjectResult>();
    updateResult.Should().BeOfType<OkResult>();
    deleteResult.Should().BeOfType<OkResult>();
}
```

## 🔧 Команды для работы с тестами

### Базовые команды

```bash
# Запустить все тесты
dotnet test

# Запустить с детальным выводом
dotnet test --verbosity detailed

# Запустить только тесты контроллера
dotnet test --filter "FullyQualifiedName~DataFileControllerTests"

# Запустить только тесты моделей
dotnet test --filter "FullyQualifiedName~Model"

# Запустить конкретный тест
dotnet test --filter "FullyQualifiedName~Get_HealthCheck_ReturnsHealthyMessage"
```

### Продвинутые команды

```bash
# Запустить тесты с логированием в файл
dotnet test --logger "trx;LogFileName=test-results.trx"

# Запустить тесты с покрытием кода (требует дополнительные пакеты)
dotnet test /p:CollectCoverage=true

# Запустить тесты с таймаутом
dotnet test --blame-hang-timeout 5m

# Запустить тесты параллельно
dotnet test --parallel
```

## 📦 Используемые пакеты

```xml
<ItemGroup>
  <PackageReference Include="NUnit" Version="3.13.3" />
  <PackageReference Include="NUnit3TestAdapter" Version="4.2.1" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.3.2" />
  <PackageReference Include="Moq" Version="4.18.2" />
  <PackageReference Include="FluentAssertions" Version="6.7.0" />
</ItemGroup>
```

### Описание пакетов

- **NUnit** - Основной фреймворк для тестирования
- **NUnit3TestAdapter** - Адаптер для запуска NUnit тестов в Visual Studio
- **Microsoft.NET.Test.Sdk** - .NET Test SDK
- **Moq** - Библиотека для создания mock-объектов (используется для будущих тестов)
- **FluentAssertions** - Делает тесты более читаемыми и выразительными

## 🎨 Стиль написания тестов

### Naming Convention

```
MethodName_Scenario_ExpectedBehavior
```

**Примеры:**
- ✅ `Get_HealthCheck_ReturnsHealthyMessage`
- ✅ `UpdateFile_WithNullFileName_ReturnsNotFound`
- ✅ `UserData_CanSetName`
- ❌ `Test1`, `TestMethod`, `MyTest`

### AAA Pattern (Arrange-Act-Assert)

Все тесты следуют паттерну:

```csharp
[Test]
public void Method_Scenario_ExpectedBehavior()
{
    // Arrange - подготовка тестовых данных
    var controller = new DataFileController(new DataClass());
    var fileName = "test-file";
    
    // Act - выполнение тестируемого действия
    var result = controller.GetOrCreate(fileName);
    
    // Assert - проверка результата
    result.Should().BeOfType<OkObjectResult>();
}
```

### Setup и Teardown

```csharp
[TestFixture]
public class MyTests
{
    private DataFileController _controller;
    
    [SetUp]
    public void SetUp()
    {
        // Выполняется ПЕРЕД каждым тестом
        _controller = new DataFileController(new DataClass());
    }
    
    [TearDown]
    public void TearDown()
    {
        // Выполняется ПОСЛЕ каждого теста
        // Очистка ресурсов, удаление временных файлов
    }
}
```

## 🔍 FluentAssertions - Примеры использования

### Базовые проверки

```csharp
// Проверка на null
result.Should().NotBeNull();
result.Should().BeNull();

// Проверка типа
result.Should().BeOfType<OkResult>();
result.Should().BeAssignableTo<IActionResult>();

// Проверка строк
text.Should().Be("expected");
text.Should().Contain("substring");
text.Should().StartWith("Hello");
text.Should().BeNullOrEmpty();
```

### Сложные проверки

```csharp
// Цепочка проверок
result.Should().NotBeNull()
      .And.BeOfType<OkObjectResult>()
      .Which.StatusCode.Should().Be(200);

// Проверка коллекций
list.Should().HaveCount(3);
list.Should().Contain(item => item.Name == "Test");

// Проверка чисел
number.Should().BeGreaterThan(0);
number.Should().BeInRange(1, 10);
```

## 🐛 Отладка тестов

### Visual Studio

1. Откройте **Test Explorer** (Test → Test Explorer)
2. Поставьте breakpoint в тесте
3. Правый клик на тест → **Debug Selected Tests**

### Visual Studio Code

1. Установите расширение **.NET Core Test Explorer**
2. Поставьте breakpoint
3. Нажмите **Debug** рядом с тестом в Test Explorer

### Rider

1. Откройте **Unit Tests** (View → Tool Windows → Unit Tests)
2. Поставьте breakpoint
3. Нажмите иконку Debug рядом с тестом

## 📈 Добавление новых тестов

### Шаг 1: Создайте тестовый класс

```csharp
using NUnit.Framework;
using FluentAssertions;

namespace userWebApi.Tests.MyFeature
{
    [TestFixture]
    public class MyNewFeatureTests
    {
        [SetUp]
        public void SetUp()
        {
            // Инициализация перед каждым тестом
        }

        [Test]
        public void MyMethod_WithValidInput_ReturnsExpectedResult()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}
```

### Шаг 2: Запустите тесты

```bash
dotnet test --filter "FullyQualifiedName~MyNewFeatureTests"
```

## ⚠️ Важные принципы

### ✅ DO (Делайте)

- ✅ Один тест проверяет одну вещь
- ✅ Используйте понятные имена тестов
- ✅ Следуйте паттерну AAA
- ✅ Изолируйте тесты друг от друга
- ✅ Очищайте ресурсы в TearDown
- ✅ Используйте TestCase для параметризации
- ✅ Пишите тесты для edge cases

### ❌ DON'T (Не делайте)

- ❌ Не тестируйте приватные методы
- ❌ Не делайте тесты зависимыми друг от друга
- ❌ Не используйте реальную базу данных
- ❌ Не делайте тесты слишком сложными
- ❌ Не игнорируйте падающие тесты
- ❌ Не используйте Thread.Sleep в тестах

## 🎓 Полезные ресурсы

- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [Moq Quick Start](https://github.com/moq/moq4/wiki/Quickstart)
- [.NET Testing Best Practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Test Driven Development (TDD)](https://martinfowler.com/bliki/TestDrivenDevelopment.html)

## 🆘 Troubleshooting

### Проблема: Тесты не обнаруживаются

**Решение:**
```bash
# Очистить и пересобрать проект
dotnet clean
dotnet build
dotnet test
```

### Проблема: Ошибка с NuGet пакетами

**Решение:**
```bash
# Восстановить пакеты
cd userWebApi.Tests
dotnet restore
```

### Проблема: Конфликт версий пакетов

**Решение:**
Проверьте `userWebApi.Tests.csproj` и убедитесь, что версии пакетов совместимы с .NET Core 3.1

## 📞 Поддержка

При возникновении вопросов:
1. Проверьте документацию в `userWebApi.Tests/README.md`
2. Изучите примеры тестов в проекте
3. Убедитесь, что все NuGet пакеты установлены
4. Проверьте совместимость версий

---

**Создано:** 2025-11-09  
**Framework:** .NET Core 3.1  
**Test Framework:** NUnit 3  
**Принципы:** AAA Pattern, SOLID, DRY, KISS
