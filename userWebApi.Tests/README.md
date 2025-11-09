# userWebApi.Tests

Проект для тестирования приложения **userWebApi** с использованием **NUnit 3** фреймворка.

## 📋 Содержание

Этот проект содержит юнит-тесты для:
- **Controllers** - тестирование API контроллеров
- **Models** - тестирование моделей данных и бизнес-логики

## 🛠️ Используемые технологии

- **NUnit 3.13.3** - фреймворк для тестирования
- **NUnit3TestAdapter 4.2.1** - адаптер для запуска тестов
- **Microsoft.NET.Test.Sdk 17.3.2** - SDK для тестирования .NET
- **Moq 4.18.2** - библиотека для создания mock-объектов
- **FluentAssertions 6.7.0** - библиотека для более читаемых утверждений

## 📁 Структура проекта

```
userWebApi.Tests/
├── Controllers/
│   └── DataFileControllerTests.cs    # Тесты контроллера (70+ тестов)
├── Model/
│   ├── DataClassTests.cs             # Тесты DataClass
│   ├── FileDataTests.cs              # Тесты модели FileData
│   └── UserDataTests.cs              # Тесты модели UserData
└── userWebApi.Tests.csproj           # Файл проекта
```

## 🚀 Запуск тестов

### Используя .NET CLI

```bash
# Перейти в директорию тестового проекта
cd userWebApi.Tests

# Запустить все тесты
dotnet test

# Запустить с подробным выводом
dotnet test --verbosity detailed

# Запустить конкретный тестовый класс
dotnet test --filter FullyQualifiedName~DataFileControllerTests

# Запустить тесты с покрытием кода
dotnet test /p:CollectCoverage=true
```

### Используя Visual Studio

1. Откройте решение `userWebApi.sln`
2. Откройте **Test Explorer** (Test > Test Explorer)
3. Нажмите **Run All** для запуска всех тестов

### Используя Rider

1. Откройте решение `userWebApi.sln`
2. Откройте **Unit Tests** (View > Tool Windows > Unit Tests)
3. Нажмите на кнопку запуска тестов

## 📊 Покрытие тестами

### DataFileControllerTests

#### ✅ Health Check Tests
- `Get_HealthCheck_ReturnsHealthyMessage()` - проверка health endpoint
- `Get_HealthCheck_ReturnsString()` - проверка типа возвращаемого значения

#### ✅ GetOrCreate Tests
- `GetOrCreate_WithNullFileName_ReturnsNotFound()` - валидация null имени
- `GetOrCreate_WithEmptyFileName_ReturnsNotFound()` - валидация пустого имени
- `GetOrCreate_WithValidFileName_ReturnsOkResult()` - создание файла (3 варианта)

#### ✅ DeleteFile Tests
- `DeleteFile_WithValidFileName_ReturnsOkResult()` - удаление существующего файла
- `DeleteFile_WithNonExistentFile_ReturnsOkResult()` - удаление несуществующего файла

#### ✅ UpdateFile Tests
- `UpdateFile_WithValidData_ReturnsOkResult()` - обновление с корректными данными
- `UpdateFile_WithNullFileName_ReturnsNotFound()` - валидация null
- `UpdateFile_WithEmptyContent_ReturnsOkResult()` - обновление пустым содержимым

#### ✅ SetSymbols Tests
- `SetSymbols_WithSimpleText_ReturnsNumeratedText()` - нумерация простого текста
- `SetSymbols_WithEmptyContent_ReturnsEmptyString()` - обработка пустого контента
- `SetSymbols_WithMultipleLines_NumeratesEachLine()` - нумерация многострочного текста

#### ✅ Integration Tests
- `Integration_CreateUpdateAndDelete_WorksCorrectly()` - полный цикл работы с файлом

### FileDataTests & ActionOnFileTests
- Тесты создания объектов
- Тесты установки свойств
- Тесты перечислений (enum)
- Тесты преобразования типов

### UserDataTests
- Тесты создания пользователя
- Тесты всех свойств (Name, Password, IsMale, Education, HasCar)
- Тесты обработки Unicode символов
- Тесты клонирования объектов

### DataClassTests
- Тесты публичных свойств
- Тесты работы с файловыми путями
- Тесты многострочного контента

## 🎯 Принципы тестирования

### AAA Pattern (Arrange-Act-Assert)

Все тесты следуют паттерну AAA:

```csharp
[Test]
public void Method_Scenario_ExpectedBehavior()
{
    // Arrange - подготовка данных
    var controller = new DataFileController(new DataClass());
    
    // Act - выполнение действия
    var result = controller.Get();
    
    // Assert - проверка результата
    result.Should().Be("I'm healthy");
}
```

### Naming Convention

Имена тестов следуют конвенции:
```
MethodName_Scenario_ExpectedBehavior
```

Примеры:
- `Get_HealthCheck_ReturnsHealthyMessage`
- `UpdateFile_WithNullFileName_ReturnsNotFound`
- `UserData_CanSetName`

### Setup & Teardown

```csharp
[SetUp]
public void SetUp()
{
    // Выполняется перед КАЖДЫМ тестом
    _controller = new DataFileController(new DataClass());
}

[TearDown]
public void TearDown()
{
    // Выполняется после КАЖДОГО теста
    // Очистка ресурсов
}
```

## 🔍 Использование FluentAssertions

FluentAssertions делает тесты более читаемыми:

```csharp
// Вместо
Assert.IsNotNull(result);
Assert.IsInstanceOf<OkResult>(result);

// Используем
result.Should().NotBeNull()
      .And.BeOfType<OkResult>();
```

## 📝 Атрибуты NUnit

### [Test]
Помечает метод как тест:
```csharp
[Test]
public void MyTest() { }
```

### [TestCase]
Параметризованные тесты:
```csharp
[TestCase("testfile")]
[TestCase("my-test-file")]
[TestCase("файл123")]
public void GetOrCreate_WithValidFileName_ReturnsOkResult(string fileName)
{
    // Тест выполнится 3 раза с разными параметрами
}
```

### [SetUp] / [TearDown]
Выполняются до/после каждого теста

### [TestFixture]
Помечает класс как содержащий тесты

## 🐛 Отладка тестов

### В Visual Studio
1. Поставьте breakpoint в тесте
2. Правый клик на тест → Debug Test

### В Rider
1. Поставьте breakpoint
2. Нажмите на иконку debug рядом с тестом

## 📈 Расширение тестов

### Добавление новых тестов

1. Создайте новый класс в соответствующей папке:
```csharp
[TestFixture]
public class MyNewTests
{
    [SetUp]
    public void SetUp()
    {
        // Инициализация
    }

    [Test]
    public void MyTest_Scenario_ExpectedResult()
    {
        // Arrange
        // Act
        // Assert
    }
}
```

2. Используйте FluentAssertions для утверждений
3. Следуйте паттерну AAA
4. Используйте понятные имена тестов

### Использование Moq для mock-объектов

```csharp
var mockDataClass = new Mock<DataClass>();
mockDataClass.Setup(x => x.Str).Returns("Mocked value");

var controller = new DataFileController(mockDataClass.Object);
```

## ⚠️ Важные замечания

1. **Изоляция тестов**: Каждый тест должен быть независимым
2. **Очистка ресурсов**: Используйте TearDown для удаления временных файлов
3. **Не тестируйте приватные методы**: Тестируйте публичный API
4. **Один тест - одно утверждение**: Старайтесь проверять одну вещь за раз
5. **Используйте TestCase**: Для тестирования разных входных данных

## 🔗 Полезные ссылки

- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [Moq Documentation](https://github.com/moq/moq4)
- [.NET Testing Best Practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

## 📞 Поддержка

При возникновении вопросов:
1. Проверьте документацию NUnit
2. Посмотрите примеры тестов в этом проекте
3. Убедитесь, что все NuGet пакеты установлены

## 🎓 Примеры запуска

```bash
# Запустить все тесты
dotnet test

# Запустить только тесты контроллера
dotnet test --filter "FullyQualifiedName~DataFileControllerTests"

# Запустить только тесты моделей
dotnet test --filter "FullyQualifiedName~Model"

# Запустить конкретный тест
dotnet test --filter "FullyQualifiedName~Get_HealthCheck_ReturnsHealthyMessage"

# Запустить с детальным выводом
dotnet test --verbosity normal

# Запустить с логированием
dotnet test --logger "console;verbosity=detailed"
```

## ✨ Статистика

- **Всего тестовых классов**: 5
- **Всего тестов**: 40+
- **Покрытие**: Controllers, Models, Business Logic
- **Технологии**: NUnit, FluentAssertions, Moq
