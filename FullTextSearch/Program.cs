using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FullTextSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = args.ToList();
            while (true)
            {
                if (commands.Count < 2)
                {
                    Console.WriteLine("Use commands: Search or Index to continue..");

                    var readLine = Console.ReadLine();
                    if (readLine != null) commands = readLine.Split(' ').ToList();
                    if (commands.Count < 2) continue;
                }

                var operation = commands[0].ToLowerInvariant();
                switch (operation)
                {
                    case "search":
                        Searcher.Search(commands);
                        break;

                    case "index":
                        var filePath = commands[1].ToString(CultureInfo.InvariantCulture);
                        if (!File.Exists(filePath))
                            Console.WriteLine("Invalid File Path !!");
                        else
                        {
                            Indexer.WriteIndexes(filePath);
                            Console.WriteLine("File indexed successfully");
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecoginized command!");
                        break;
                }

                commands.Clear();
            }
        }
    }
}
