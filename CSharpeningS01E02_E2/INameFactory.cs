using System.Collections.Generic;

namespace CSharpeningS01E02_E2
{
    /// <summary>
    /// A factory that creates random names based on input.
    /// </summary>
    public interface INameFactory
    {
        /// <summary>
        /// Gets or sets how many names was requested.
        /// </summary>
        int NamesCount { get; set; }
        /// <summary>
        /// Gets or sets of how many parts a name should consist.
        /// </summary>
        int PartsCount { get; set; }
        /// <summary>
        /// Gets or sets the character to be used as names separator.
        /// </summary>
        /// <remarks>
        /// Will only be used when the PartsCount > 1.
        /// </remarks>
        string Separator { get; set; }

        /// <summary>
        /// Gets a generator that returns random names.
        /// The returned amount will match requested amount but names uniqueness is not required.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetRandomNames();

        /// <summary>
        /// Reads the words from file to be used as (parts of) names.
        /// </summary>
        void CreateWordPool(string filePath);

    }
}