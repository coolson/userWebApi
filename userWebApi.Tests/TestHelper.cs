using System;
using System.IO;

namespace userWebApi.Tests
{
    /// <summary>
    /// Вспомогательный класс для работы с тестами
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Получить путь к временной директории для тестов
        /// </summary>
        public static string GetTestDirectory()
        {
            return Path.Combine(Path.GetTempPath(), "userWebApiTests", Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Создать временную директорию для тестов
        /// </summary>
        public static string CreateTestDirectory()
        {
            var path = GetTestDirectory();
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Удалить временную директорию после тестов
        /// </summary>
        public static void CleanupTestDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception ex)
                {
                    // Логируем, но не падаем
                    Console.WriteLine($"Failed to cleanup test directory: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Создать временный файл с контентом
        /// </summary>
        public static string CreateTestFile(string directory, string fileName, string content)
        {
            var filePath = Path.Combine(directory, fileName);
            File.WriteAllText(filePath, content);
            return filePath;
        }

        /// <summary>
        /// Проверить существование файла
        /// </summary>
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Прочитать содержимое файла
        /// </summary>
        public static string ReadFileContent(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            return File.ReadAllText(filePath);
        }
    }
}
