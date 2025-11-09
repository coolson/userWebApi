using System;
using NUnit.Framework;
using userWebApi.Model;
using FluentAssertions;

namespace userWebApi.Tests.Model
{
    /// <summary>
    /// Тесты для класса DataClass
    /// Тестируем публичные свойства, так как большинство методов приватные/статические
    /// </summary>
    [TestFixture]
    public class DataClassTests
    {
        [Test]
        public void DataClass_CanBeCreated()
        {
            // Act
            var dataClass = new DataClass();

            // Assert
            dataClass.Should().NotBeNull();
        }

        [Test]
        public void DataClass_StrProperty_CanBeSetAndGet()
        {
            // Arrange
            var dataClass = new DataClass();
            var expectedStr = "Test String";

            // Act
            dataClass.Str = expectedStr;

            // Assert
            dataClass.Str.Should().Be(expectedStr);
        }

        [Test]
        public void DataClass_FilepathProperty_CanBeSetAndGet()
        {
            // Arrange
            var dataClass = new DataClass();
            var expectedPath = @"C:\temp\test.txt";

            // Act
            dataClass.Filepath = expectedPath;

            // Assert
            dataClass.Filepath.Should().Be(expectedPath);
        }

        [Test]
        public void DataClass_FileContentProperty_CanBeSetAndGet()
        {
            // Arrange
            var dataClass = new DataClass();
            var expectedContent = "This is test file content\nWith multiple lines";

            // Act
            dataClass.FileContent = expectedContent;

            // Assert
            dataClass.FileContent.Should().Be(expectedContent);
        }

        [Test]
        public void DataClass_AllProperties_CanBeSetSimultaneously()
        {
            // Arrange
            var dataClass = new DataClass();
            var expectedStr = "Test";
            var expectedPath = @"C:\test\file.txt";
            var expectedContent = "Content";

            // Act
            dataClass.Str = expectedStr;
            dataClass.Filepath = expectedPath;
            dataClass.FileContent = expectedContent;

            // Assert
            dataClass.Str.Should().Be(expectedStr);
            dataClass.Filepath.Should().Be(expectedPath);
            dataClass.FileContent.Should().Be(expectedContent);
        }

        [Test]
        public void DataClass_Properties_DefaultToNull()
        {
            // Act
            var dataClass = new DataClass();

            // Assert
            dataClass.Str.Should().BeNull();
            dataClass.Filepath.Should().BeNull();
            dataClass.FileContent.Should().BeNull();
        }

        [Test]
        public void DataClass_StrProperty_CanHandleEmptyString()
        {
            // Arrange
            var dataClass = new DataClass();

            // Act
            dataClass.Str = string.Empty;

            // Assert
            dataClass.Str.Should().BeEmpty();
        }

        [Test]
        public void DataClass_FileContentProperty_CanHandleMultilineText()
        {
            // Arrange
            var dataClass = new DataClass();
            var multilineContent = "Line 1\nLine 2\nLine 3\nLine 4";

            // Act
            dataClass.FileContent = multilineContent;

            // Assert
            dataClass.FileContent.Should().Be(multilineContent);
            dataClass.FileContent.Split('\n').Should().HaveCount(4);
        }

        [Test]
        public void DataClass_FilepathProperty_CanHandleUnixPath()
        {
            // Arrange
            var dataClass = new DataClass();
            var unixPath = "/home/user/documents/test.txt";

            // Act
            dataClass.Filepath = unixPath;

            // Assert
            dataClass.Filepath.Should().Be(unixPath);
        }

        [Test]
        public void DataClass_CanBeUsedAsSingleton()
        {
            // Arrange
            var instance1 = new DataClass();
            instance1.Str = "Instance 1";

            var instance2 = new DataClass();
            instance2.Str = "Instance 2";

            // Assert - different instances should have different values
            instance1.Str.Should().NotBe(instance2.Str);
        }
    }
}
