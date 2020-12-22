using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using userWebApi.Model;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    public class UserRegisterController : ControllerBase
    {
        public DataClass Dataclass;
        public UserRegisterController(DataClass dtc)
        {
            Dataclass = dtc;
        }
        [HttpGet]
        public string Get()
        {
            return "I'm healthy";
        }

        [HttpGet]
        [Route("Check")]
        public IActionResult Check(FileData fd)
        {
            fd.FileContent.ToUpper();
            fd.FileName.ToUpper();
            return Ok(fd);
        }
        
        [HttpGet]
        [Route("Create")]
        public IActionResult CreateFile(FileData fd)
        {
            string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (FileStream fs = System.IO.File.Create(path)) ;
            return Ok();
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult DeleteFile(FileData fd)
        {
            string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetContent")]
        public IActionResult GetContent(FileData fd)
        {
            string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
            if (System.IO.File.Exists(path))
            {
                return Ok(fd.FileContent);
            }
            else
            {
                throw new Exception("no file");
            }
        }

        [HttpGet]
        [Route("EditContent")]
        public IActionResult EditContent(FileData fd)
        {
            string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, string.Empty);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(fd.FileContent);
                }
            }
            else
            {
                throw new Exception("no file");
            }
            return Ok();
        }


        [HttpGet]
        [Route("SetSymbols")]
        public IActionResult SetSymbols(FileData fd)
        {
            string path = @"C:\dev\react_laba2\userWebApi\ClientApp\src\" + fd.FileName + ".txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, string.Empty);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(fd.FileContent);
                }
                var s = NumerateSymbols(path);
                System.IO.File.WriteAllText(path, string.Empty);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(s);
                }
            }
            else
            {
                throw new Exception("no file");
            }
            return Ok(GetFileContent(path));
        }

        static string[] NumerateSymbols(string filePath)
        {
            try
            {
                string oldContent = "";
                string newContent = "";

                StreamReader reader = new StreamReader(filePath);

                using (reader)
                {
                    oldContent = reader.ReadToEnd();
                }


                int inx = 0;
                string[] strArr = oldContent.Split("\n");
                List<List<string>> raws = new List<List<string>>();
                for (int i = 0; i < strArr.Length; i++)
                {
                    List<string> oneRaw = new List<string>();
                    for (int j = 0; j < strArr[i].Length; j++)
                    {
                        if (Char.IsDigit(strArr[i][0]))
                        {

                            if (Char.IsDigit(strArr[i][0]) && strArr[i][j] == '\r')
                            {
                                oneRaw.Add(")");
                                oneRaw.Insert(0, "(");

                            }
                            else
                            {
                                oneRaw.Add(strArr[i][j].ToString());

                            }
                        }
                        else
                        {
                            if (strArr[i][j] != '\r' && strArr[i][j] != '\n')
                            {
                                if (strArr[i][j] == ' ')
                                {
                                    oneRaw.Add(" ");
                                }
                                else
                                {
                                    oneRaw.Add(String.Format("{0}: {1}", inx, strArr[i][j].ToString()));
                                    //oneRaw.Add(strArr[i][j].ToString());
                                    inx++;
                                }
                            }
                        }
                    }
                    raws.Add(new List<string>(oneRaw));
                    oneRaw.Clear();
                }

                return strArr;

            }
            catch (FileNotFoundException)
            {
                throw new Exception("FileNotFoundException");
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("DirectoryNotFoundException");
            }
            catch (IOException)
            {
                throw new Exception("IOException");
            }
        }
        static string GetFileContent(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string oldContent = "";
            using (reader)
            {
                oldContent = reader.ReadToEnd();
            }

            return oldContent;
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
