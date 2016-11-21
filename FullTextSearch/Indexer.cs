using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FullTextSearch
{
    public class Indexer
    {
        const string IndexFile = @".\Index.txt";
        const string Seperator = "#%#";
        static readonly Dictionary<string, SortedSet<string>> Indexes = new Dictionary<string, SortedSet<string>>();

        public static Dictionary<string, SortedSet<string>> GetIndexes()
        {
            if (!File.Exists(IndexFile) || Indexes.Any()) return Indexes;

            var lines = File.ReadAllLines(IndexFile);
            foreach (var index in lines.Select(line => line.Split(new[] { Seperator }, StringSplitOptions.RemoveEmptyEntries)))
                Indexes[index[0]] = new SortedSet<string>(index[1].Split(','));

            return Indexes;
        }

        public static void WriteIndexes(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName == null) return;

            if (Path.GetExtension(fileName) != ".txt")
            {
                Console.WriteLine("File type not supported. Use text files(.txt) only");
                return;
            }

            var allWords = GetFileContents(filePath);
            var indexes = UpdateIndex(allWords, fileName);
            WriteToFile(indexes);
        }

        private static IEnumerable<string> GetFileContents(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);
            var allWords = fileContents
                .Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim());
            return allWords;
        }

        private static Dictionary<string, SortedSet<string>> UpdateIndex(IEnumerable<string> allWords, string fileName)
        {
            var indexes = GetIndexes();
            foreach (var word in allWords)
            {
                if (indexes.ContainsKey(word))
                    indexes[word].Add(fileName);
                else
                    indexes[word] = new SortedSet<string> { fileName };
            }
            return indexes;
        }

        private static void WriteToFile(Dictionary<string, SortedSet<string>> indexes)
        {
            var linesToIndex = new StringBuilder();
            foreach (var index in indexes)
            {
                linesToIndex.AppendFormat("{0}{1}", index.Key, Seperator)
                    .AppendLine(string.Join(",", index.Value));
            }

            File.WriteAllText(IndexFile, linesToIndex.ToString());
        }
    }
}