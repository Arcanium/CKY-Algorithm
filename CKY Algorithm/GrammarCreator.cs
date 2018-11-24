using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYK_Algorithm
{
    class GrammarCreator
    {
        //Line split "S -> A B" produces 4 items.
        //Line split "A -> Hello" produces 3 items.
        //Line split "B -> World | Yourself" produces 5 items.
        //Non terminals will always be 4 items. Terminals will always be odd.

        Dictionary<string, List<string>> grammars;

        public GrammarCreator()
        {
            grammars = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> CreateGrammar(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                string grammar = streamReader.ReadLine();
                string[] grammarPieces = grammar.Split(' ');
                if (grammarPieces.Length == 4) //Non-terminal.
                    ParseNonTerminal(grammarPieces);
                else if(grammarPieces.Length >= 3) //Terminal.
                    ParseTerminal(grammarPieces);
            }
            return grammars;
        }

        private void ParseNonTerminal(string[] grammarPieces)
        {
            //0  1  2 3
            //S ->  A B
            List<string> nonTerminals = new List<string>
            {
                grammarPieces[2].ToUpper(),
                grammarPieces[3].ToUpper()
            };

            grammars.Add(grammarPieces[0], nonTerminals);
        }

        private void ParseTerminal(string[] grammarPieces)
        {
            //0  1  2  3  4  ...
            //S -> "H" | "E"
            List<string> terminals = new List<string>();
            int index = 2; //0 and 1 are S and ->
            while (index < grammarPieces.Length)
            {
                terminals.Add(grammarPieces[index].ToLower());
                index += 2;
            }

            grammars.Add(grammarPieces[0], terminals);
        }
    }
}
