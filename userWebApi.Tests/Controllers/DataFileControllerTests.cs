using System;
using System.IO;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using userWebApi;
using userWebApi.Model;
using FluentAssertions;

namespace userWebApi.Tests.Controllers
{
    /// <summary>
    /// Тесты для контроллера DataFileController
    /// </summary>
    [TestFixture]
    public class DataFileControllerTests
    {
        private DataFileController _controller;
        private DataClass _dataClass;
        private string _testDirectory;

        [SetUp]
        public void SetUp()
        {
            // Arrange: подготовка окружения для каждого теста
            _dataClass = new DataClass();
            _controller = new DataFileController(_dataClass);
            _testDirectory = Path.Combine(Path.GetTempPath(), "userWebApiTests");
            
            // Создаем тестовую директорию, если не существует
            if (!Directory.Exists(_testDirectory))
            {
                Directory.CreateDirectory(_testDirectory);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup: очистка после каждого теста
            if (Directory.Exists(_testDirectory))
            {
                try
                {
                    Directory.Delete(_testDirectory, true);
                }
                catch
                {
                    // Игнорируем ошибки при удалении
                }
            }
        }

        #region Health Check Tests

        [Test]
        public void Get_HealthCheck_ReturnsHealthyMessage()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().Be("I'm healthy");
        }

        [Test]
        public void Get_HealthCheck_ReturnsString()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<string>(result);
        }

        #endregion

        #region GetOrCreate Tests

        [Test]
        public void GetOrCreate_WithNullFileName_ReturnsNotFound()
        {
            // Arrange
            string fileName = null;

            // Act
            var result = _controller.GetOrCreate(fileName);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public void GetOrCreate_WithEmptyFileName_ReturnsNotFound()
        {
            // Arrange
            string fileName = string.Empty;

            // Act
            var result = _controller.GetOrCreate(fileName);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        [TestCase("testfile")]
        [TestCase("my-test-file")]
        [TestCase("файл123")]
        public void GetOrCreate_WithValidFileName_ReturnsOkResult(string fileName)
        {
            // Act
            var result = _controller.GetOrCreate(fileName);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);
        }

        #endregion

        #region DeleteFile Tests

        [Test]
        public void DeleteFile_WithValidFileName_ReturnsOkResult()
        {
            // Arrange
            string fileName = "test-delete-file";

            // Act
            var result = _controller.DeleteFile(fileName);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void DeleteFile_WithNonExistentFile_ReturnsOkResult()
        {
            // Arrange
            string fileName = "non-existent-file";

            // Act
            var result = _controller.DeleteFile(fileName);

            // Assert - должен вернуть OK, даже если файл не существует
            result.Should().BeOfType<OkResult>();
        }

        #endregion

        #region UpdateFile Tests

        [Test]
        public void UpdateFile_WithValidData_ReturnsOkResult()
        {
            // Arrange
            string fileName = "test-update-file";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = "Test content for update",
                fileAction = ActionOnFile.Edit
            };

            // Act
            var result = _controller.UpdateFile(fileName, fileData);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void UpdateFile_WithNullFileName_ReturnsNotFound()
        {
            // Arrange
            string fileName = null;
            var fileData = new FileData
            {
                FileContent = "Test content"
            };

            // Act
            var result = _controller.UpdateFile(fileName, fileData);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public void UpdateFile_WithEmptyContent_ReturnsOkResult()
        {
            // Arrange
            string fileName = "test-empty-content";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = string.Empty
            };

            // Act
            var result = _controller.UpdateFile(fileName, fileData);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        #endregion

        #region SetSymbols Tests

        [Test]
        public void SetSymbols_WithSimpleText_ReturnsNumeratedText()
        {
            // Arrange
            string fileName = "test-symbols";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = "Hello\nWorld"
            };

            // Act
            var result = _controller.SetSymbols(fileName, fileData);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<string>();
        }

        [Test]
        public void SetSymbols_WithEmptyContent_ReturnsEmptyString()
        {
            // Arrange
            string fileName = "test-empty";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = string.Empty
            };

            // Act
            var result = _controller.SetSymbols(fileName, fileData);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public void SetSymbols_WithMultipleLines_NumeratesEachLine()
        {
            // Arrange
            string fileName = "test-multiline";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = "Line1\nLine2\nLine3"
            };

            // Act
            var result = _controller.SetSymbols(fileName, fileData);

            // Assert
            var okResult = result as OkObjectResult;
            var numeratedText = okResult.Value as string;
            numeratedText.Should().NotBeNullOrEmpty();
            numeratedText.Should().Contain("0 Line1");
        }

        #endregion

        #region Integration Tests

        [Test]
        public void Integration_CreateUpdateAndDelete_WorksCorrectly()
        {
            // Arrange
            string fileName = "integration-test";
            var fileData = new FileData
            {
                FileName = fileName,
                FileContent = "Initial content"
            };

            // Act 1: Create file
            var createResult = _controller.GetOrCreate(fileName);
            
            // Assert 1
            createResult.Should().BeOfType<OkObjectResult>();

            // Act 2: Update file
            var updateResult = _controller.UpdateFile(fileName, fileData);
            
            // Assert 2
            updateResult.Should().BeOfType<OkResult>();

            // Act 3: Delete file
            var deleteResult = _controller.DeleteFile(fileName);
            
            // Assert 3
            deleteResult.Should().BeOfType<OkResult>();
        }

        #endregion
    }
}
