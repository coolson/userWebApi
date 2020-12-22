using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace userWebApi.Model
{
	public class DataClass
	{
        public string Str { get; set; }
        public string Filepath { get; set; }
        public string FileContent { get; set; }

		static string GetUpperStr(string str)
		{
			return str.ToUpper();
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
		static List<string> GetLinesListFromFile(string filePath)
		{
			List<string> lines = new List<string>();
			try
			{
				StreamReader reader = new StreamReader(filePath);

				using (reader)
				{
					string line = reader.ReadLine();
					int lineNumber = 1;


					while (line != null)
					{
						lines.Add(String.Format("Line {1}: {0}", line, lineNumber));
						lineNumber++;
						line = reader.ReadLine();
					}
				}
			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine(
					"Can not find file {0}.", filePath);
			}
			catch (DirectoryNotFoundException)
			{
				Console.Error.WriteLine(
					"Invalid directory in the file path.");
			}
			catch (IOException)
			{
				Console.Error.WriteLine(
					"Can not open the file {0}", filePath);
			}

			return lines;
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
		static void PrintFileContent(string filePath)
		{
			StreamReader reader = new StreamReader(filePath);
			string oldContent = "";
			using (reader)
			{
				oldContent = reader.ReadToEnd();
			}

			Console.WriteLine(oldContent);

		}
		static void AddTextToStartOfFile(string filePath, string text)
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

				//SetFileWithYourLines(filePath, );

				newContent = text + oldContent;
				string[] strArr = newContent.Split("\r\n");
				System.IO.File.WriteAllLines(filePath, strArr);

			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine(
					"Can not find file {0}.", filePath);
			}
			catch (DirectoryNotFoundException)
			{
				Console.Error.WriteLine(
					"Invalid directory in the file path.");
			}
			catch (IOException)
			{
				Console.Error.WriteLine(
					"Can not open the file {0}", filePath);
			}
		}
		static void DeleteStringByIndexFromFile(string filePath, int i)
		{
			var stringArr = GetFileContent(filePath).Split("\n");

			if (i < stringArr.Length && i >= 0)
			{
				stringArr = stringArr.Where(w => w != stringArr[i]).ToArray();

				SetFileWithYourLines(filePath, stringArr);
			}
			else
			{
				throw new Exception(String.Format("File has just {0} lines, you want delete {1}", stringArr.Length, i));
			}
		}
		static void EditStringInFile(string filePath, int inx, string text)
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



				string[] strArr = oldContent.Split("\n");
				strArr[inx] = text;
				System.IO.File.WriteAllLines(filePath, strArr);

			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine(
					"Can not find file {0}.", filePath);
			}
			catch (DirectoryNotFoundException)
			{
				Console.Error.WriteLine(
					"Invalid directory in the file path.");
			}
			catch (IOException)
			{
				Console.Error.WriteLine(
					"Can not open the file {0}", filePath);
			}
		}
		static void NumerateSymbols(string filePath)
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

				foreach (var raw in raws)
				{
					foreach (var el in raw)
					{
						Console.Write("  {0} ", el);
					}

					Console.WriteLine();
				}
			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine(
					"Can not find file {0}.", filePath);
			}
			catch (DirectoryNotFoundException)
			{
				Console.Error.WriteLine(
					"Invalid directory in the file path.");
			}
			catch (IOException)
			{
				Console.Error.WriteLine(
					"Can not open the file {0}", filePath);
			}
		}
		static void CompareTwoRaws(string filePath, int i1, int i2, string subStr)
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
				string[] strArr = oldContent.Split("\n");

				string f = strArr[i1];
				string s = strArr[i2];
				Console.WriteLine("Length of first raw: {0}", f.Length);
				Console.WriteLine("Length of second raw: {0}", s.Length);
				int a = f.IndexOf(subStr);
				if (a == -1)
				{
					Console.WriteLine("there is no that text in first raw");
				}
				else
				{
					Console.WriteLine("index of first match in first raw: {0}", a.ToString());

				}

				if (f.CompareTo(s) == 0)
				{ Console.WriteLine("Raws are equal"); }
				else
				{
					Console.WriteLine("Raws are not equal");

				}
			}
			catch (FileNotFoundException)
			{
				Console.Error.WriteLine(
					"Can not find file {0}.", filePath);
			}
			catch (DirectoryNotFoundException)
			{
				Console.Error.WriteLine(
					"Invalid directory in the file path.");
			}
			catch (IOException)
			{
				Console.Error.WriteLine(
					"Can not open the file {0}", filePath);
			}
		}
		static void MakeCopyOfFile(string filePath, string filePath2)
		{

			//File.Create(filePath2).Dispose();

			FileInfo fi1 = new FileInfo(filePath);
			FileInfo fi2 = new FileInfo(filePath2);
			fi1.CopyTo(filePath2);
			//Console.WriteLine("{0} was copied to {1}.", path, path2);
		}
		static void DeleteFile(string filePath)
		{
			File.Delete(filePath);

		}
		static void RenameFile(string path, string newName)
		{
			System.IO.File.Move(path, newName);

		}
		static void CreateNewDir(string dirPath)
		{
			// Specify the directory you want to manipulate.

			try
			{
				// Determine whether the directory exists.
				if (Directory.Exists(dirPath))
				{
					Console.WriteLine("That path exists already.");
					return;
				}

				// Try to create the directory.
				DirectoryInfo di = Directory.CreateDirectory(dirPath);
				Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(dirPath));

				// Delete the directory.
				//di.Delete();
				//Console.WriteLine("The directory was deleted successfully.");
			}
			catch (Exception e)
			{
				Console.WriteLine("The process failed: {0}", e.ToString());
			}
			finally { }
		}
		static void DeleteDir(string dirPath)
		{
			// Specify the directory you want to manipulate.

			try
			{
				// Determine whether the directory exists.
				if (Directory.Exists(dirPath))
				{
					Directory.Delete(dirPath);
					Console.WriteLine("The directory was deleted successfully.");
					return;
				}

				// Try to create the directory.
				//DirectoryInfo di = Directory.CreateDirectory(dirPath);
				//Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(dirPath));

				// Delete the directory.

			}
			catch (Exception e)
			{
				Console.WriteLine("The process failed: {0}", e.ToString());
			}
			finally { }
		}
		static void CopyDir(string dirPath, string dirPath2)
		{
			try
			{

				FileSystem.CopyDirectory(dirPath, dirPath2);
			}
			catch (Exception e)
			{
				Console.WriteLine("The process failed: {0}", e.ToString());
			}
			finally { }

		}
		static void RenameDir(string dirPath, string newNameDir)
		{
			Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(dirPath, newNameDir);
		}

	}

	public class UserData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsMale { get; set; }
        public string Education { get; set; }
        public bool HasCar { get; set; }


        private static string MakeEducationString(string education)
        {
            switch (education)
            {
                case "High":
                    return "Высшее";
                case "Partly":
                    return "Незаконченное высшее";
                case "Middle":
                    return "Среднее";
                default:
                    return "Загадочное";
            }
        }
    }
}
