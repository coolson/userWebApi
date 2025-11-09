using System;
using NUnit.Framework;
using userWebApi;
using FluentAssertions;

namespace userWebApi.Tests.Model
{
    /// <summary>
    /// Тесты для модели FileData
    /// </summary>
    [TestFixture]
    public class FileDataTests
    {
        [Test]
        public void FileData_CanBeCreated_WithDefaultValues()
        {
            // Act
            var fileData = new FileData();

            // Assert
            fileData.Should().NotBeNull();
            fileData.FileName.Should().BeNull();
            fileData.FileContent.Should().BeNull();
            fileData.fileAction.Should().Be(ActionOnFile.Create);
        }

        [Test]
        public void FileData_CanSetFileName()
        {
            // Arrange
            var fileData = new FileData();
            var expectedFileName = "test-file.txt";

            // Act
            fileData.FileName = expectedFileName;

            // Assert
            fileData.FileName.Should().Be(expectedFileName);
        }

        [Test]
        public void FileData_CanSetFileContent()
        {
            // Arrange
            var fileData = new FileData();
            var expectedContent = "This is test content";

            // Act
            fileData.FileContent = expectedContent;

            // Assert
            fileData.FileContent.Should().Be(expectedContent);
        }

        [Test]
        public void FileData_CanSetFileAction()
        {
            // Arrange
            var fileData = new FileData();

            // Act & Assert for each action
            fileData.fileAction = ActionOnFile.Create;
            fileData.fileAction.Should().Be(ActionOnFile.Create);

            fileData.fileAction = ActionOnFile.Delete;
            fileData.fileAction.Should().Be(ActionOnFile.Delete);

            fileData.fileAction = ActionOnFile.GetContent;
            fileData.fileAction.Should().Be(ActionOnFile.GetContent);

            fileData.fileAction = ActionOnFile.Edit;
            fileData.fileAction.Should().Be(ActionOnFile.Edit);

            fileData.fileAction = ActionOnFile.SetSymbols;
            fileData.fileAction.Should().Be(ActionOnFile.SetSymbols);
        }

        [Test]
        public void FileData_WithCompleteData_InitializesCorrectly()
        {
            // Arrange & Act
            var fileData = new FileData
            {
                FileName = "complete-test.txt",
                FileContent = "Complete test content",
                fileAction = ActionOnFile.Edit
            };

            // Assert
            fileData.FileName.Should().Be("complete-test.txt");
            fileData.FileContent.Should().Be("Complete test content");
            fileData.fileAction.Should().Be(ActionOnFile.Edit);
        }
    }

    /// <summary>
    /// Тесты для перечисления ActionOnFile
    /// </summary>
    [TestFixture]
    public class ActionOnFileTests
    {
        [Test]
        public void ActionOnFile_HasExpectedValues()
        {
            // Assert
            ((int)ActionOnFile.Create).Should().Be(0);
            ((int)ActionOnFile.Delete).Should().Be(1);
            ((int)ActionOnFile.GetContent).Should().Be(2);
            ((int)ActionOnFile.Edit).Should().Be(3);
            ((int)ActionOnFile.SetSymbols).Should().Be(4);
        }

        [Test]
        [TestCase(ActionOnFile.Create, "Create")]
        [TestCase(ActionOnFile.Delete, "Delete")]
        [TestCase(ActionOnFile.GetContent, "GetContent")]
        [TestCase(ActionOnFile.Edit, "Edit")]
        [TestCase(ActionOnFile.SetSymbols, "SetSymbols")]
        public void ActionOnFile_ToString_ReturnsCorrectName(ActionOnFile action, string expectedName)
        {
            // Act
            var result = action.ToString();

            // Assert
            result.Should().Be(expectedName);
        }

        [Test]
        public void ActionOnFile_CanBeCastToInt()
        {
            // Arrange
            ActionOnFile action = ActionOnFile.Edit;

            // Act
            int intValue = (int)action;

            // Assert
            intValue.Should().Be(3);
        }

        [Test]
        public void ActionOnFile_CanBeCastFromInt()
        {
            // Arrange
            int intValue = 2;

            // Act
            ActionOnFile action = (ActionOnFile)intValue;

            // Assert
            action.Should().Be(ActionOnFile.GetContent);
        }
    }
}
