using System;
using System.Diagnostics;
using TextAnalyze;
using Xunit;

namespace BookNumbers_Tests
{
    public class TextAnalyze_Should
    {
        private Analysis.AnalysisType atype = Analysis.AnalysisType.FileReader;

        [Fact]
        [Trait("Category", "FileIO")]
        /// <remarks>
        /// Attempting to open a nonexistent file should throw a FileNotFound exception
        /// </remarks>
        public void NonexistentInputFile_ExpectException()
        {
            string testfile="";
            Analysis.AnalysisType atype = Analysis.AnalysisType.FileReader;
            var exception = Record.Exception(() => new Analysis(testfile,atype));
            Assert.IsType(typeof(System.IO.FileNotFoundException), exception);
        }

        [Fact]
        [Trait("Category", "FileIO")]
        /// <remarks>
        /// Test01 - file containing one word should return a TextDictionary with one word
        /// </remarks>
        public void TestFile01_ContainsOneWord()
        {
            string testfile = "C:\\Users\\laure\\Dropbox\\ctm\\booknumbers\\booknumberstest\\tests\\test01.txt";
            Analysis _analysis = new Analysis(testfile, atype);
            Assert.Equal(1, _analysis.TextDictionary.Count);
        }

        [Fact]
        [Trait("Category", "Parsing")]
        /// <remarks>
        /// Test02 - file containing ten words on one line should return a TextDictionary with ten words
        /// </remarks>
        public void TestFile02_ContainsTenWords()
        {
            string testfile = "C:\\Users\\laure\\Dropbox\\ctm\\booknumbers\\booknumberstest\\tests\\test02.txt";
            Analysis _analysis = new Analysis(testfile, atype);
            Assert.Equal(10, _analysis.TextDictionary.Count);
        }

        [Fact]
        [Trait("Category", "Parsing")]
        /// <remarks>
        /// Test03 - file containing ten words over five lines should return a TextDictionary with ten words
        /// </remarks>
        public void TestFile03_ContainsTenWords()
        {
            string testfile = "C:\\Users\\laure\\Dropbox\\ctm\\booknumbers\\booknumberstest\\tests\\test03.txt";
            Analysis _analysis = new Analysis(testfile, atype);
            Assert.Equal(10, _analysis.TextDictionary.Count);
        }

        [Fact]
        [Trait("Category", "Parsing")]
        /// <remarks>
        /// Test04 - file with two words separated by comma should return a TextDictionary with two words
        /// </remarks>
        public void TestFile04_ContainsTwoWords()
        {
            string testfile = "C:\\Users\\laure\\Dropbox\\ctm\\booknumbers\\booknumberstest\\tests\\test04.txt";
            Analysis _analysis = new Analysis(testfile, atype);
            Assert.Equal(2, _analysis.TextDictionary.Count);
        }

        [Fact]
        [Trait("Category", "Parsing")]
        /// <remarks>
        /// Test05 - file containing unwanted characters around words comma should return a TextDictionary with cleaned words
        /// </remarks>
        public void TestFile05_ContainsCleanedWords()
        {
            string testfile = "C:\\Users\\laure\\Dropbox\\ctm\\booknumbers\\booknumberstest\\tests\\test05.txt";
            Analysis _analysis = new Analysis(testfile, atype);
            foreach (TextData x in _analysis.TextDictionary)
            {
                Debug.WriteLine($"{x.Word} {x.Frequency} {x.IsPrime}");
            }
            Assert.Equal(2, _analysis.TextDictionary.Count);
        }

        // TODO: test that a given word occurs x times in a file
        // TODO: test that a given word occuring a prime number of times is flagged as prime
        // TODO: test that a large file can be processed without resources being affected
        // TODO: test that a file can be processed in a target timeframe
        
    }
}
