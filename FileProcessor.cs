using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class FileProcessor
{
  private string directoryPath;
  private Dictionary<string, string> typoCorrections;
  private string phonePattern = @"\((\d{3})\) (\d{3})-(\d{2})-(\d{2})";

  public FileProcessor(string directoryPath)
  {
    this.directoryPath = directoryPath;
    this.typoCorrections = new Dictionary<string, string>();

    Console.WriteLine("Enter the number of typos to replace:");
    if (int.TryParse(Console.ReadLine(), out int count))
    {
      for (int index = 0; index < count; ++index)
      {
        Console.WriteLine($"#{index + 1} Misspell:");
        string typo = Console.ReadLine();
        Console.WriteLine($"#{index + 1} Correction:");
        string correction = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(typo) && !string.IsNullOrWhiteSpace(correction))
        {
          typoCorrections[typo] = correction;
        }
      }
    }
    else
    {
      Console.WriteLine("Invalid input, there will be no replacements.");
    }
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
        fileContent = FixPhoneNumbers(fileContent);
        File.WriteAllText(file, fileContent);
      }
    }
    else
    {
      Console.WriteLine("The folder was not found.");
    }
  }

  private string FixTypos(string content)
  {
    foreach (var typo in typoCorrections)
    {
      Console.WriteLine($"replacing this: {typo.Key} with this: {typo.Value}");
      content = content.Replace(typo.Key, typo.Value);
    }
    return content;
  }

  private string FixPhoneNumbers(string content)
  {
    return Regex.Replace(content, phonePattern, new MatchEvaluator(FormatPhoneNumber));
  }

  private string FormatPhoneNumber(Match match)
  {
    string digits = Regex.Replace(match.Value, @"\D", "");

    if (digits.Length == 10)
    {
      return $"+380 {digits.Substring(0, 2)} {digits.Substring(2, 3)} {digits.Substring(5, 2)} {digits.Substring(7, 2)}";
    }
    else
    {
      return match.Value;
    }
  }
}