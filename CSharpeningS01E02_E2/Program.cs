using Mono.Options;
using System;
using System.Collections.Generic;

namespace CSharpeningS01E02_E2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var namesFile = string.Empty;
            var namesCount = 0;
            var partsCount = 1;
            var separator = " ";
            var help = false;
                        
            var optionSet = new OptionSet {
                { "f|namesFile=",    "Text file with the words to use for making names - each word on separate line",
                    s => namesFile = s },
                { "n|namesCount=",  "Number of names to output",
                    (int n) => namesCount = n },
                { "p|partsCount=",  "How many parts each name consists of",
                    (int n) => partsCount = n },
                { "s|separator=",  "What character to use for separating the names",
                    s => separator = s },
                { "h|help",     "Display help",
                    h => help = h != null },
            };

            // Handle some of the bad input options.
            List<string> extra;
            try
            {
                extra = optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("CSharpeningS01E02_E2.exe: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `CSharpeningS01E02_E2.exe --help' for more information.");
                return;
            }

            // Print help information if requested. 
            if (help)
            {
                optionSet.WriteOptionDescriptions(Console.Out);
                return;
            }
            
            // Using the NameFactory
            var nameFactory = new NameFactory();

            try
            {
                nameFactory.CreateWordPool(namesFile);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(-1);
            }

            nameFactory.NamesCount = namesCount;
            nameFactory.PartsCount = partsCount;
            nameFactory.Separator = separator;

            try
            {
                foreach (var name in nameFactory.GetRandomNames())
                {
                    Console.WriteLine(name);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Failed to generate names: {ex.Message}");
                Environment.Exit(-1);
            }
        }
    }
}
