using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using userWebApi.Model;
using System.IO;
using System.Linq;

namespace userWebApi
{
    public enum ActionOnFile
    {
        Create = 0,
        Delete = 1,
        GetContent = 2,
        Edit = 3,
        SetSymbols = 4
    }

    public class FileData
    {
        public string FileName { get; set; }
        public string FileContent { get; set; }

        public ActionOnFile fileAction { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DataFileController : ControllerBase
    {
        public DataClass Dataclass;
        public DataFileController(DataClass dtc)
        {
            Dataclass = dtc;
        }
        [HttpGet]
        [Route("health")] //http://localhost:51001/api/datafile/health
        public string Get()
        {
            return "I'm healthy";
        }

        [HttpGet]
        public IActionResult GetOrCreate(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return NotFound(fileName);

            if (System.IO.File.Exists(PathTheFile(fileName)))
                return Ok(System.IO.File.ReadAllText(PathTheFile(fileName)));

            //creating stream and closing it with the file
            using (var stream = System.IO.File.Create(PathTheFile(fileName), 1024)) { }
            return Ok();
        }

        private static string PathTheFile(string fileName)
        {
            if (!System.IO.Directory.Exists(@"\temp"))
                System.IO.Directory.CreateDirectory(@"\temp");

            return @$"\temp\{fileName}.txt";
        }

        [HttpDelete]
        public IActionResult DeleteFile(string fileName)
        {
            if (System.IO.File.Exists(PathTheFile(fileName)))
                System.IO.File.Delete(PathTheFile(fileName));

            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateFile(string fileName, [FromBody] FileData data)
        {
            if (string.IsNullOrEmpty(fileName))
                return NotFound(fileName);

            System.IO.File.WriteAllText(PathTheFile(fileName), data.FileContent, System.Text.Encoding.UTF8);
            return Ok();
        }


        //[HttpGet]
        //[Route("GetContent")]
        //public IActionResult GetContent(FileData fd)
        //{
        //    string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
        //    if (System.IO.File.Exists(path))
        //    {
        //        return Ok(fd.FileContent);
        //    }
        //    else
        //    {
        //        throw new Exception("no file");
        //    }
        //}

        //[HttpGet]
        //[Route("EditContent")]
        //public IActionResult EditContent(FileData fd)
        //{
        //    string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.WriteAllText(path, string.Empty);
        //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        //        {
        //            file.WriteLine(fd.FileContent);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("no file");
        //    }
        //    return Ok();
        //}


        [HttpPost]
        [Route("SetSymbols")]
        public IActionResult SetSymbols(string fileName, FileData fd)
        {
            return Ok(NumerateSymbols(fd.FileContent));
        }

        static string NumerateSymbols(string content)
        {
            var lines = content.Split('\n', '\r')
                .Select((x, i) => NumerateLine(x, i))
                .ToArray();

            return string.Join('\n', lines);
        }

        static string NumerateLine(string line, int no)
        {
            if (string.IsNullOrEmpty(line))
                return "";

            return char.IsDigit(line[0]) ? $"{no} ({line})" : $"{no} {line}";
        }

        static string GetFileContent(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }

        static void SetFileWithYourLines(string filePath, string[] lines)
        {
            //System.IO.File.WriteAllLines(filePath, testLines);
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    file.WriteLine(line);
                }
            }


        }


    }
}
