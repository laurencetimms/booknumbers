using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyze
{
    public static class Utils
    {
        /// Characters that will be used delimit words
        private static char[] delimiterChars = { ' ', ',', '.', ':', ';', '-', '\t' };

        /// <summary>
        /// Determines whether a given integer is a prime number
        /// Normal mathematic rules apply: zero and negative numbers are not prime, nor is 1. 
        /// </summary>
        /// <returns>Boolean true if the number is prime else false</returns>
        public static bool IsPrime(int number)
        {
            if (number < 1) return false;
            if (number == 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Tidies a line of text by rebuilding it with letter characters unchanged and other characters converted to spaces,
        /// then shifting the result to lower case and trimming leading and trailing spaces
        /// </summary>
        /// <returns>String tidied line</returns>
        public static string TidyLine(string line)
        {
            string newline = String.Empty;
            foreach (char c in line)
            {
                if (char.IsLetter(c))
                    newline += c;
                else
                    newline += " ";
            }
            return newline.ToLower().Trim();
        }

        /// <summary>
        /// Splits a line of text into constituent words
        /// </summary>
        /// <returns>string array of words, discarding empty array entries</returns>
        public static string[] SplitWords(string line) => line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Uber simple file validator - only returns true if there is a file that can be opened at path
        /// </summary>
        /// <param name="path">string path of file to validate</param>
        /// <returns></returns>
        public static bool ValidateFile(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                    return false;
            }
            catch 
            {
                return false;
            }
            return true;
        }


    }
}
