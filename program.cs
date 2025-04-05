using System;

class Program
{
  static void Main()
  {
    Console.WriteLine("Paste the path to the folder with .txt file:");
    Console.WriteLine();
    
    string directoryPath = Console.ReadLine();
    FileProcessor processor = new FileProcessor(directoryPath);
    processor.ProcessFiles();
  }
}