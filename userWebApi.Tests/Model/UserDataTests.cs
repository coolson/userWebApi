using System;
using NUnit.Framework;
using userWebApi.Model;
using FluentAssertions;

namespace userWebApi.Tests.Model
{
    /// <summary>
    /// Тесты для модели UserData
    /// </summary>
    [TestFixture]
    public class UserDataTests
    {
        [Test]
        public void UserData_CanBeCreated_WithDefaultValues()
        {
            // Act
            var userData = new UserData();

            // Assert
            userData.Should().NotBeNull();
            userData.Name.Should().BeNull();
            userData.Password.Should().BeNull();
            userData.IsMale.Should().BeFalse();
            userData.Education.Should().BeNull();
            userData.HasCar.Should().BeFalse();
        }

        [Test]
        public void UserData_CanSetName()
        {
            // Arrange
            var userData = new UserData();
            var expectedName = "Иван Иванов";

            // Act
            userData.Name = expectedName;

            // Assert
            userData.Name.Should().Be(expectedName);
        }

        [Test]
        public void UserData_CanSetPassword()
        {
            // Arrange
            var userData = new UserData();
            var expectedPassword = "SecurePassword123";

            // Act
            userData.Password = expectedPassword;

            // Assert
            userData.Password.Should().Be(expectedPassword);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void UserData_CanSetIsMale(bool isMale)
        {
            // Arrange
            var userData = new UserData();

            // Act
            userData.IsMale = isMale;

            // Assert
            userData.IsMale.Should().Be(isMale);
        }

        [Test]
        [TestCase("High")]
        [TestCase("Partly")]
        [TestCase("Middle")]
        [TestCase("None")]
        public void UserData_CanSetEducation(string education)
        {
            // Arrange
            var userData = new UserData();

            // Act
            userData.Education = education;

            // Assert
            userData.Education.Should().Be(education);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void UserData_CanSetHasCar(bool hasCar)
        {
            // Arrange
            var userData = new UserData();

            // Act
            userData.HasCar = hasCar;

            // Assert
            userData.HasCar.Should().Be(hasCar);
        }

        [Test]
        public void UserData_WithCompleteData_InitializesCorrectly()
        {
            // Arrange & Act
            var userData = new UserData
            {
                Name = "Петр Петров",
                Password = "Password123!",
                IsMale = true,
                Education = "High",
                HasCar = true
            };

            // Assert
            userData.Name.Should().Be("Петр Петров");
            userData.Password.Should().Be("Password123!");
            userData.IsMale.Should().BeTrue();
            userData.Education.Should().Be("High");
            userData.HasCar.Should().BeTrue();
        }

        [Test]
        public void UserData_CanBeCloned()
        {
            // Arrange
            var original = new UserData
            {
                Name = "Original User",
                Password = "Pass123",
                IsMale = true,
                Education = "High",
                HasCar = false
            };

            // Act
            var clone = new UserData
            {
                Name = original.Name,
                Password = original.Password,
                IsMale = original.IsMale,
                Education = original.Education,
                HasCar = original.HasCar
            };

            // Assert
            clone.Name.Should().Be(original.Name);
            clone.Password.Should().Be(original.Password);
            clone.IsMale.Should().Be(original.IsMale);
            clone.Education.Should().Be(original.Education);
            clone.HasCar.Should().Be(original.HasCar);
        }

        [Test]
        public void UserData_PasswordProperty_CanStoreSpecialCharacters()
        {
            // Arrange
            var userData = new UserData();
            var specialPassword = "P@$$w0rd!#%^&*()";

            // Act
            userData.Password = specialPassword;

            // Assert
            userData.Password.Should().Be(specialPassword);
        }

        [Test]
        public void UserData_NameProperty_CanStoreUnicodeCharacters()
        {
            // Arrange
            var userData = new UserData();
            var unicodeName = "Владимир Жуковский";

            // Act
            userData.Name = unicodeName;

            // Assert
            userData.Name.Should().Be(unicodeName);
        }
    }
}
