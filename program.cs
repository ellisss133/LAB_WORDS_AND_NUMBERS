using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class FileProcessor
{
  private string directoryPath;
  private Dictionary<string, string> typoCorrections;
  private string phonePattern = @"\d{3} \d{3}-\d{2}-\d{2}";

  public FileProcessor(string directoryPath)
  {
    this.directoryPath = directoryPath;
    this.typoCorrections = new Dictionary<string, string>
    {
      { "првиет", "привет" },
      { "привье", "привет" },
      { "приеаь", "привет" },
      { "привел", "привет" },
      { "прiвiт", "привет" },
      { "пирвет", "привет" },
      { "друх", "друг" },
      { "друк", "друг" },
      { "друе", "друг" },
      { "друц", "друг" }
    };
  }

  public void ProcessFiles()
  {
    if (Directory.Exists(directoryPath))
    {
      string[] files = Directory.GetFiles(directoryPath, "*.txt");

      foreach (string file in files)
      {
        string fileContent = File.ReadAllText(file);
        fileContent = FixTypos(fileContent);
        File.WriteAllText(file, fileContent);
      }
    }
    else
    {
      Console.WriteLine("The directory was not found.");
    }
  }

  private string FixTypos(string content)
  {
    foreach (var typo in typoCorrections)
    {
      content = content.Replace(typo.Key, typo.Value);
    }
    return content;
  }
}

class Program
{
  static void Main()
  {
    Console.WriteLine("Paste the path to the folder with .txt file:");

    string directoryPath = Console.ReadLine();
    FileProcessor processor = new FileProcessor(directoryPath);
    processor.ProcessFiles();
  }
}