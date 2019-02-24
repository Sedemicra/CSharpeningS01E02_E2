using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpeningS01E02_E2
{ 
    public sealed class NameFactory : INameFactory
    {        
        public int NamesCount { get; set; }
        
        public int PartsCount { get; set; }
        
        public string Separator { get; set; }

        // All words read from the input file are stored in a list to be used later.
        private readonly List<string> wordPool = new List<string>();

        public IEnumerable<string> GetRandomNames()
        {
            if (NamesCount <= 0)
                throw new InvalidOperationException("Requested amount must be positive.");
            if(PartsCount <= 0)
                throw new InvalidOperationException("Name parts count must be positive.");

            var rnd = new Random();
            var count = 0;
            // Generating the random names one by one.
            // Generator doesn't care if the word has already been used or if the returned combination has already been returned.
            do
            {
                var parts = new List<string>();
                for (int i = 0; i < PartsCount; i++)
                {
                    parts.Add(wordPool[rnd.Next(0, wordPool.Count)]);
                }
                yield return string.Join(Separator, parts);
                count++;
            } while (count < NamesCount);
        }

        public void CreateWordPool(string filePath)
        {
            bool failure = false;
            try
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        // Filter out empty lines from input file.
                        if (s != "")
                        wordPool.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encountered a problem with retrieving words from file: {ex.Message}");
                failure = true;
            }
            if (failure || wordPool.Count < 1)
            {
                throw new InvalidOperationException("At least one word is required from the words input file.");
            }
        }
    }
}
