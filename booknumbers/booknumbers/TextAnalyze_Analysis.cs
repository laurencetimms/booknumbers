using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TextAnalyze
{
    /// <summary>
    /// The main Analyze class
    /// The text is analyzed on instantation 
    /// </summary>
    /// <returns>
    /// Returns a List<TextData>
    /// </returns>
    public class Analysis
{        
        /// <value>
        /// buffer size for streamreader
        /// </value>
        const Int32 BufferSize = 128;

        /// <value>
        /// enum representing analysis approaches
        /// </value>
        public enum AnalysisType : byte { FileReader=1, StreamReader=2 };

        /// Internal list of TextData objects 
        private List<TextData> _wordList = new List<TextData>();

        /// <value>
        /// Exposes the analysed list of TextData objects
        /// </value>
        public List<TextData> TextDictionary
        {
            get { return _wordList; }
        }

        /// <summary>
        /// Invokes the requested analysis of the given text 
        /// </summary>
        public Analysis(string path, AnalysisType analysisType)
        {
            // ensure the file path is valid and the file exists before trying to open it
            if (!Utils.ValidateFile(path))
                return;

            // execute the preferred analysis
            switch (analysisType)
            {
                case AnalysisType.FileReader:
                    FileReaderAnalysis(path);
                break;

                case AnalysisType.StreamReader:
                default:
                    StreamReaderAnalysis(path);
                break;
            }
        }

        /// <summary>
        /// Analyses text using file.readlines
        /// </summary>
        private void FileReaderAnalysis(string path)
        {
            // read each line of text from the file
            foreach (string line in File.ReadLines(path))
            {
                // tidy the line and split it into words
                foreach (string s in Utils.SplitWords(Utils.TidyLine(line)))
                {
                    // if the word is new, add it to the word list
                    // and always increment the number of times the
                    // word has been found by 1
                    if (!_wordList.Any(x => x.Word == s))
                        _wordList.Add(new TextData { Word = s, Frequency = 1, IsPrime = false });
                    else
                        _wordList.First(w => w.Word == s).Frequency++;
                }
            }
            FinaliseAnalysis();
        }

        /// <summary>
        /// Analyses text using streamreader
        /// </summary>
        private void StreamReaderAnalysis(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // tidy the line and split it into words
                    foreach (string s in Utils.SplitWords(Utils.TidyLine(line)))
                    {
                        // if the word is new, add it to the word list
                        // and always increment the number of times the
                        // word has been found by 1
                        if (!_wordList.Any(x => x.Word == s))
                            _wordList.Add(new TextData { Word = s, Frequency = 1, IsPrime = false });
                        else
                            _wordList.First(w => w.Word == s).Frequency++;
                    }
                }
            }
            FinaliseAnalysis();
        }

        /// <summary>
        /// Finalises a text analysis by updating prime calculations and ordering the list
        /// </summary>
        private void FinaliseAnalysis()
        {
            // scan the word list for prime numbers
            foreach (TextData _word in _wordList.Where(r => Utils.IsPrime(r.Frequency)))
                _word.IsPrime = true;

            // order by descending frequency of word
            _wordList = _wordList.OrderByDescending(x => x.Frequency).ToList();

        }
    }

}
