using TextAnalyze;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace booknumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length==0)
            { 
                Console.WriteLine("Provide the fully qualified path of the file to analyse.");
                return;
            }

            string path = args[0];
            string analysisParam = args.Length < 2 ? "-1" : args[1];
            Analysis.AnalysisType analysisType;
            switch (analysisParam)
            {
                case "-1":
                    analysisType = Analysis.AnalysisType.FileReader;
                    break;
                case "-2":
                default:
                    analysisType = Analysis.AnalysisType.StreamReader;
                    break;
            }

            Stopwatch s = new Stopwatch();
            s.Start();
            Analysis myAnalysis = new Analysis(args[0],analysisType);
            s.Stop();
            Console.WriteLine($"Text Analysis of {args[0]} using {analysisType.GetType().ToString()}");
            Console.WriteLine($"Completed in {s.Elapsed}");
            foreach (TextData w in myAnalysis.TextDictionary)
            {
                Console.WriteLine($"{w.Word} {w.Frequency} {w.IsPrime}");
            }

        }
    }
}
