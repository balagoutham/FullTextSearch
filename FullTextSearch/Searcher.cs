using System;
using System.Collections.Generic;
using System.Linq;

namespace FullTextSearch
{
    public class Searcher
    {
        public static void Search(IEnumerable<string> commands)
        {
            var searchIndexes = Indexer.GetIndexes();
            var searchWords = commands.Select(w => w.Trim()).Skip(1);

            var counter = 1;
            foreach (var word in searchWords)
            {
                Console.WriteLine("{0}. Searching for '{1}' ...", counter, word);
                if (searchIndexes.ContainsKey(word))
                {
                    Console.WriteLine("   Found in:");
                    Console.Write("\t");
                    Console.WriteLine(string.Join("\r\n\t", searchIndexes[word]));
                }
                else
                    Console.WriteLine("   No matches found.");

                counter++;
            }
        }
    }
}