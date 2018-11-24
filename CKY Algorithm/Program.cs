using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYK_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader("input.txt");
            string sentence = streamReader.ReadLine();
            GrammarCreator grammarCreator = new GrammarCreator();
            Dictionary<string, List<string>> grammars = grammarCreator.CreateGrammar(streamReader);

            List<string>[,] results = CYKAlgorithm.ParseGrammar(sentence, grammars);
            CYKAlgorithm.PrintCYKResults(results, sentence);
        }
    }
}
